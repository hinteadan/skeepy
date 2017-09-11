using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using H.Skeepy.API;
using FluentAssertions;

namespace H.Skeepy.Testicles.API
{
    [TestClass]
    public class PasswordPolicyOperations
    {
        [TestMethod]
        public void PasswordValidator_DoesntAllowNullOrEmptyPasswords()
        {
            new Action(() => { PasswordPolicy.Validate(null); }).ShouldThrow<SkeepyApiException>();
            new Action(() => { PasswordPolicy.Validate(string.Empty); }).ShouldThrow<SkeepyApiException>();
            new Action(() => { PasswordPolicy.Validate("   \t "); }).ShouldThrow<SkeepyApiException>();
        }
    }
}
