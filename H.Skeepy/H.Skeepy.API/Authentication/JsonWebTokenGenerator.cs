using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class JsonWebTokenGenerator : ICanGenerateTokens<Credentials>
    {
        private readonly IJwtEncoder jwtEncoder = new JwtEncoder(
            new HMACSHA256Algorithm(),
            new JsonNetSerializer(),
            new JwtBase64UrlEncoder());

        public Token Generate(Credentials payload)
        {
            var secret = GenerateSecretKey();
            return new Token(secret, jwtEncoder.Encode(payload, secret));
        }

        private string GenerateSecretKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
