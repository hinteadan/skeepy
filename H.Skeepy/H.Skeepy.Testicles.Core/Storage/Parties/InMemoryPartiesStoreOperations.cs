using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage.Parties;

namespace H.Skeepy.Testicles.Core.Storage.Parties
{
    [TestClass]
    public class InMemoryPartiesStoreOperations : PartiesStoreOperations
    {
        public InMemoryPartiesStoreOperations() 
            : base(() => new InMemoryPartiesStore())
        {
        }
    }
}
