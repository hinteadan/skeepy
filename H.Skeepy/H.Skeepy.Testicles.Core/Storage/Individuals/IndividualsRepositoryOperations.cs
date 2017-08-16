using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage.Individuals;
using FluentAssertions;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;

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
            FakeData.FillStorage(storage);
            repository.All().Result.Should().NotBeEmpty();
        }
    }
}
