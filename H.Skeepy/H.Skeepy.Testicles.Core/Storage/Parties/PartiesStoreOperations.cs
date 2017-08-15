using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core.Storage.Parties
{
    public abstract class PartiesStoreOperations : StoreOperationsBase<Party>
    {
        public PartiesStoreOperations(Func<ICanManageSkeepyStorageFor<Party>> storeFactory)
            : base(storeFactory)
        {
        }

        [TestMethod]
        public void PartieStore_IsEmptyByDefault()
        {
            store.Any().Result.Should().BeFalse();
        }
    }
}
