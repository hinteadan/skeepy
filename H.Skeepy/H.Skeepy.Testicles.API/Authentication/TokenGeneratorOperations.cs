using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    public abstract class TokenGeneratorOperations
    {
        private readonly ICanGenerateTokens<Credentials> tokenGenerator;

        public TokenGeneratorOperations(ICanGenerateTokens<Credentials> tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }

        [TestMethod]
        public void TokenGenerator_GeneratesUniqueTokens()
        {
            tokenGenerator.Generate(new Credentials("fed", "123qwe")).Should().NotBe(tokenGenerator.Generate(new Credentials("fed", "123qwe")));
        }
    }
}
