using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public abstract class TokenAuthenticator : ICanAuthenticate<string>
    {
        private readonly ICanGetSkeepyEntity<Token> tokenStore;

        public TokenAuthenticator(ICanGetSkeepyEntity<Token> tokenStore)
        {
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public Task<AuthenticationResult> Authenticate(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
