using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryCredentialsAuthenticator : CredentialsAuthenticator
    {
        public InMemoryCredentialsAuthenticator(ICanGenerateTokens<Credentials> tokenGenerator, params Credentials[] users)
            : base(new InMemoryCredentialsStore(users), tokenGenerator)
        { }
        public InMemoryCredentialsAuthenticator(ICanGenerateTokens<Credentials> tokenGenerator, ICanGetSkeepyEntity<Credentials> credentialStore)
            : base(credentialStore, tokenGenerator)
        { }


        public InMemoryCredentialsAuthenticator(params Credentials[] users)
            : this(new GuidTokenGenerator(), users)
        { }
        public InMemoryCredentialsAuthenticator(ICanGetSkeepyEntity<Credentials> credentialStore)
            : this(new GuidTokenGenerator(), credentialStore)
        { }

    }
}
