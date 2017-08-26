using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API.Authentication;

namespace H.Skeepy.Testicles.API.Authentication
{
    [TestClass]
    public class GuidTokenGeneratorOperations : TokenGeneratorOperations
    {
        public GuidTokenGeneratorOperations() 
            : base(new GuidTokenGenerator())
        {
        }
    }
}
