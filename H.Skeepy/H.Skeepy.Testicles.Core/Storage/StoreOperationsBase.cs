using H.Skeepy.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Core.Storage
{
    public abstract class StoreOperationsBase<TSkeepy>
    {
        protected readonly Func<ICanManageSkeepyStorageFor<TSkeepy>> storeFactory;
        protected ICanManageSkeepyStorageFor<TSkeepy> store;

        public StoreOperationsBase(Func<ICanManageSkeepyStorageFor<TSkeepy>> storeFactory)
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
    }
}
