﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using H.Skeepy.Core;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core
{
    [TestClass]
    public class PointTrackerScenarios
    {
        private PointTracker useCase;
        private Party fed = Party.New("Fed", Individual.New("Fed"));
        private Party rafa = Party.New("Rafa", Individual.New("Rafa"));
        private Clash clash;

        public PointTrackerScenarios()
        {
            clash = Clash.New(fed, rafa);
        }

        [TestInitialize]
        public void Init()
        {
            useCase = new PointTracker(clash);
        }

        [TestMethod]
        public void PointTracker_CanTrackPointsOnlyForMemberParties()
        {
            var wawrinka = Party.New("Stan", Individual.New("The Man"));
            Assert.ThrowsException<InvalidOperationException>(() => useCase.PointFor(wawrinka));
        }

        [TestMethod]
        public void PointTracker_TracksPoints()
        {
            var a = useCase.PointFor(fed);
            var b = useCase.PointFor(rafa);
            useCase.Points.Should().Equal(b, a);
        }

        [TestMethod]
        public void PointTracker_CanGiveMeThePointsForSpecificParty()
        {
            var wawrinka = Party.New("Stan", Individual.New("The Man"));
            Assert.ThrowsException<InvalidOperationException>(() => useCase.PointsOf(wawrinka));

            var fedPoint = useCase.PointFor(fed);
            var rafaPoint = useCase.PointFor(rafa);

            useCase.PointsOf(fed).Should().Equal(fedPoint);
            useCase.PointsOf(rafa).Should().Equal(rafaPoint);
        }

        [TestMethod]
        public void PointTracker_CanUndoLastPoint()
        {
            var a = useCase.PointFor(fed);
            var b = useCase.PointFor(rafa);

            useCase.Undo();

            useCase.Points.Should().Equal(a);
            useCase.PointsOf(rafa).Should().BeEmpty();
            useCase.PointsOf(fed).Should().Equal(a);
        }

        [TestMethod]
        public void PointTracker_UndoLastPointDoesNothingWhenThereAreNoPoints()
        {
            useCase.Undo();
            useCase.PointFor(fed);
            useCase.PointFor(fed);
            useCase.Undo().Undo().Undo().Undo().Undo().Undo();
            useCase.PointsOf(fed).Should().BeEmpty();
            useCase.Points.Should().BeEmpty();
        }

        [TestMethod]
        public void PointTracker_ShouldRaisePointsChangedEventWhenScoringPoints()
        {
            useCase.MonitorEvents();
            useCase.PointFor(fed);
            useCase.ShouldRaise("PointsChanged")
                .WithSender(useCase)
                .WithArgs<PointsChangedEventArgs>(x => true);
        }

        [TestMethod]
        public void PointTracker_ShouldRaisePointsChangedEventWhenUndoingPoints()
        {
            useCase.PointFor(fed);
            useCase.MonitorEvents();
            useCase.Undo();
            useCase.ShouldRaise("PointsChanged")
                .WithSender(useCase)
                .WithArgs<PointsChangedEventArgs>(x => true);
        }
    }
}
