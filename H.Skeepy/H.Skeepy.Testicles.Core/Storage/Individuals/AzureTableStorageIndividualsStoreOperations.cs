using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Azure.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class AzureTableStorageIndividualsStoreOperations : IndividualsStoreOperations
    {

        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net";
        private static readonly string collectionName = "SkeepyIndividualsTests";
        private static CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        private static CloudTableClient tableStoreClient = storageAccount.CreateCloudTableClient();
        private static CloudTable tablesStore = tableStoreClient.GetTableReference(collectionName);

        public AzureTableStorageIndividualsStoreOperations()
            : base(() => new AzureTableStorageIndividualsStore(connectionString, collectionName))
        {
        }

        [ClassCleanup]
        public static void DoTheHousekeeping()
        {
            tablesStore.DeleteIfExists();
        }
    }
}
