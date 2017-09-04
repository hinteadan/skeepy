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
            using (var tokenStore = new InMemoryTokensStore())
            {
                var registrationToken = new RegistrationFlow(tokenStore, tokenGenerator).Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
                registrationToken.Should().NotBeNull();
                registrationToken.UserId.Should().Be("hintea_dan@yahoo.co.uk");
                tokenStore.Get(registrationToken.Id).Result.ShouldBeEquivalentTo(registrationToken);
            }
        }

        [TestMethod]
        public void RegistrationFlow_CorrectlyValidatesApplicantToken()
        {
            using (var tokenStore = new InMemoryTokensStore())
            {
                var registration = new RegistrationFlow(tokenStore, tokenGenerator);
                var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
                registration.Validate(registrationToken.Public).Result.ShouldBeEquivalentTo(registrationToken);
                registration.Validate("SomeRandomInexistentTokenPublicKey").Result.Should().BeNull();

                var expiredRegistrationFlow = new RegistrationFlow(tokenStore, new JsonWebTokenGenerator(TimeSpan.FromSeconds(-1)));
                var expiredToken = expiredRegistrationFlow.Apply(new ApplicantDto { FirstName = "Rafa", Email = "hinteadan@yahoo.co.uk" }).Result;
                expiredRegistrationFlow.Validate(expiredToken.Public).Result.HasExpired().Should().BeTrue();
            }
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
