using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Registration;
using H.Skeepy.API.Authentication;
using FluentAssertions;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Model;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.API.Notifications;
using H.Skeepy.API;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Model.Storage;

namespace H.Skeepy.Testicles.API.Registration
{
    [TestClass]
    public class RegistrationFlowOperations
    {
        private readonly ICanGenerateTokens<string> tokenGenerator = new JsonWebTokenGenerator();
        private ICanManageSkeepyStorageFor<Token> tokenStore;
        private ICanManageSkeepyStorageFor<RegisteredUser> registrationStore;
        private InMemoryCredentialsStore credentialStore;
        private RegistrationFlow registration;
        private ICanManageSkeepyStorageFor<Individual> individualStore;

        [TestInitialize]
        public void Init()
        {
            tokenStore = new InMemoryTokensStore();
            registrationStore = new InMemoryRegistrationStore();
            credentialStore = new InMemoryCredentialsStore();
            individualStore = new InMemoryIndividualsStore();
            registration = new RegistrationFlow(registrationStore, credentialStore, individualStore, tokenStore, tokenGenerator, new NullNotifier());
        }

        [TestCleanup]
        public void UnInit()
        {
            individualStore.Dispose();
            credentialStore.Dispose();
            registrationStore.Dispose();
            tokenStore.Dispose();
        }

        [TestMethod]
        public void RegistrationFlow_StoresApplicantData()
        {
            registration.Apply(new ApplicantDto
            {
                FirstName = "Fed",
                Email = "hintea_dan@yahoo.co.uk",
                FacebookDetails = new DetailsHolderDto
                {
                    Details = new DetailsHolderDto.Entry[] {
                        new DetailsHolderDto.Entry{ Key="FacebooDetail1", Value="FacebookValue1" },
                        new DetailsHolderDto.Entry{ Key="FacebooDetail2", Value="FacebookValue2" }
                    }
                }
            }).Wait();
            registrationStore
                .Get("hintea_dan@yahoo.co.uk")
                .Result
                .ShouldBeEquivalentTo(new RegisteredUser
                {
                    FirstName = "Fed",
                    Email = "hintea_dan@yahoo.co.uk"
                }
                .SetDetail("FacebooDetail1", "FacebookValue1")
                .SetDetail("FacebooDetail2", "FacebookValue2"));
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

            var expiredRegistrationFlow = new RegistrationFlow(registrationStore, credentialStore, individualStore, tokenStore, new JsonWebTokenGenerator(TimeSpan.FromSeconds(-1)), new NullNotifier());
            var expiredToken = expiredRegistrationFlow.Apply(new ApplicantDto { FirstName = "Rafa", Email = "hinteadan@yahoo.co.uk" }).Result;
            expiredRegistrationFlow.Validate(expiredToken.Public).Result.HasExpired().Should().BeTrue();
        }

        [TestMethod]
        public void RegistrationFlow_ThrowsExceptionOnInvalidPasswords()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            new Action(() => { registration.SetPassword(registrationToken.Public, null).Wait(); }).ShouldThrow<InvalidOperationException>();
            new Action(() => { registration.SetPassword(registrationToken.Public, string.Empty).Wait(); }).ShouldThrow<InvalidOperationException>();
            new Action(() => { registration.SetPassword(registrationToken.Public, "   \t ").Wait(); }).ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void RegistrationFlow_SetsPasswordForValidatedApplicant()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registration.Validate(registrationToken.Public).Wait();
            registrationStore.Get("hintea_dan@yahoo.co.uk").Result.Status.Should().Be(RegisteredUser.AccountStatus.PendingSetPassword);
            registration.SetPassword(registrationToken.Public, "123qwe").Wait();
            registrationStore.Get("hintea_dan@yahoo.co.uk").Result.Status.Should().Be(RegisteredUser.AccountStatus.Valid);
        }

        [TestMethod]
        public void RegistrationFlow_HashesPasswords()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registration.SetPassword(registrationToken.Public, "123qwe").Wait();
            credentialStore.Get("hintea_dan@yahoo.co.uk").Result.Should().NotBeNull();
            credentialStore.Get("hintea_dan@yahoo.co.uk").Result.Password.Should().NotBeEmpty();
            credentialStore.Get("hintea_dan@yahoo.co.uk").Result.Password.Should().NotBe("123qwe");
        }

        [TestMethod]
        public void RegistrationFlow_MakesApplicantAuthenticateableAndIndividualInSkeepy()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registration.SetPassword(registrationToken.Public, "123qwe").Wait();

            var auth = new InMemoryCredentialsAuthenticator(credentialStore);
            var authResult = auth.Authenticate(new Credentials("hintea_dan@yahoo.co.uk", "123qwe")).Result;
            authResult.Should().NotBeNull();
            authResult.IsSuccessful.Should().BeTrue();
            authResult.Token.Should().NotBeNull();
            authResult.Token.HasExpired().Should().BeFalse();

            var user = registrationStore.Get("hintea_dan@yahoo.co.uk").Result;
            user.SkeepyId.Should().NotBeNullOrWhiteSpace();

            var fed = individualStore.Get(user.SkeepyId).Result;
            fed.Should().NotBeNull();
            fed.FirstName.Should().Be("Fed");
            fed.LastName.Should().BeNullOrEmpty();
            fed.GetDetail("Email").Should().Be("hintea_dan@yahoo.co.uk");
        }

        [TestMethod]
        public void RegistrationFlow_CleansUpTokensAfterRegistrationCompletes()
        {
            var registrationToken = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = "hintea_dan@yahoo.co.uk" }).Result;
            registration.SetPassword(registrationToken.Public, "123qwe").Wait();
            tokenStore.Get(registrationToken.Id).Result.Should().BeNull();
        }

        [TestMethod]
        public void RegistrationFlow_ShouldNotAllowRegistrationWithSameEmailAddress()
        {
            var email = "hintee@skeepy.ro";
            var token = registration.Apply(new ApplicantDto { FirstName = "Fed", Email = email }).Result;
            new Action(() => registration.Apply(new ApplicantDto { FirstName = "Rafa", Email = email }).Wait()).ShouldThrow<SkeepyApiException>();
            registration.Validate(token.Public).Wait();
            new Action(() => registration.Apply(new ApplicantDto { FirstName = "Rafa", Email = email }).Wait()).ShouldThrow<SkeepyApiException>();
            registration.SetPassword(token.Public, "123qwe").Wait();
            new Action(() => registration.Apply(new ApplicantDto { FirstName = "Rafa", Email = email }).Wait()).ShouldThrow<SkeepyApiException>();
        }
    }
}
