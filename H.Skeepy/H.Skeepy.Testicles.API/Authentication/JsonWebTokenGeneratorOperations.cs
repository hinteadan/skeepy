using System;
using H.Skeepy.API.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class JsonWebTokenGeneratorOperations : TokenGeneratorOperations
    {
        public JsonWebTokenGeneratorOperations()
            : base(new JsonWebTokenGenerator())
        {
        }
    }
}
