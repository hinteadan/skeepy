using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.API.Authentication.Storage;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class LoginFlowOperations
    {
        private InMemoryTokensStore tokenStore;
        private LoginFlow loginFlow;

        [TestInitialize]
        public void Init()
        {
            tokenStore = new InMemoryTokensStore();
            loginFlow = new LoginFlow(new InMemoryCredentialsAuthenticator(new Credentials("fed", "123")), tokenStore);
        }

        [TestCleanup]
        public void Uninit()
        {
            tokenStore.Dispose();
            tokenStore = null;
            loginFlow = null;
        }

        [TestMethod]
        public void LoginFlow_StoresTheTokenUponSuccesfulLogin()
        {
            loginFlow.Login(new Credentials("fed", "123asdasd")).Wait();
            tokenStore.Any().Result.Should().BeFalse();
            var token = loginFlow.Login(new Credentials("fed", "123")).Result.Token;
            tokenStore.Any().Result.Should().BeTrue();
            tokenStore.Get(token.Id).Result.ShouldBeEquivalentTo(token);
        }

        [TestMethod]
        public void LoginFlow_GeneratesTokensThatCanAfterwardsBeAuthenticated()
        {
            var token = loginFlow.Login(new Credentials("fed", "123")).Result.Token;
            new InMemoryTokenAuthenticator(tokenStore).Authenticate(token.Public).Result.ShouldBeEquivalentTo(AuthenticationResult.Successful(token));
        }
    }
}
