using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Registration;
using H.Skeepy.API.Authentication;
using FluentAssertions;
using H.Skeepy.API.Authentication.Storage;

namespace H.Skeepy.Testicles.API.Registration
{
    [TestClass]
    public class RegistrationFlowOperations
    {
        private readonly ICanGenerateTokens<string> tokenGenerator = new JsonWebTokenGenerator();

        [TestMethod]
        public void RegistrationFlow_GeneratesAndStoresTokenForValidatingApplicant()
        {
            var tokenStore = new InMemoryTokensStore();

            var registrationToken = new RegistrationFlow(tokenGenerator).Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registrationToken.Should().NotBeNull();
            registrationToken.UserId.Should().Be("hintea_dan@yahoo.co.uk");
            tokenStore.Get(registrationToken.Id).Result.ShouldBeEquivalentTo(registrationToken);
        }

        [TestMethod]
        public void RegistrationFlow_ValidatesApplicantToken()
        {

        }

        [TestMethod]
        public void RegistrationFlow_SetsPasswordForValidatedApplicant()
        {

        }

        [TestMethod]
        public void RegistrationFlow_MakesApplicantAuthenticateableAndIndividualInSkeepy()
        {

        }
    }
}
