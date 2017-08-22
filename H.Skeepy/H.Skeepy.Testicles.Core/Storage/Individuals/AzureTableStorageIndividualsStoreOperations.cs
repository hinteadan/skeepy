using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Azure.Storage;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class AzureTableStorageIndividualsStoreOperations : IndividualsStoreOperations
    {
        public AzureTableStorageIndividualsStoreOperations()
            : base(() => new AzureTableStorageIndividualsStore("dfsdf"))
        {
        }

        [TestInitialize]
        public override void Init()
        {
            base.Init();
        }

        [TestCleanup]
        public override void Uninit()
        {
            base.Uninit();
        }
    }
}
