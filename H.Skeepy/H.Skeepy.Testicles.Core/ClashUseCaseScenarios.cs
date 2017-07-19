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
            useCase.PointFor(fed);
            useCase.PointFor(rafa);
            useCase.Points.Length.Should().Be(2);
        }
    }
}
