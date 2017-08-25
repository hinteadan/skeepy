using FluentAssertions;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Core.Storage
{
    public abstract class StoreOperationsBase<TSkeepy> where TSkeepy : IHaveId
    {
        protected readonly Func<ICanManageSkeepyStorageFor<TSkeepy>> storeFactory;
        protected ICanManageSkeepyStorageFor<TSkeepy> store;

        public StoreOperationsBase(Func<ICanManageSkeepyStorageFor<TSkeepy>> storeFactory)
        {
            this.storeFactory = storeFactory;
        }

        protected abstract TSkeepy CreateModel();

        [TestInitialize]
        public virtual void Init()
        {
            store = storeFactory();
        }

        [TestCleanup]
        public virtual void Uninit()
        {
            store?.Dispose();
        }

        [TestMethod]
        public void SkeepyStore_IsEmptyByDefault()
        {
            store.Any().Result.Should().BeFalse();
        }

        [TestMethod]
        public void SkeepyStore_ReturnsNullForInexsitentEntities()
        {
            store.Get("InexistentId").Result.Should().BeNull();
        }

        [TestMethod]
        public void SkeepyStore_CanSaveAndRetrieveAnEntity()
        {
            var fed = CreateModel();

            store.Put(fed).Wait();
            store.Get(fed.Id).Result.ShouldBeEquivalentTo(fed);
        }

        [TestMethod]
        public void SkeepyStore_IsNotEmpty_AfterStoringSomeData()
        {
            store.Put(CreateModel()).Wait();
            store.Any().Result.Should().BeTrue();
        }

        [TestMethod]
        public void SkeepyStore_CanIterateThroughStoredEntities()
        {
            var fed = CreateModel();
            var rafa = CreateModel();
            var stan = CreateModel();
            Task.WaitAll(store.Put(fed), store.Put(rafa), store.Put(stan));
            store.Get().Result.Select(x => x.Summary.Id).Should().BeEquivalentTo(fed.Id, rafa.Id, stan.Id);
            store.Get().Result.Select(x => x.Full).ShouldAllBeEquivalentTo(new TSkeepy[] { fed, rafa, stan });
        }

        [TestMethod]
        public void SkeepyStore_CanDeleteEntity()
        {
            var fed = CreateModel();
            store.Put(fed).Wait();
            store.Zap(fed.Id).Wait();
            store.Get(fed.Id).Result.Should().BeNull();
        }
    }
}
