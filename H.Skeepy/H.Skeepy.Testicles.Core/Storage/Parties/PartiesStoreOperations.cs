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

        protected override Party CreateModel()
        {
            return FakeData.GenerateParty();
        }

        [TestMethod]
        public void PartyStorage_Individuals_Are_Decoupled()
        {
            var fed = FakeData.GenerateIndividual();
            var rafa = FakeData.GenerateIndividual();
            var party = Party.New("Fedal", fed, rafa);

            (store as IDependOn<Individual>).WithDependency(id => party[id]);

            store.Put(party).Wait();

            store.Get(party.Id).Result.Members.Should().BeEquivalentTo(fed, rafa);
        }
    }
}
