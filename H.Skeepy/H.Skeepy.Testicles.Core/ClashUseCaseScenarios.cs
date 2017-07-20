using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using H.Skeepy.Core;
using FluentAssertions;

namespace H.Skeepy.Testicles.Core
{
    [TestClass]
    public class ClashUseCaseScenarios
    {
        private ClashUseCase useCase;
        private Party fed = Party.New("Fed", Individual.New("Fed"));
        private Party rafa = Party.New("Rafa", Individual.New("Rafa"));
        private Clash clash;

        public ClashUseCaseScenarios()
        {
            clash = Clash.New(fed, rafa);
        }

        [TestInitialize]
        public void Init()
        {
            useCase = new ClashUseCase(clash);
        }

        [TestMethod]
        public void ClashUseCase_CanTrackPointsOnlyForMemberParties()
        {
            var wawrinka = Party.New("Stan", Individual.New("The Man"));
            Assert.ThrowsException<InvalidOperationException>(() => useCase.PointFor(wawrinka));
        }

        [TestMethod]
        public void ClashUseCase_TracksPoints()
        {
            var a = useCase.PointFor(fed);
            var b = useCase.PointFor(rafa);
            useCase.Points.Should().Equal(b, a);
        }

        [TestMethod]
        public void ClashUseCase_CanGiveMeThePointsForSpecificParty()
        {
            var wawrinka = Party.New("Stan", Individual.New("The Man"));
            Assert.ThrowsException<InvalidOperationException>(() => useCase.PointsOf(wawrinka));

            var fedPoint = useCase.PointFor(fed);
            var rafaPoint = useCase.PointFor(rafa);

            useCase.PointsOf(fed).Should().Equal(fedPoint);
            useCase.PointsOf(rafa).Should().Equal(rafaPoint);
        }

        [TestMethod]
        public void ClashUseCase_CanUndoLastPoint()
        {
            var a = useCase.PointFor(fed);
            var b = useCase.PointFor(rafa);

            useCase.Undo();

            useCase.Points.Should().Equal(a);
            useCase.PointsOf(rafa).Should().BeEmpty();
            useCase.PointsOf(fed).Should().Equal(a);
        }

        [TestMethod]
        public void ClashUseCase_UndoLastPointDoesNothingWhenThereAreNoPoints()
        {
            useCase.Undo();
        }
    }
}
