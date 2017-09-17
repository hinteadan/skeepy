using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Azure.Storage.Model;

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
