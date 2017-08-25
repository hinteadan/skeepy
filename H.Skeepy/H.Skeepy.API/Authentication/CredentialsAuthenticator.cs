using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class CredentialsAuthenticator
    {
        public bool Authenticate(string username, string password)
        {
            return username == "hintee" && password == "123qwe";
        }
    }
}
