using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class SkeepyApiAuthenticationOperations
    {
        private readonly ICanAuthenticate<(string,string)> authenticator = new InMemoryCredentialsAuthenticator(("hintee", "123qwe"));

        [TestMethod]
        public void SkeepyAuthApi_CanAuthenticateUserViaCredentials()
        {
            authenticator.Authenticate(("asd", "cxcvv")).IsSuccessful.Should().BeFalse();
            authenticator.Authenticate(("hintee", "123qwe")).IsSuccessful.Should().BeTrue();
        }
    }
}
