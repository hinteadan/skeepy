﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class SkeepyApiAuthenticationOperations
    {
        private readonly InMemoryCredentialsAuthenticator authenticator = new InMemoryCredentialsAuthenticator(("hintee", "123qwe"), ("hintee2", "123qwerty"));

        [TestMethod]
        public void SkeepyAuthApi_CanAuthenticateUserViaCredentials()
        {
            authenticator.Authenticate("asd", "cxcvv").Should().BeFalse();
            authenticator.Authenticate("hintee", "123qwe").Should().BeTrue();
            authenticator.Authenticate("hintee2", "123qwerty").Should().BeTrue();
        }
    }
}
