using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class GuidTokenGenerator : ICanGenerateTokens<Credentials>
    {
        public Token Generate(Credentials payload)
        {
            return new Token(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}
