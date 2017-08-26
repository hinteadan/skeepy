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
        public void TokenGenerator_GeneratesUniquePublicTokens()
        {
            tokenGenerator.Generate(new Credentials("fed", "123qwe")).Public.Should().NotBe(tokenGenerator.Generate(new Credentials("fed", "123qwe")).Public);
            tokenGenerator.Generate(new Credentials("fed", "123qwe")).Secret.Should().NotBe(tokenGenerator.Generate(new Credentials("fed", "123qwe")).Secret);
        }
    }
}
