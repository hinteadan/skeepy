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
            Assert.ThrowsException<InvalidOperationException>(() => new Party());
        }

        [TestMethod]
        public void Party_IndexesAnIndividualById()
        {
            var fed = Individual.Existing("fed", "Roger", "Federer");
            var party = new Party(fed);
            party["fed"].Should().Be(fed);
            party["feddy"].Should().BeNull();
        }
    }
}
