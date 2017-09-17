using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.Azure.Storage.Model;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageRegistrationStore : AzureTableStorage<RegisteredUserTableEntity, RegisteredUser>
    {
        public AzureTableStorageRegistrationStore(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        { }

        public AzureTableStorageRegistrationStore(string connectionString)
            : this(connectionString, "SkeepyRegistrations")
        { }

        protected override RegisteredUserTableEntity Map(RegisteredUser model)
        {
            return new RegisteredUserTableEntity(model);
        }

        protected override RegisteredUser Map(RegisteredUserTableEntity entry)
        {
            return entry?.ToSkeepy();
        }

        protected override RegisteredUser SummaryFor(string id)
        {
            return new RegisteredUser { Email = id };
        }
    }
}
