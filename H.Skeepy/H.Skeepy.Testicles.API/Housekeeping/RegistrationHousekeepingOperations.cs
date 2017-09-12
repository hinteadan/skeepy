using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Registration;
using H.Skeepy.Model;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.API.Notifications;

namespace H.Skeepy.Testicles.API.Housekeeping
{
    [TestClass]
    public class RegistrationHousekeepingOperations
    {
        private readonly JsonWebTokenGenerator expiredTokenGenerator = new JsonWebTokenGenerator(TimeSpan.FromMinutes(-1));
        private readonly JsonWebTokenGenerator validTokenGenerator = new JsonWebTokenGenerator(TimeSpan.FromHours(1));

        private ICanManageSkeepyStorageFor<Token> tokenStore;
        private ICanManageSkeepyStorageFor<RegisteredUser> registrationStore;
        private InMemoryCredentialsStore credentialStore;
        private ICanManageSkeepyStorageFor<Individual> individualStore;
        private RegistrationFlow expiredRegistration;
        private RegistrationFlow validRegistration;

        [TestInitialize]
        public void Init()
        {
            tokenStore = new InMemoryTokensStore();
            registrationStore = new InMemoryRegistrationStore();
            credentialStore = new InMemoryCredentialsStore();
            individualStore = new InMemoryIndividualsStore();
            expiredRegistration = new RegistrationFlow(registrationStore, credentialStore, individualStore, tokenStore, expiredTokenGenerator, new NullNotifier());
            validRegistration = new RegistrationFlow(registrationStore, credentialStore, individualStore, tokenStore, validTokenGenerator, new NullNotifier());
        }

        [TestCleanup]
        public void TestUninit()
        {
            individualStore?.Dispose();
            credentialStore?.Dispose();
            registrationStore?.Dispose();
            tokenStore?.Dispose();
        }

        [TestMethod]
        public void RegistrationJanitor_DoesnNothingForValidRegistartions()
        {

        }

        [TestMethod]
        public void RegistrationJanitor_ZapsApplicationWithExpiredOrInexistentTokens()
        {

        }
    }
}
