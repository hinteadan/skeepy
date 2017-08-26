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
        private readonly TimeSpan validitySpan = TimeSpan.MaxValue;

        private readonly IJwtEncoder jwtEncoder = new JwtEncoder(
            new HMACSHA256Algorithm(),
            new JsonNetSerializer(),
            new JwtBase64UrlEncoder());

        public JsonWebTokenGenerator(TimeSpan validitySpan)
        {
            this.validitySpan = validitySpan;
        }

        public JsonWebTokenGenerator()
            : this(TimeSpan.MaxValue) { }

        public Token Generate(Credentials payload)
        {
            var secret = GenerateSecretKey();
            return validitySpan == TimeSpan.MaxValue ?
                new Token(secret, jwtEncoder.Encode(new { userId = payload.Id }, secret)) :
                new Token(secret, jwtEncoder.Encode(new { userId = payload.Id, exp = CalculateUnixTimeValidity() }, secret), DateTime.Now + validitySpan);
        }

        private int CalculateUnixTimeValidity()
        {
            return (int)((DateTime.UtcNow + validitySpan).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        }

        private string GenerateSecretKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
