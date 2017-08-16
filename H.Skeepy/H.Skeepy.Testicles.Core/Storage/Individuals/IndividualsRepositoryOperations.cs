using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage.Individuals;
using FluentAssertions;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using System.Linq;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class IndividualsRepositoryOperations
    {
        private IndividualsRepository repository;
        private readonly ICanManageSkeepyStorageFor<Individual> storage = new InMemoryIndividualsStore();

        [TestInitialize]
        public void Init()
        {
            repository = new IndividualsRepository(storage);
        }

        [TestCleanup]
        public void Uninit()
        {
            repository.Dispose();
        }

        [TestMethod]
        public void IndividualsRepository_CanGiveMeExistingData()
        {
            repository.All().Result.Should().BeEmpty();
            FakeData.FillStorage(storage, 10);
            repository.All().Result.Should().NotBeEmpty();
            repository.All().Result.ShouldAllBeEquivalentTo(storage.Get().Result.Select(x => x.Full));
        }

        [TestMethod]
        public void IndividualsRepository_GivesMeDataInReasonableTime()
        {
            FakeData.FillStorage(storage, 1000);
            repository.ExecutionTimeOf(x => x.All().Wait()).ShouldNotExceed(TimeSpan.FromMilliseconds(50));
        }
    }
}
