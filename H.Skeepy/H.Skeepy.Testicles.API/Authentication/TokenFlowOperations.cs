using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class TokenFlowOperations
    {
        private readonly Credentials validCredentials = new Credentials("fed", "123");

        private InMemoryTokensStore tokenStore;
        private LoginFlow loginFlow;
        private TokenFlow tokenFlow;

        [TestInitialize]
        public void Init()
        {
            tokenStore = new InMemoryTokensStore();
            loginFlow = new LoginFlow(new InMemoryCredentialsAuthenticator(new JsonWebTokenGenerator(TimeSpan.FromDays(14)), validCredentials), tokenStore);
            tokenFlow = new TokenFlow(new InMemoryTokenAuthenticator(tokenStore), new JsonWebTokenGenerator(TimeSpan.FromDays(14)), tokenStore);
        }

        [TestCleanup]
        public void Uninit()
        {
            tokenStore.Dispose();
            tokenStore = null;
            tokenFlow = null;
        }

        [TestMethod]
        public void TokenFlow_ReturnsTheSameTokenIfIsStillValid()
        {
            var loginResult = loginFlow.Login(validCredentials).Result;
            var tokenResult = tokenFlow.Authenticate(loginResult.Token.Public).Result;
            tokenFlow.Authenticate(loginResult.Token.Public).Result.ShouldBeEquivalentTo(tokenResult);
            tokenFlow.Authenticate(loginResult.Token.Public).Result.ShouldBeEquivalentTo(tokenResult);
        }

        [TestMethod]
        public void TokenFlow_RegenratesTokenIfIsExpiredAndNotTimedOutAndUpdatesTheStore()
        {
            tokenFlow = new TokenFlow(new InMemoryTokenAuthenticator(tokenStore), new JsonWebTokenGenerator(TimeSpan.FromSeconds(-1)), tokenStore, TimeSpan.FromMinutes(10));
            loginFlow = new LoginFlow(new InMemoryCredentialsAuthenticator(new JsonWebTokenGenerator(TimeSpan.FromSeconds(-1)), validCredentials), tokenStore);
            var loginResult = loginFlow.Login(validCredentials).Result;
            var tokenResult = tokenFlow.Authenticate(loginResult.Token.Public).Result;
            tokenResult.IsSuccessful.Should().BeTrue();
            tokenResult.Token.Secret.Should().NotBe(loginResult.Token.Secret);
            tokenResult.Token.Public.Should().NotBe(loginResult.Token.Public);
            tokenResult.Token.UserId.Should().Be(loginResult.Token.UserId);
            tokenStore.Get(loginResult.Token.Id).Result.Should().BeNull();
            tokenStore.Get(tokenResult.Token.Id).Result.ShouldBeEquivalentTo(tokenResult.Token);
        }
    }
}
