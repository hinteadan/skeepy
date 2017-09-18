using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Core.Storage;
using NLog;
using System;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public sealed class LoginFlow
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly ICanStoreSkeepy<Token> tokenStore;
        private readonly CredentialsAuthenticator credentialsAuthenticator;

        public LoginFlow(CredentialsAuthenticator credentialsAuthenticator, ICanStoreSkeepy<Token> tokenStore)
        {
            this.credentialsAuthenticator = credentialsAuthenticator ?? throw new InvalidOperationException($"Must provide a {nameof(credentialsAuthenticator)}");
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public Task<AuthenticationResult> Login(Credentials credentials)
        {
            log.Info($"Logging in {credentials.Username}...");

            return credentialsAuthenticator
                .Authenticate(credentials)
                .ContinueWith(t =>
                {
                    if (!t.Result.IsSuccessful)
                    {
                        log.Info($"Login failed for {credentials.Username} because {t.Result.FailureReason}");
                        return t.Result;
                    }

                    tokenStore.Put(t.Result.Token).Wait();

                    log.Info($"Login successful for {credentials.Username}");
                    return t.Result;
                });
        }
    }
}
