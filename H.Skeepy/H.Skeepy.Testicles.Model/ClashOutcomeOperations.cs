using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class ClashOutcomeOperations
    {
        private static readonly Clash clash = Clash.New(Party.New("Fed", Individual.New("Fed")), Party.New("Rafa", Individual.New("Rafa")));

        [TestMethod]
        public void ClashOutcome_MustHaveAnUnderlyingClash()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new ClashOutcome(null));
        }

        [TestMethod]
        public void ClashOutcome_CanBeWonByOneParticipatingParty()
        {
            var outcome = new ClashOutcome(clash);
            outcome.HasWinner.Should().Be(true);
        }
    }
}
