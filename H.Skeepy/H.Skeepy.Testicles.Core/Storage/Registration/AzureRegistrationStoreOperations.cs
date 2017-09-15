using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Azure.Storage;

namespace H.Skeepy.Testicles.Core.Storage.Registration
{
    [TestClass]
    public class AzureRegistrationStoreOperations : StoreOperationsBase<RegisteredUser>
    {
        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net";
        private static string collectionName = "SkeepyRegistrationTest";

        private static CloudTableClient tableStoreClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();

        public AzureRegistrationStoreOperations() : base(() => new AzureTableStorageRegistrationStore())
        {
        }

        [TestInitialize]
        public override void Init()
        {
            collectionName = $"SkeepyRegistrationTest{Guid.NewGuid()}".Replace("-", string.Empty);
            base.Init();
        }

        [TestCleanup]
        public override void Uninit()
        {
            base.Uninit();
            tableStoreClient.GetTableReference(collectionName).DeleteIfExists();
        }

        protected override RegisteredUser CreateModel()
        {
            return FakeData.GenerateRegisteredUser();
        }
    }
}
