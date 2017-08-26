using System;
using H.Skeepy.API.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class InMemoryTokenAuthenticationOperations : AuthenticationOperations<string>
    {
        public InMemoryTokenAuthenticationOperations()
            : base(new InMemoryTokenAuthenticator(new Token("secretKey", "publicKey")), "publicKey", "invalidPublicKey")
        {
        }
    }
}
