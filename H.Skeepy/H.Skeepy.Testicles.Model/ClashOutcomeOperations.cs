using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class ClashOutcomeOperations : DetailsHolder
    {
        private static readonly Party fed = Party.New("Fed", Individual.New("Fed"));
        private static readonly Party rafa = Party.New("Rafa", Individual.New("Rafa"));
        private static readonly Clash clash = Clash.New(fed, rafa);

        [TestMethod]
        public void ClashOutcome_MustHaveAnUnderlyingClash()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new ClashOutcome(null));
        }

        [TestMethod]
        public void ClashOutcome_WinnerPartyMustBePartOfTheClash()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new ClashOutcome(clash).WonBy(Party.New("Wawrinka", Individual.New("Stan"))));
        }

        [TestMethod]
        public void ClashOutcome_CanBeWonByOneParticipatingParty()
        {
            var outcome = new ClashOutcome(clash).WonBy(fed);
            outcome.HasWinner.Should().BeTrue();
            outcome.Winner.Should().Be(fed);
        }

        [TestMethod]
        public void ClashOutcome_CanHaveMultipleWinners()
        {
            var outcome = new ClashOutcome(clash).WonBy(fed).WonBy(rafa);
            outcome.HasWinner.Should().BeTrue();
            Assert.ThrowsException<InvalidOperationException>(() => outcome.Winner);
            outcome.Winners.Should().BeEquivalentTo(fed, rafa);
        }
    }
}
