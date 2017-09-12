using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Housekeeping;
using System.Linq;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Housekeeping
{
    [TestClass]
    public class TokenHousekeepingOperations
    {
        private readonly JsonWebTokenGenerator expiredTokenGenerator = new JsonWebTokenGenerator(TimeSpan.FromMinutes(-1));
        private readonly JsonWebTokenGenerator validTokenGenerator = new JsonWebTokenGenerator(TimeSpan.FromHours(1));

        private ICanManageSkeepyStorageFor<Token> tokenStore;

        [TestCleanup]
        public void TestUninit()
        {
            tokenStore?.Dispose();
        }

        [TestMethod]
        public void TokenHousekeeping_DoesNothingForValidTokens()
        {
            var tokens = new Token[] { validTokenGenerator.Generate("hintee1"), validTokenGenerator.Generate("hintee2") };
            tokenStore = new InMemoryTokensStore(tokens);
            new TokenJanitor(tokenStore).Clean().Wait();
            tokenStore.Get().Result.Select(x => x.Full).ShouldAllBeEquivalentTo(tokens);
        }

        [TestMethod]
        public void TokenHousekeeping_ZapsExpiredTokens()
        {
            var tokens = new Token[] { expiredTokenGenerator.Generate("hintee1"), validTokenGenerator.Generate("hintee2") };
            tokenStore = new InMemoryTokensStore(tokens);
            new TokenJanitor(tokenStore).Clean().Wait();
            tokenStore.Get().Result.Select(x => x.Full).ShouldAllBeEquivalentTo(new Token[] { tokens[1] });
        }
    }
}
