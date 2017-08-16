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
        protected IndividualsRepository repository;
        protected readonly Func<ICanManageSkeepyStorageFor<Individual>> storeFactory;
        protected ICanManageSkeepyStorageFor<Individual> storage;
        protected readonly TimeSpan fetchExecutionTimeLimit;

        public IndividualsRepositoryOperations(Func<ICanManageSkeepyStorageFor<Individual>> storeFactory, TimeSpan fetchExecutionTimeLimit)
        {
            this.storeFactory = storeFactory;
            this.fetchExecutionTimeLimit = fetchExecutionTimeLimit;
        }

        [TestInitialize]
        public virtual void Init()
        {
            storage = storeFactory();
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
            FakeData.FillStorage(storage, 1000);
            repository.All(100).Result.ShouldAllBeEquivalentTo(storage.Get().Result.Take(100).Select(x => x.Full));
            repository.All(100, 100).Result.ShouldAllBeEquivalentTo(storage.Get().Result.Skip(100).Take(100).Select(x => x.Full));
        }

        [TestMethod]
        public void IndividualsRepository_ChunkRetrievalDoesntAffectExecutionTime()
        {
            FakeData.FillStorage(storage, 1000);
            repository.ExecutionTimeOf(x => x.All(100).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
            repository.ExecutionTimeOf(x => x.All(100, 500).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
            repository.ExecutionTimeOf(x => x.All(100, 900).Wait()).ShouldNotExceed(fetchExecutionTimeLimit);
        }
    }
}
