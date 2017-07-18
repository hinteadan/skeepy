using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using H.Skeepy.Core;

namespace H.Skeepy.Testicles.Core
{
    [TestClass]
    public class ClashUseCaseScenarios
    {
        private ClashUseCase useCase;
        private Clash clash = Clash.New(Party.New("Fed", Individual.New("Fed")), Party.New("Rafa", Individual.New("Rafa")));

        [TestInitialize]
        public void Init()
        {
            useCase = new ClashUseCase(clash);
        }

        [TestMethod]
        public void ClashUseCase_()
        {
        }
    }
}
