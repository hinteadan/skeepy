using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;

namespace H.Skeepy.Testicles.Core.Storage.Parties
{
    [TestClass]
    public class PartiesStoreOperations
    {
        private ICanManageSkeepyStorageFor<Party> store;

        [TestMethod]
        public void PartieStore_IsEmptyByDefault()
        {

        }
    }
}
