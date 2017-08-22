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
    public class AzureTableStorageIndividualsStore : ICanManageSkeepyStorageFor<Individual>
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableStore;

        public AzureTableStorageIndividualsStore(string connectionString)
        {
            storageAccount = CloudStorageAccount.Parse(connectionString);
            tableStore = storageAccount.CreateCloudTableClient();
        }

        public Task<bool> Any()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<Individual> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LazyEntity<Individual>>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Put(Individual model)
        {
            throw new NotImplementedException();
        }
    }
}
