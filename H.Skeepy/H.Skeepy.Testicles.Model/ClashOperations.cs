using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class ClashOperations
    {
        [TestMethod]
        public void Clash_MustHaveAtLeastOneParty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Clash<Point>.New());
        }

        [TestMethod]
        public void ExistingClash_MustHaveId()
        {
            var party = Party.New("Fed", Individual.New("Fed"));
            Assert.ThrowsException<InvalidOperationException>(() => Clash<Point>.Existing(null, party));
            Assert.ThrowsException<InvalidOperationException>(() => Clash<Point>.Existing(string.Empty, party));
            Assert.ThrowsException<InvalidOperationException>(() => Clash<Point>.Existing("  \t", party));

        }
    }
}
