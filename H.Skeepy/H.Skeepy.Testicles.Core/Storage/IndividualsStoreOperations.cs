using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage;
using FluentAssertions;
using H.Skeepy.Model;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Core.Storage
{
    [TestClass]
    public class IndividualsStoreOperations
    {
        private InMemoryIndividualsStore store;

        [TestInitialize]
        public void Init()
        {
            store = new InMemoryIndividualsStore();
        }

        [TestCleanup]
        public void Uninit()
        {
            store.Dispose();
        }

        [TestMethod]
        public void IndividualsStore_IsEmptyByDefault()
        {
            store.Any().Should().BeFalse();
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
            store.Any().Should().BeTrue();
        }

        [TestMethod]
        public void IndividualsStore_CanIterateThroughStoredIndividuals()
        {
            var fed = Individual.New("Fed");
            var rafa = Individual.New("Rafa");
            var stan = Individual.New("Stan");
            Task.WaitAll(store.Put(fed), store.Put(rafa), store.Put(stan));
            store.Get().Result.ShouldAllBeEquivalentTo(new Individual[] { fed, rafa, stan });
        }
    }
}
