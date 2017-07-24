using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using H.Skeepy.Core;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core
{
    [TestClass]
    public class MostBasicClashProcessorTesticles
    {
        private static readonly Party playerA = Party.New("Team A", Individual.New("Player", "A"));
        private static readonly Party playerB = Party.New("Team B", Individual.New("Player", "B"));
        private static readonly Party playerC = Party.New("Team C", Individual.New("Player", "C"));
        private static readonly Clash clash = Clash.New(playerA, playerB, playerC);
        private PointTracker pointTracker;
        private ICanProcessClashOutcome outcomeProcessor;
        private ICanProjectScore<int> scoreProjector;


        [TestInitialize]
        public void BeforeEachTest()
        {
            pointTracker = new PointTracker(clash);
            var processor = new MostBasicClashProcessor(clash, pointTracker);
            outcomeProcessor = processor;
            scoreProjector = processor;
        }

        [TestMethod]
        public void MostBasicClash_StartsInTie()
        {
            outcomeProcessor.ProcessOutcome().IsTie.Should().BeTrue();
            outcomeProcessor.ProcessOutcome().Winners.Should().BeEquivalentTo(playerA, playerB, playerC);
        }

        [TestMethod]
        public void MostBasicClash_IsWonByThePartiesWithMostPoints()
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

        [TestMethod]
        public void MostBasicClash_InitialScoreIsLoveAll()
        {
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 0), (playerB, 0), (playerC, 0));
        }

        [TestMethod]
        public void MostBasicClash_ScoreIsTrackedPointByPoint()
        {
            pointTracker.PointFor(playerA);
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 1), (playerB, 0), (playerC, 0));
            pointTracker.PointFor(playerB);
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 1), (playerB, 1), (playerC, 0));
            pointTracker.PointFor(playerC);
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 1), (playerB, 1), (playerC, 1));
            pointTracker.PointFor(playerA);
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 2), (playerB, 1), (playerC, 1));
            pointTracker.PointFor(playerA);
            pointTracker.PointFor(playerB);
            pointTracker.PointFor(playerB);
            pointTracker.PointFor(playerC);
            pointTracker.PointFor(playerA);
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 4), (playerB, 3), (playerC, 2));
            pointTracker.Undo();
            scoreProjector.ProjectScore().Scores.Should().BeEquivalentTo((playerA, 3), (playerB, 3), (playerC, 2));
        }
    }
}
