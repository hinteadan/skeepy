using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API;
using FluentAssertions;

namespace H.Skeepy.Testicles.API
{
    [TestClass]
    public class HashOperations
    {
        [TestMethod]
        public void Hasher_CorrectlyHashesStrings()
        {
            var hash = Hasher.Hash("123qwe");
            hash.Should().NotBeEmpty();
            hash.Should().NotBe("123qwe");
            Hasher.Verify(hash, "123qwe").Should().BeTrue();
        }
    }
}
