using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Core.Storage;
using H.Skeepy.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public sealed class TokenFlow
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly TokenAuthenticator tokenAuthenticator;
        private readonly ICanGenerateTokens<string> tokenGenerator;
        private readonly ICanStoreSkeepy<Token> tokenStore;
        private readonly TimeSpan tokenRegenerationTimeout;

        public TokenFlow(TokenAuthenticator tokenAuthenticator, ICanGenerateTokens<string> tokenGenerator, ICanStoreSkeepy<Token> tokenStore, TimeSpan tokenRegenerationTimeout)
        {
            this.tokenAuthenticator = tokenAuthenticator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenAuthenticator)}");
            this.tokenGenerator = tokenGenerator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenGenerator)}");
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
            this.tokenRegenerationTimeout = tokenRegenerationTimeout;
        }

        public TokenFlow(TokenAuthenticator tokenAuthenticator, ICanGenerateTokens<string> tokenGenerator, ICanStoreSkeepy<Token> tokenStore) : this(tokenAuthenticator, tokenGenerator, tokenStore, TimeSpan.FromDays(14))
        { }

        public Task<AuthenticationResult> Authenticate(string publicToken)
        {
            log.Info($"Authenticating token {publicToken}...");

            return tokenAuthenticator
                .Authenticate(publicToken)
                .ContinueWith(t =>
                {
                    if (t.Result.IsSuccessful)
                    {
                        log.Info($"Token authentication successful for {publicToken}");
                        return t.Result;
                    }

                    if (t.Result.Token.HasExpired() && !HasTimedOut(t.Result.Token))
                    {
                        using (log.Timing($"Token has expired, will refresh it"))
                        {
                            var newToken = tokenGenerator.Generate(t.Result.Token.UserId);
                            Task.WaitAll(
                                tokenStore.Zap(t.Result.Token.Id),
                                tokenStore.Put(newToken)
                                );
                            return AuthenticationResult.Successful(newToken).AndLog(x => log.Info($"Token authentication successful for {publicToken} and refreshed to {newToken.Id}"));
                        }
                    }

                    tokenStore.Zap(t.Result.Token.Id).Wait();
                    return AuthenticationResult.Failed(AuthenticationFailureReason.TokenExpiredAndTimedOut, t.Result.Token).AndLog(x => log.Info($"Token authentication failed for {publicToken} because it has timed out"));
                });
        }

        private bool HasTimedOut(Token token)
        {
            return DateTime.Now >= token.GeneratedAt + tokenRegenerationTimeout;
        }
    }
}
