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

        public bool HasExpired()
        {
            return DateTime.Now >= ValidUntil;
        }

        public string Id => Public;

        public Token(string userId, string secretToken, string publicToken, DateTime validUntil)
        {
            Public = publicToken;
            Secret = secretToken;
            UserId = userId;
            ValidUntil = validUntil;
        }

        public Token(string userId, string secretToken, string publicToken)
             : this(userId, secretToken, publicToken, DateTime.MaxValue) { }
    }
}
