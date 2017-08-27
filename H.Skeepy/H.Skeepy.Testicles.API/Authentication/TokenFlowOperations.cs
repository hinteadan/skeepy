using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Authentication;

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
            loginFlow = new LoginFlow(new InMemoryCredentialsAuthenticator(validCredentials), tokenStore);
            tokenFlow = new TokenFlow(new InMemoryTokenAuthenticator(tokenStore), tokenStore);
        }

        [TestCleanup]
        public void Uninit()
        {
            tokenStore.Dispose();
            tokenStore = null;
            tokenFlow = null;
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
