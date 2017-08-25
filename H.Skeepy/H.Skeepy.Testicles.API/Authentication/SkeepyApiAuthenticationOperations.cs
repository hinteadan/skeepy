using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class SkeepyApiAuthenticationOperations
    {
        [TestMethod]
        public void SkeepyAuthApi_CanAuthenticateUserViaCredentials()
        {
            var authenticator = new CredentialsAuthenticator();
            authenticator.Authenticate("asd", "cxcvv").Should().BeFalse();
            authenticator.Authenticate("hintee", "123qwe").Should().BeTrue();
        }
    }
}
