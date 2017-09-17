using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using H.Skeepy.Azure.Storage;

namespace H.Skeepy.Testicles.Core.Storage.Registration
{
    [TestClass]
    public class AzureTokenStoreOperations : StoreOperationsBase<Token>
    {
        private static readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net";
        private static string collectionName = "SkeepyRegistrationTest";

        private static CloudTableClient tableStoreClient = CloudStorageAccount.Parse(connectionString).CreateCloudTableClient();

        public AzureTokenStoreOperations() 
            : base(() => new AzureTableStorageTokenStore())
        {
        }

        protected override Token CreateModel()
        {
            throw new NotImplementedException();
        }
    }
}
