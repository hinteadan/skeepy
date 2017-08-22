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
            : base(() => new AzureTableStorageIndividualsStore("DefaultEndpointsProtocol=https;AccountName=hskeepydev;AccountKey=gdhovqMPlUxFcFyLG4G12NnRdceGKu0YNEE+EdX250GgTGBkXUTbttFpwoZk5KjuliFjE1OFJ/KtWtwLr7bw5g==;EndpointSuffix=core.windows.net"))
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
