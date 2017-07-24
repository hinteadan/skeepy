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
        private MostBasicClashOutcomeProcessor outcomeProcessor;


        [TestInitialize]
        public void BeforeEachTest()
        {
            pointTracker = new PointTracker(clash);
            outcomeProcessor = new MostBasicClashOutcomeProcessor(clash, pointTracker);
        }

        [TestMethod]
        public void MostBasicClashOutcome_StartsInTie()
        {
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeTrue();
        }
    }
}
