using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class TokenOperations
    {
        [TestMethod]
        public void Tokens_CanExpire()
        {
            new Token("", "", "").HasExpired().Should().BeFalse();
            new Token("", "", "", DateTime.Now.AddSeconds(10)).HasExpired().Should().BeFalse();
            new Token("", "", "", DateTime.Now.AddSeconds(-1)).HasExpired().Should().BeTrue();
        }
    }
}
