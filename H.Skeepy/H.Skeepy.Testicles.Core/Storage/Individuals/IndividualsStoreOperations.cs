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
    public abstract class IndividualsStoreOperations : StoreOperationsBase<Individual>
    {
        public IndividualsStoreOperations(Func<ICanManageSkeepyStorageFor<Individual>> storeFactory)
            : base(storeFactory)
        { }

        protected override Individual CreateModel()
        {
            return FakeData.GenerateIndividual();
        }
    }
}
