using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication.Storage;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryCredentialsAuthenticator : CredentialsAuthenticator
    {
        public InMemoryCredentialsAuthenticator(ICanGenerateTokens tokenGenerator, params Credentials[] users)
            : base(new InMemoryCredentialsStore(users), tokenGenerator)
        { }

        public InMemoryCredentialsAuthenticator(params Credentials[] users)
            : this(new GuidTokenGenerator(), users)
        { }
    }
}
