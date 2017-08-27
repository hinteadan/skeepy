using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class GuidTokenGenerator : ICanGenerateTokens<Credentials>, ICanGenerateTokens<string>
    {
        public Token Generate(Credentials payload)
        {
            return Generate(payload.Id);
        }

        public Token Generate(string payload)
        {
            return new Token(payload, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
    }
}
