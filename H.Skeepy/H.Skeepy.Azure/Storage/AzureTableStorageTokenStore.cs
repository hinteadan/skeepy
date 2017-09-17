using H.Skeepy.API.Authentication;
using H.Skeepy.Azure.Storage.Model;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageTokenStore : AzureTableStorage<TokenTableEntity, Token>
    {
        public AzureTableStorageTokenStore(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        { }

        public AzureTableStorageTokenStore(string connectionString)
            : this(connectionString, "SkeepyTokens")
        { }

        protected override TokenTableEntity Map(Token model)
        {
            return new TokenTableEntity(model);
        }

        protected override Token Map(TokenTableEntity entry)
        {
            return entry?.ToToken();
        }

        protected override Token SummaryFor(string id)
        {
            return new Token(null, null, id);
        }
    }
}
