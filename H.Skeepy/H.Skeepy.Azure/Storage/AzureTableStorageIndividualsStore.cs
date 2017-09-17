using H.Skeepy.Azure.Storage.Model;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage
{
    public class AzureTableStorageIndividualsStore : AzureTableStorage<IndividualTableEntity, Individual>
    {
        public AzureTableStorageIndividualsStore(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
        }

        public AzureTableStorageIndividualsStore(string connectionString)
            : this(connectionString, "SkeepyIndividuals")
        {
        }

        protected override IndividualTableEntity Map(Individual model)
        {
            if (model == null) throw new InvalidOperationException($"Must provide a {nameof(model)} to convert to Azure Table Entry");
            return new IndividualTableEntity(model);
        }

        protected override Individual Map(IndividualTableEntity entry)
        {
            return entry?.ToSkeepy();
        }

        protected override Individual SummaryFor(string id)
        {
            return Individual.Existing(id, id);
        }
    }
}
