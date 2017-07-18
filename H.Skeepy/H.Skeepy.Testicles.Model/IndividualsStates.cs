using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using System.Threading.Tasks;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class IndividualsStates
    {
        [TestMethod]
        public void Individual_MustHaveName()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(string.Empty, string.Empty));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New("   \t", "    \t"));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(string.Empty, null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, string.Empty));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(" \t", null));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.New(null, "\t   "));

            Individual.New("Roger", "Federer").FullName.Should().Be("Roger Federer");
            Individual.New("Roger", null).FullName.Should().Be("Roger");
            Individual.New(string.Empty, "Federer").FullName.Should().Be("Federer");
            Individual.New("Hintee").FullName.Should().Be("Hintee");
        }

        [TestMethod]
        public void ExisitngIndividual_MustHaveId()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Individual.Existing(null, "Fed"));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.Existing(string.Empty, "Fed"));
            Assert.ThrowsException<InvalidOperationException>(() => Individual.Existing("   \t", "Fed"));
        }
    }
}
