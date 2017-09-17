using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryTokenAuthenticator : TokenAuthenticator
    {
        public InMemoryTokenAuthenticator(params Token[] validTokens)
            : base(new InMemoryTokensStore(validTokens))
        {
        }

        public InMemoryTokenAuthenticator(ICanGetSkeepyEntity<Token> store)
            : base(store)
        { }
    }
}
