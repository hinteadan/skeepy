using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class ClashOperations
    {
        [TestMethod]
        public void Clash_MustHaveAtLeastOneParty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Clash.New());
        }

        [TestMethod]
        public void ExistingClash_MustHaveId()
        {
            var party = Party.New("Fed", Individual.New("Fed"));
            Assert.ThrowsException<InvalidOperationException>(() => Clash.Existing(null, party));
            Assert.ThrowsException<InvalidOperationException>(() => Clash.Existing(string.Empty, party));
            Assert.ThrowsException<InvalidOperationException>(() => Clash.Existing("  \t", party));
        }

        [TestMethod]
        public void Clash_GetsPartyById()
        {
            var fed = Party.New("Fed", Individual.New("Fed"));
            var clash = Clash.New(fed);

            clash.Participant(fed.Id).Should().Be(fed);
            clash.Participant(string.Empty).Should().BeNull();
            clash.Participant(null).Should().BeNull();
            clash.Participant("Rafa").Should().BeNull();
        }
    }
}
