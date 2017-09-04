using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Registration;
using H.Skeepy.API.Authentication;
using FluentAssertions;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Registration.Storage;

namespace H.Skeepy.Testicles.API.Registration
{
    [TestClass]
    public class RegistrationFlowOperations
    {
        private readonly ICanGenerateTokens<string> tokenGenerator = new JsonWebTokenGenerator();
        private ICanManageSkeepyStorageFor<Token> tokenStore;
        private ICanManageSkeepyStorageFor<RegisteredUser> registrationStore;
        private RegistrationFlow registration;

        [TestInitialize]
        public void Init()
        {
            tokenStore = new InMemoryTokensStore();
            registrationStore = new InMemoryRegistrationStore();
            registration = new RegistrationFlow(registrationStore, tokenStore, tokenGenerator);
        }

        [TestCleanup]
        public void UnInit()
        {
            registrationStore.Dispose();
            tokenStore.Dispose();
        }

        [TestMethod]
        public void RegistrationFlow_StoresApplicantData()
        {
            registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Wait();
            registrationStore.Get("hintea_dan@yahoo.co.uk").Result.ShouldBeEquivalentTo(new RegisteredUser { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" });
        }

        [TestMethod]
        public void RegistrationFlow_GeneratesAndStoresTokenForValidatingApplicant()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registrationToken.Should().NotBeNull();
            registrationToken.UserId.Should().Be("hintea_dan@yahoo.co.uk");
            tokenStore.Get(registrationToken.Id).Result.ShouldBeEquivalentTo(registrationToken);
        }

        [TestMethod]
        public void RegistrationFlow_CorrectlyValidatesApplicantToken()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registration.Validate(registrationToken.Public).Result.ShouldBeEquivalentTo(registrationToken);
            registration.Validate("SomeRandomInexistentTokenPublicKey").Result.Should().BeNull();

            var expiredRegistrationFlow = new RegistrationFlow(registrationStore, tokenStore, new JsonWebTokenGenerator(TimeSpan.FromSeconds(-1)));
            var expiredToken = expiredRegistrationFlow.Apply(new ApplicantDto { FirstName = "Rafa", Email = "hinteadan@yahoo.co.uk" }).Result;
            expiredRegistrationFlow.Validate(expiredToken.Public).Result.HasExpired().Should().BeTrue();
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
