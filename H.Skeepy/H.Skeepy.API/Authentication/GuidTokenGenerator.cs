using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class GuidTokenGenerator : ICanGenerateTokens<Credentials>
    {
        public string Generate(Credentials payload)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
