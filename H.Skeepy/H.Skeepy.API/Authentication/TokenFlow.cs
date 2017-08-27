using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public sealed class TokenFlow
    {
        private readonly TokenAuthenticator tokenAuthenticator;
        private readonly ICanStoreSkeepy<Token> tokenStore;

        public TokenFlow(TokenAuthenticator tokenAuthenticator, ICanStoreSkeepy<Token> tokenStore)
        {
            this.tokenAuthenticator = tokenAuthenticator ?? throw new InvalidOperationException($"Must provide a {nameof(tokenAuthenticator)}");
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }
    }
}
