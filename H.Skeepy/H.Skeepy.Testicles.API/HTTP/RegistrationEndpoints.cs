﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using H.Skeepy.API;
using FluentAssertions;
using Nancy;
using H.Skeepy.API.Registration;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class RegistrationEndpoints
    {
        private readonly Browser browser = new Browser(new SkeepyApiInMemoryNancyBootsrapper());
        private readonly ApplicantDto applicant = new ApplicantDto
        {
            FirstName = "Roger",
            LastName = "Federer",
            Email = "hintea_dan@yahoo.co.uk",
        };

        [TestMethod]
        public void Registration_RequestCanBeSubmittedByAnyone()
        {
            var result = browser.Post("/registration/apply", x =>
            {
                x.JsonBody(applicant);
            });
            result.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [TestMethod]
        public void Registration_ChecksApplicantData()
        {
            browser.Post("/registration/apply", x => { x.JsonBody(new { }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { LastName = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { Email = "Roger" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger", LastName = "Federer", Email = "dfasdfas" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger", LastName = "Federer", Email = "@dfasdfas" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            browser.Post("/registration/apply", x => { x.JsonBody(new { FirstName = "Roger", LastName = "Federer", Email = "dfasdfas@" }); })
                .StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public void Registration_ValidatesApplicantToken()
        {
            browser.Get($"/registration/validate/InexistentRegistrationToken").StatusCode.Should().Be(HttpStatusCode.NotFound);

            var response = browser.Post("/registration/apply", x => { x.JsonBody(applicant); });
            var token = response.Body.AsString();
            token.Should().NotBeNullOrWhiteSpace();
            response = browser.Get($"/registration/validate/{token}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
