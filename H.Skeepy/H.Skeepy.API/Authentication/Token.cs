using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class Token : IHaveId
    {
        public readonly string Public;
        public readonly string Secret;
        public readonly string UserId;
        public readonly DateTime ValidUntil;
        public readonly DateTime GeneratedAt;

        public bool HasExpired()
        {
            return DateTime.Now >= ValidUntil;
        }

        public string Id => Public;

        public Token(string userId, string secretToken, string publicToken, DateTime validUntil, DateTime generatedAt)
        {
            Public = publicToken;
            Secret = secretToken;
            UserId = userId;
            ValidUntil = validUntil;
            GeneratedAt = generatedAt;
        }

        public Token(string userId, string secretToken, string publicToken, DateTime validUntil)
             : this(userId, secretToken, publicToken, validUntil, DateTime.Now) { }

        public Token(string userId, string secretToken, string publicToken)
             : this(userId, secretToken, publicToken, DateTime.MaxValue, DateTime.Now) { }
    }
}
