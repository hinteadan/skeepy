using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class InMemoryCredentialsAuthenticator
    {
        private readonly ReadOnlyDictionary<string, string> users;

        public InMemoryCredentialsAuthenticator(params (string, string)[] users)
        {
            this.users = new ReadOnlyDictionary<string, string>(users.ToDictionary(x => x.Item1, x => x.Item2));
        }

        public bool Authenticate(string username, string password)
        {
            return users.ContainsKey(username) && users[username] == password;
        }
    }
}
