using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.Authentication
{
    public abstract class AuthenticationOperations<T>
    {
        protected readonly ICanAuthenticate<T> authenticator;
        protected readonly T validId;
        protected readonly T invalidId;

        public AuthenticationOperations(ICanAuthenticate<T> authenticator, T validId, T invalidId)
        {
            this.authenticator = authenticator;
            this.validId = validId;
            this.invalidId = invalidId;
        }

        [TestMethod]
        public void SkeepyAuthApi_CanAuthenticateIdentity()
        {
            authenticator.Authenticate(invalidId).Result.IsSuccessful.Should().BeFalse();
            authenticator.Authenticate(validId).Result.IsSuccessful.Should().BeTrue();
        }

        [TestMethod]
        public void SkeepyAuthApi_GeneratesTokenUponSuccessfulAuthentication()
        {
            authenticator.Authenticate(validId).Result.Token.Should().NotBeNull();
        }
    }
}
