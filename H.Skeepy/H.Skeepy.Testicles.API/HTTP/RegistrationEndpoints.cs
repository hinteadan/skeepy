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

        [TestMethod]
        public void Registration_ChecksApplicantData()
        {
            browser.Post("/registration/apply", x => { x.JsonBody(null as object); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { LastName = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { Email = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger", LastName = "Federer", Email = "dfasdfas" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger", LastName = "Federer", Email = "abc@asdasd" }); })
               .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}
