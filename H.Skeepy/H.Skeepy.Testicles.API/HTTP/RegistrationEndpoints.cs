using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using H.Skeepy.API;
using FluentAssertions;
using Nancy;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class RegistrationEndpoints
    {
        private readonly Browser browser = new Browser(new SkeepyApiNancyBootsrapper());

        [TestMethod]
        public void Registration_RequestCanBeSubmittedByAnyone()
        {
            var applicant = new
            {
                FirstName = "Roger",
                LastName = "Federer",
                Email = "hintea_dan@yahoo.co.uk",
            };
            var result = browser.Post("/registration/apply", x =>
            {
                x.JsonBody(applicant);
            });
            result.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }
    }
}
