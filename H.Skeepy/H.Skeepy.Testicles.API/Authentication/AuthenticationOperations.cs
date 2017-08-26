using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    public abstract class AuthenticationOperations<T>
    {
        private readonly ICanAuthenticate<T> authenticator;
        private readonly T validId;
        private readonly T invalidId;

        public AuthenticationOperations(ICanAuthenticate<T> authenticator, T validId, T invalidId)
        {
            this.authenticator = authenticator;
            this.validId = validId;
            this.invalidId = invalidId;
        }

        [TestMethod]
        public void SkeepyAuthApi_CanAuthenticateIdentity()
        {
            authenticator.Authenticate(invalidId).IsSuccessful.Should().BeFalse();
            authenticator.Authenticate(validId).IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        public void SkeepyAuthApi_GeneratesTokenUponSuccessfulAuthentication()
        {
            authenticator.Authenticate(invalidId).Token.Should().BeNull();
            authenticator.Authenticate(validId).Token.Should().NotBeNull();
        }
    }
}
