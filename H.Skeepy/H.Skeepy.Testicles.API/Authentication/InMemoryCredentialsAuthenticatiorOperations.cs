using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;
using H.Skeepy.API;
using FluentAssertions;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class InMemoryCredentialsAuthenticatiorOperations : AuthenticationOperations<Credentials>
    {
        public InMemoryCredentialsAuthenticatiorOperations() 
            : base(new InMemoryCredentialsAuthenticator(new Credentials("fed", "123"), new Credentials("hintee", Hasher.Hash("123qwe"))), new Credentials("fed", "123"), new Credentials("rafa", "fed"))
        {
        }

        [TestMethod]
        public void CredentialsAuthenticator_AuthenticatesHashedPasswords()
        {
            var result = authenticator.Authenticate(new Credentials("hintee", "123qwe")).Result;
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Token.Should().NotBeNull();
            result.Token.HasExpired().Should().BeFalse();
        }
    }
}
