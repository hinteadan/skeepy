using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using H.Skeepy.Azure.Storage;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.Testicles.Core.Storage.Registration
{
    [TestClass]
    public class AzureCredentialStoreOperations : AzureStoreOperationsBase<Credentials>
    {
        public AzureCredentialStoreOperations()
            : base("SkeepyCredentialsTest", collection => new AzureTableStorageCredentialStore(ConnectionString, collection))
        { }

        protected override Credentials CreateModel()
        {
            return FakeData.GenerateCredentials();
        }
    }
}
