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
    public abstract class TokenAuthenticator : ICanAuthenticate<string>
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly ICanGetSkeepyEntity<Token> tokenStore;

        public TokenAuthenticator(ICanGetSkeepyEntity<Token> tokenStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public Task<AuthenticationResult> Authenticate(string identifier)
        {
            log.Info($"Authenticating {identifier}...");

            return tokenStore
                .Get(identifier)
                .ContinueWith(t => t.Result == null ?
                    AuthenticationResult.Failed(AuthenticationFailureReason.InvalidToken).AndLog(x => log.Info($"Authentication failed for {identifier} as the token is invalid")) :
                    AuthenticateToken(t.Result));
        }

        private AuthenticationResult AuthenticateToken(Token token)
        {
            if (token.HasExpired())
            {
                return AuthenticationResult.Failed(AuthenticationFailureReason.TokenExpired, token).AndLog(x => log.Info($"Authentication failed for {token.Id} as the token has expired"));
            }

            return IsTokenValid(out var reason) ?
                AuthenticationResult.Successful(token).AndLog(x => log.Info($"Authentication successful for {token.Id}")) :
                AuthenticationResult.Failed(reason, token).AndLog(x => log.Info($"Authentication failed for {token.Id} because {reason}"));
        }

        protected virtual bool IsTokenValid(out AuthenticationFailureReason reason)
        {
            reason = AuthenticationFailureReason.None;
            return true;
        }
    }
}
