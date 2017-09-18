using H.Skeepy.Core.Storage;
using H.Skeepy.Logging;
using H.Skeepy.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H.Skeepy.Azure.Storage
{
    public abstract class AzureTableStorage<T, TSkeepy> : ICanManageSkeepyStorageFor<TSkeepy>
        where T : TableEntity, new()
        where TSkeepy : IHaveId
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly CloudStorageAccount storageAccount;
        private readonly CloudTableClient tableStoreClient;
        private readonly CloudTable tablesStore;

        public AzureTableStorage(string connectionString, string collectionName)
        {
            storageAccount = CloudStorageAccount.Parse(connectionString);
            tableStoreClient = storageAccount.CreateCloudTableClient();
            tablesStore = tableStoreClient.GetTableReference(collectionName);
        }

        protected abstract T Map(TSkeepy model);
        protected abstract TSkeepy Map(T entry);
        protected abstract TSkeepy SummaryFor(string id);

        public async Task Put(TSkeepy model)
        {
            using (log.Timing($"Store {model.Id} into {tablesStore.Name}", NLog.LogLevel.Info))
            {
                await tablesStore.CreateIfNotExistsAsync();
                tablesStore.Execute(TableOperation.InsertOrReplace(Map(model)));
            }
        }

        public async Task Zap(string id)
        {
            using (log.Timing($"Remove {id} from {tablesStore.Name}", NLog.LogLevel.Info))
            {
                await tablesStore.ExecuteAsync(TableOperation.Delete(new T { RowKey = id, PartitionKey = id, ETag = "*" }));
            }
        }

        public async Task<bool> Any()
        {
            using (log.Timing($"Check for any entries in {tablesStore.Name}", NLog.LogLevel.Info))
            {
                if (!await tablesStore.ExistsAsync())
                {
                    return false;
                }

                return tablesStore.ExecuteQuery(new TableQuery<T>().Take(1))
                    .ToArray()
                    .Any();
            }
        }

        public async Task<IEnumerable<LazyEntity<TSkeepy>>> Get()
        {
            using (log.Timing($"Fetch all entries from {tablesStore.Name}", NLog.LogLevel.Info))
            {
                await tablesStore.CreateIfNotExistsAsync();

                return tablesStore
                    .CreateQuery<T>()
                    .Select(r => r.RowKey)
                    .ToArray()
                    .Select(id =>
                        new LazyEntity<TSkeepy>(SummaryFor(id), y => Map(tablesStore.Execute(TableOperation.Retrieve<T>(y.Id, y.Id)).Result as T))
                    );
            }
        }

        public async Task<TSkeepy> Get(string id)
        {
            using (log.Timing($"Fetch {id} from {tablesStore.Name}", NLog.LogLevel.Info))
            {
                await tablesStore.CreateIfNotExistsAsync();
                return Map(tablesStore.Execute(TableOperation.Retrieve<T>(id, id)).Result as T);
            }
        }

        public virtual void Dispose()
        {

        }
    }
}
