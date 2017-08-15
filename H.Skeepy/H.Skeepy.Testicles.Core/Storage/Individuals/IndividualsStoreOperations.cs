using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage;
using FluentAssertions;
using H.Skeepy.Model;
using System.Threading.Tasks;
using System.Linq;
using H.Skeepy.Core.Storage.Individuals;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public abstract class IndividualsStoreOperations
    {
        private readonly Func<ICanManageSkeepyStorageFor<Individual>> storeFactory;
        private ICanManageSkeepyStorageFor<Individual> store;

        public IndividualsStoreOperations(Func<ICanManageSkeepyStorageFor<Individual>> storeFactory)
        {
            this.storeFactory = storeFactory;
        }

        [TestInitialize]
        public virtual void Init()
        {
            store = storeFactory();
        }

        [TestCleanup]
        public virtual void Uninit()
        {
            store.Dispose();
        }

        [TestMethod]
        public void IndividualsStore_IsEmptyByDefault()
        {
            store.Any().Result.Should().BeFalse();
        }

        [TestMethod]
        public void IndividualsStore_CanSaveAndRetrieveAnIndividual()
        {
            var fed = Individual.New("Roger", "Federer");
            fed.SetDetail("Rank", "1").SetDetail("Country", "SWZ");

            store.Put(fed).Wait();
            store.Get(fed.Id).Result.ShouldBeEquivalentTo(fed);
        }

        [TestMethod]
        public void IndividualsStore_IsNotEmpty_AfterStoringSomeData()
        {
            store.Put(Individual.New("Fed")).Wait();
            store.Any().Result.Should().BeTrue();
        }

        [TestMethod]
        public void IndividualsStore_CanIterateThroughStoredIndividuals()
        {
            var fed = Individual.New("Fed");
            var rafa = Individual.New("Rafa");
            var stan = Individual.New("Stan");
            Task.WaitAll(store.Put(fed), store.Put(rafa), store.Put(stan));
            store.Get().Result.Select(x => x.Summary.Id).Should().BeEquivalentTo(fed.Id, rafa.Id, stan.Id);
            store.Get().Result.Select(x => x.Full).ShouldAllBeEquivalentTo(new Individual[] { fed, rafa, stan });
        }
    }
}
