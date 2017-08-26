using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class Token
    {
        public readonly string Public;
        public readonly string Secret;

        public Token(string secretToken, string publicToken)
        {
            Public = publicToken;
            Secret = secretToken;
        }
    }
}
