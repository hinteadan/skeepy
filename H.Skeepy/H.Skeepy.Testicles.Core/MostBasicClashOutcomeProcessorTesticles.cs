using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using H.Skeepy.Core;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core
{
    [TestClass]
    public class MostBasicClashOutcomeProcessorTesticles
    {
        private static readonly Party playerA = Party.New("Team A", Individual.New("Player", "A"));
        private static readonly Party playerB = Party.New("Team B", Individual.New("Player", "B"));
        private static readonly Party playerC = Party.New("Team C", Individual.New("Player", "C"));
        private static readonly Clash clash = Clash.New(playerA, playerB, playerC);
        private PointTracker pointTracker;
        private ICanProcessClashOutcome outcomeProcessor;


        [TestInitialize]
        public void BeforeEachTest()
        {
            pointTracker = new PointTracker(clash);
            var processor = new MostBasicClashOutcomeProcessor(clash, pointTracker);
            outcomeProcessor = processor;
        }

        [TestMethod]
        public void MostBasicClashOutcome_StartsInTie()
        {
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeTrue();
            outcomeProcessor.ProcessOutcome().Winners.Should().BeEquivalentTo(playerA, playerB, playerC);
        }

        [TestMethod]
        public void MostBasicClashOutcome_IsWonByThePartiesWithMostPoints()
        {
            pointTracker.PointFor(playerA);
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeFalse();
            outcomeProcessor.ProcessOutcome().Winner.Should().Be(playerA);

            pointTracker.PointFor(playerB);
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeFalse();
            outcomeProcessor.ProcessOutcome().Winners.Should().BeEquivalentTo(playerA, playerB);

            pointTracker.PointFor(playerC);
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeTrue();
            outcomeProcessor.ProcessOutcome().Winners.Should().BeEquivalentTo(playerA, playerB, playerC);

            pointTracker.PointFor(playerA);
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeFalse();
            outcomeProcessor.ProcessOutcome().Winner.Should().Be(playerA);
        }
    }
}
