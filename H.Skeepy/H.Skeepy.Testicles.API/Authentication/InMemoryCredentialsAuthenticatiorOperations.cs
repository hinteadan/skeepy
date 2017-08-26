using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class InMemoryCredentialsAuthenticatiorOperations : AuthenticationOperations<Credentials>
    {
        public InMemoryCredentialsAuthenticatiorOperations() 
            : base(new InMemoryCredentialsAuthenticator(new Credentials("fed", "123")), new Credentials("fed", "123"), new Credentials("rafa", "fed"))
        {
        }
    }
}
