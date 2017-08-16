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

        protected IDependOn<Individual> IndividualsDependency
        {
            get
            {
                return (IDependOn<Individual>)store;
            }
        }

        protected override Party CreateModel()
        {
            return FakeData.GenerateParty();
        }

        [TestMethod]
        public void PartyStorage_Individuals_CanBe_Decoupled()
        {
            var fed = FakeData.GenerateIndividual();
            var rafa = FakeData.GenerateIndividual();
            var party = Party.New("Fedal", fed, rafa);

            IndividualsDependency.WithDependency(id => party[id]);

            store.Put(party).Wait();

            store.Get(party.Id).Result.Members.Should().BeEquivalentTo(fed, rafa);
        }
    }
}
