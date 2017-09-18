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
    public abstract class CredentialsAuthenticator : ICanAuthenticate<Credentials>
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly ICanGenerateTokens<Credentials> tokenGenerator;
        private readonly ICanGetSkeepyEntity<Credentials> credentialStore;

        public CredentialsAuthenticator(ICanGetSkeepyEntity<Credentials> credentialStore, ICanGenerateTokens<Credentials> tokenGenerator)
        {
            this.credentialStore = credentialStore ?? throw new InvalidOperationException($"Must provide a {nameof(credentialStore)}");
            this.tokenGenerator = tokenGenerator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenGenerator)}");
        }

        public Task<AuthenticationResult> Authenticate(Credentials identifier)
        {
            log.Info($"Authenticating {identifier.Username}...");

            return credentialStore
                .Get(identifier.Id)
                .ContinueWith(t =>
                {
                    if (t.Result == null)
                    {
                        log.Info($"Authentication failed for {identifier.Username} as the user doesn't exist");
                        return AuthenticationResult.Failed(AuthenticationFailureReason.InvalidCredentials);
                    }

                    return t.Result.Username == identifier.Username && (t.Result.Password == identifier.Password || Hasher.Verify(t.Result.Password, identifier.Password)) ?
                        AuthenticationResult.Successful(tokenGenerator.Generate(identifier)).AndLog(x => log.Info($"Authentication successful for {identifier.Username}")) :
                        AuthenticationResult.Failed(AuthenticationFailureReason.InvalidCredentials).AndLog(x => log.Info($"Authentication failed for {identifier.Username} as the password was incorrect"));
                });
        }
    }
}
