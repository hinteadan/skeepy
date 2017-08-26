using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public abstract class TokenAuthenticator : ICanAuthenticate<string>
    {
        public Task<AuthenticationResult> Authenticate(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
