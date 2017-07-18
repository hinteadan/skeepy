using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Model;
using FluentAssertions;

namespace H.Skeepy.Testicles.Model
{
    [TestClass]
    public class DetailsHolderOperations
    {
        [TestMethod]
        public void DetailsHolder_HasNoDetailsToStartWith()
        {
            var fed = Individual.New("Roger", "Federer");
            fed.Details.Should().HaveCount(0);
        }

        [TestMethod]
        public void DetailsHolder_CanCrudDetails()
        {
            var fed = Individual.New("Roger", "Federer");
            fed.SetDetail("Height", "1.85m");
            fed.Details.Should().HaveCount(1);
            fed.GetDetail("Height").Should().Be("1.85m");
            fed.ZapDetail("Height");
            fed.Details.Should().HaveCount(0);
        }
    }
}
