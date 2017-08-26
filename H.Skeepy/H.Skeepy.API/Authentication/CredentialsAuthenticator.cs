using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public abstract class CredentialsAuthenticator : ICanAuthenticate<Credentials>
    {
        private readonly ICanGenerateTokens<Credentials> tokenGenerator;
        private readonly ICanGetSkeepyEntity<Credentials> credentialStore;

        public CredentialsAuthenticator(ICanGetSkeepyEntity<Credentials> credentialStore, ICanGenerateTokens<Credentials> tokenGenerator)
        {
            this.credentialStore = credentialStore ?? throw new InvalidOperationException($"Must provide a {nameof(credentialStore)}");
            this.tokenGenerator = tokenGenerator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenGenerator)}");
        }

        public Task<AuthenticationResult> Authenticate(Credentials identifier)
        {
            return credentialStore
                .Get(identifier.Id)
                .ContinueWith(t => {
                    if(t.Result == null)
                    {
                        return AuthenticationResult.Failed;
                    }

                    return t.Result.Username == identifier.Username && t.Result.Password == identifier.Password ?
                        AuthenticationResult.Successful(tokenGenerator.Generate(identifier)) :
                        AuthenticationResult.Failed;
                });
        }
    }
}
