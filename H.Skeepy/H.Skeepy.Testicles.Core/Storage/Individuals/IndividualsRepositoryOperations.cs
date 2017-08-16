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
    public abstract class IndividualsRepositoryOperations
    {
        private IndividualsRepository repository;
        private readonly ICanManageSkeepyStorageFor<Individual> storage;
        private readonly TimeSpan fetchExecutionTimeLimit;

        public IndividualsRepositoryOperations(ICanManageSkeepyStorageFor<Individual> storage, TimeSpan fetchExecutionTimeLimit)
        {
            this.storage = storage;
            this.fetchExecutionTimeLimit = fetchExecutionTimeLimit;
        }

        [TestInitialize]
        public virtual void Init()
        {
            repository = new IndividualsRepository(storage);
        }

        [TestCleanup]
        public virtual void Uninit()
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
            repository.ExecutionTimeOf(x => x.All().Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
        }

        [TestMethod]
        public void IndividualsRepository_CanGiveMeChunks()
        {
            FakeData.FillStorage(storage, 10000);
            repository.All(1000).Result.ShouldAllBeEquivalentTo(storage.Get().Result.Take(1000).Select(x => x.Full));
            repository.All(1000, 1000).Result.ShouldAllBeEquivalentTo(storage.Get().Result.Skip(1000).Take(1000).Select(x => x.Full));
        }

        [TestMethod]
        public void IndividualsRepository_ChunkRetrievalDoesntAffectExecutionTime()
        {
            FakeData.FillStorage(storage, 10000);
            repository.ExecutionTimeOf(x => x.All(1000).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
            repository.ExecutionTimeOf(x => x.All(1000, 5000).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
            repository.ExecutionTimeOf(x => x.All(1000, 9000).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
        }
    }
}
