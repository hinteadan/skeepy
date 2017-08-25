using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class InMemoryCredentialsAuthenticatiorOperations : AuthenticationOperations<(string, string)>
    {
        public InMemoryCredentialsAuthenticatiorOperations() 
            : base(new InMemoryCredentialsAuthenticator(("fed", "123")), ("fed", "123"), ("rafa", "fed"))
        {
        }
    }
}
