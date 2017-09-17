using System;
using H.Skeepy.API.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Contracts.Authentication;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class InMemoryTokenAuthenticationOperations : AuthenticationOperations<string>
    {
        public InMemoryTokenAuthenticationOperations()
            : base(new InMemoryTokenAuthenticator(new Token("fed", "secretKey", "publicKey")), "publicKey", "invalidPublicKey")
        {
        }
    }
}
