using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Azure.Storage.Model;
using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
