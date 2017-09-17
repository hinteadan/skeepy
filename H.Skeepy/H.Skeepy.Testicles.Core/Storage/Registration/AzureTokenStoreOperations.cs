using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using H.Skeepy.Azure.Storage;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.Testicles.Core.Storage.Registration
{
    [TestClass]
    public class AzureTokenStoreOperations : AzureStoreOperationsBase<Token>
    {
        public AzureTokenStoreOperations()
            : base("SkeepyTokensTest", collection => new AzureTableStorageTokenStore(ConnectionString, collection))
        {
        }

        protected override Token CreateModel()
        {
            return new JsonWebTokenGenerator(TimeSpan.FromHours(2)).Generate("hintee@skeepy.ro");
        }
    }
}
