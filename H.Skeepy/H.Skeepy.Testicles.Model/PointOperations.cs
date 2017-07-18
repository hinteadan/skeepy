using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class PointOperations
    {
        [TestMethod]
        public void Point_MustCountForParty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Point.NewFor(null));
        }

        [TestMethod]
        public void ExistingPoint_MustHaveId()
        {
            var party = Party.New("Fed", Individual.New("Fed"));
            Assert.ThrowsException<InvalidOperationException>(() => Point.Existing(null, DateTime.Now, party));
            Assert.ThrowsException<InvalidOperationException>(() => Point.Existing(string.Empty, DateTime.Now, party));
            Assert.ThrowsException<InvalidOperationException>(() => Point.Existing("  \t", DateTime.Now, party));
        }
    }
}
