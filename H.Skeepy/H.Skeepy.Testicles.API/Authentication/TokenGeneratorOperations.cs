using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    public abstract class TokenGeneratorOperations
    {
        private readonly ICanGenerateTokens tokenGenerator;

        public TokenGeneratorOperations(ICanGenerateTokens tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }

        [TestMethod]
        public void TokenGenerator_GeneratesUniqueTokens()
        {
            tokenGenerator.Generate().Should().NotBe(tokenGenerator.Generate());
        }
    }
}
