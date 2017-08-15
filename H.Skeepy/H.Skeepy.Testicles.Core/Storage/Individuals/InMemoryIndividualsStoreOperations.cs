using System;
using H.Skeepy.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class InMemoryIndividualsStoreOperations : IndividualsStoreOperations
    {
        public InMemoryIndividualsStoreOperations() 
            : base(() => new InMemoryIndividualsStore())
        {
        }
    }
}
