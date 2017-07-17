using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using System.Threading.Tasks;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class IndividualsStates
    {
        [TestMethod]
        public void IndividualsMustHaveNames()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(string.Empty, string.Empty));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New("   \t", "    \t"));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(string.Empty, null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, string.Empty));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(" \t", null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, "\t   "));

            Assert.AreEqual(Individual.New("Roger", "Federer").FullName, "Roger Federer");
            Assert.AreEqual(Individual.New("Roger", null).FullName, "Roger");
            Assert.AreEqual(Individual.New(string.Empty, "Federer").FullName, "Federer");
            Assert.AreEqual(Individual.New("Hintee").FullName, "Hintee");
        }
    }
}
