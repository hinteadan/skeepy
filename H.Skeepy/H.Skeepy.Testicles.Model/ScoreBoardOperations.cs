using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class ScoreBoardOperations
    {
        private static readonly Party fed = Party.New("Fed", Individual.New("Fed"));
        private static readonly Party rafa = Party.New("Rafa", Individual.New("Rafa"));
        private static readonly Clash clash = Clash.New(fed, rafa);

        [TestMethod]
        public void ScoreBoard_MustHaveUnderlyingClash()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new ScoreBoard<int>(null));
        }

        [TestMethod]
        public void ScoreBoard_HoldsScorePartiesThatArePartOfTheClash()
        {
            var board = new ScoreBoard<int>(clash);
            var stan = Party.New("Stan", Individual.New("Stan"));
            Assert.ThrowsException<InvalidOperationException>(() => board.SetScore(stan, 10));
        }

        [TestMethod]
        public void ScoreBoard_HoldsScoreForEachParty()
        {
            var board = new ScoreBoard<int>(clash);
            board[fed].Should().Be(0);
            board.SetScore(fed, 10)[fed].Should().Be(10);
            board.SetScore(rafa, 8);
            board.Scores.Should().BeEquivalentTo((fed, 10), (rafa, 8));
        }
    }
}
