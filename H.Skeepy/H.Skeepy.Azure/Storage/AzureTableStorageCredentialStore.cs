using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Azure.Storage.Model;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageCredentialStore : AzureTableStorage<CredentialsTableEntity, Credentials>
    {
        public AzureTableStorageCredentialStore(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        { }

        public AzureTableStorageCredentialStore(string connectionString)
            : this(connectionString, "SkeepyCredentials")
        { }

        protected override CredentialsTableEntity Map(Credentials model)
        {
            return new CredentialsTableEntity(model);
        }

        protected override Credentials Map(CredentialsTableEntity entry)
        {
            return entry?.ToSkeepy();
        }

        protected override Credentials SummaryFor(string id)
        {
            return new Credentials(id, null);
        }
    }
}
