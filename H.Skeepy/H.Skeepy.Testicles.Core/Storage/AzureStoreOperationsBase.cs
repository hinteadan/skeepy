using H.Skeepy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H.Skeepy.Core.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

namespace H.Skeepy.Testicles.Core.Storage
{
    public abstract class AzureStoreOperationsBase<TSkeepy> : StoreOperationsBase<TSkeepy>, IDisposable where TSkeepy : IHaveId
    {
        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net";
        private readonly string collectionNamePrefix;
        private static ConcurrentStack<string> collectionNamesStack = new ConcurrentStack<string>();
        private static CloudTableClient tableStoreClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();

        protected static string ConnectionString { get; } = connectionString;

        public AzureStoreOperationsBase(string collectionNamePrefix, Func<string, ICanManageSkeepyStorageFor<TSkeepy>> storeFactory)
            : base(() =>
            {
                if (!collectionNamesStack.TryPeek(out var collection))
                {
                    throw new InvalidOperationException("Azure storage not initialized");
                }
                return storeFactory(collection);
            })
        {
            this.collectionNamePrefix = collectionNamePrefix;
        }

        [TestInitialize]
        public override void Init()
        {
            collectionNamesStack.Push($"{collectionNamePrefix}{Guid.NewGuid()}".Replace("-", string.Empty));
            base.Init();
        }

        [TestCleanup]
        public override void Uninit()
        {
            base.Uninit();
        }

        public void Dispose()
        {
            while(collectionNamesStack.TryPop(out string collection))
            {
                tableStoreClient.GetTableReference(collection).DeleteIfExists();
            }
        }
    }
}
