using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class PartyOperations
    {
        [TestMethod]
        public void Party_MustHaveAtLeastOneIndividual()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Party.New("Fed"));
        }

        [TestMethod]
        public void Party_MustHaveName()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Party.New(null, Individual.New("Fed")));
            Assert.ThrowsException<InvalidOperationException>(() => Party.New(string.Empty, Individual.New("Fed")));
            Assert.ThrowsException<InvalidOperationException>(() => Party.New("  \t", Individual.New("Fed")));
        }

        [TestMethod]
        public void ExistingParty_MustHaveId()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Party.Existing(null, "Fed", Individual.New("Fed")));
            Assert.ThrowsException<InvalidOperationException>(() => Party.Existing(string.Empty, "Fed", Individual.New("Fed")));
            Assert.ThrowsException<InvalidOperationException>(() => Party.Existing("  \t", "Fed", Individual.New("Fed")));

        }

        [TestMethod]
        public void Party_IndexesAnIndividualById()
        {
            var fed = Individual.Existing("fed", "Roger", "Federer");
            var party = Party.New("Federer", fed);
            party["fed"].Should().Be(fed);
            party["feddy"].Should().BeNull();
        }
    }
}
