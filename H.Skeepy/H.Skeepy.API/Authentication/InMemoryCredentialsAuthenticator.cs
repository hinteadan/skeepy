using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryCredentialsAuthenticator : ICanAuthenticate<InMemoryCredentialsAuthenticator.Credentials>
    {
        public class Credentials
        {
            public readonly string Username;
            public readonly string Password;

            public Credentials(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }

        private readonly ICanGenerateTokens tokenGenerator;
        private readonly ReadOnlyDictionary<string, string> users;

        public InMemoryCredentialsAuthenticator(ICanGenerateTokens tokenGenerator, params Credentials[] users)
        {
            this.tokenGenerator = tokenGenerator;
            this.users = new ReadOnlyDictionary<string, string>(users.ToDictionary(x => x.Username, x => x.Password));
        }
        public InMemoryCredentialsAuthenticator(params Credentials[] users)
            : this(new GuidTokenGenerator(), users)
        { }


        public AuthenticationResult Authenticate(Credentials identifier)
        {
            return Authenticate(identifier.Username, identifier.Password);
        }

        private AuthenticationResult Authenticate(string username, string password)
        {
            return users.ContainsKey(username) && users[username] == password ?
                AuthenticationResult.Successful(tokenGenerator.Generate()) :
                AuthenticationResult.Failed;
        }


    }
}
