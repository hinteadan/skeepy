using System;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage.Individuals;

namespace H.Skeepy.Testicles.Core.Storage.Individuals
{
    [TestClass]
    public class InMemeoryIndividualsRepositoryOperations : IndividualsRepositoryOperations
    {
        public InMemeoryIndividualsRepositoryOperations()
            : base(new InMemoryIndividualsStore(), TimeSpan.FromMilliseconds(10))
        {
        }
    }
}
