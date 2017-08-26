using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class JsonWebTokenGenerator : ICanGenerateTokens<Credentials>
    {
        public string Generate(Credentials payload)
        {
            throw new NotImplementedException();
        }
    }
}
