using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Azure.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class AzureTableStorageIndividualsRepositoryOperations : IndividualsRepositoryOperations
    {
        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net";
        private static string collectionName = "SkeepyIndividualsTest";

        private static CloudTableClient tableStoreClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();

        public AzureTableStorageIndividualsRepositoryOperations()
            : base(() => new AzureTableStorageIndividualsStore(connectionString, collectionName), TimeSpan.FromSeconds(0.7), 50, 10)
        {
        }

        [TestInitialize]
        public override void Init()
        {
            collectionName = $"SkeepyIndividualsTest{Guid.NewGuid()}".Replace("-", string.Empty);
            base.Init();
        }

        [TestCleanup]
        public override void Uninit()
        {
            base.Uninit();
            tableStoreClient.GetTableReference(collectionName).DeleteIfExists();
        }
    }
}
