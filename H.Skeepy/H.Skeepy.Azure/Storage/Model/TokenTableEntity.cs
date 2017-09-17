using H.Skeepy.API.Authentication;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage.Model
{
    public class TokenTableEntity : TableEntity
    {
        public TokenTableEntity()
        {
        }

        public TokenTableEntity(Token token)
        {
            RowKey = token.Public;
            PartitionKey = token.Public;

            Public = token.Public;
            Secret = token.Secret;
            UserId = token.UserId;
            ValidUntil = new DateTimeOffset(token.ValidUntil).Ticks;
            GeneratedAt = new DateTimeOffset(token.GeneratedAt).Ticks;
        }

        public string Public { get; set; }
        public string Secret { get; set; }
        public string UserId { get; set; }
        public long ValidUntil { get; set; }
        public long GeneratedAt { get; set; }

        public Token ToToken()
        {
            return new Token(UserId, Secret, Public, new DateTime(ValidUntil, DateTimeKind.Utc), new DateTime(GeneratedAt, DateTimeKind.Utc));
        }

    }
}
