using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryCredentialsAuthenticator : ICanAuthenticate<(string, string)>
    {
        private readonly ReadOnlyDictionary<string, string> users;

        public InMemoryCredentialsAuthenticator(params (string, string)[] users)
        {
            this.users = new ReadOnlyDictionary<string, string>(users.ToDictionary(x => x.Item1, x => x.Item2));
        }

        public AuthenticationResult Authenticate((string, string) identifier)
        {
            return Authenticate(identifier.Item1, identifier.Item2);
        }

        private AuthenticationResult Authenticate(string username, string password)
        {
            return users.ContainsKey(username) && users[username] == password ?
                AuthenticationResult.Successful :
                AuthenticationResult.Failed;
        }
    }
}
