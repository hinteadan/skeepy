using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using H.Skeepy.API;
using FluentAssertions;
using Nancy;
using H.Skeepy.API.Registration;
using Nancy.Helpers;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Infrastructure;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class RegistrationEndpoints
    {
        private Browser browser;
        private readonly ApplicantDto applicant = new ApplicantDto
        {
            FirstName = "Roger",
            LastName = "Federer",
            Email = "hintea_dan@yahoo.co.uk",
        };

        [TestInitialize]
        public void Init()
        {
            browser = new Browser(new SkeepyApiInMemoryNancyBootsrapper(), x => {
                x.AjaxRequest();
            });
        }

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

        [TestMethod]
        public void Registration_SetsApplicantPassword()
        {
            browser.Post("/registration/pass/InexistentRegistrationToken", x => { x.Body("123qwe"); }).StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            var token = browser.Post("/registration/apply", x => { x.JsonBody(applicant); }).Body.AsString();
            browser.Post($"/registration/pass/{token}", x => { x.Body("123qwe"); }).StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestMethod]
        public void Registration_ValidatesEmailAvailability()
        {
            browser.Post($"/registration/email/availability", x => { x.FormValue("email", applicant.Email); }).Body.AsString().Should().Be("true");
            browser.Post("/registration/apply", x =>
            {
                x.JsonBody(applicant);
            });
            browser.Post($"/registration/email/availability", x => { x.FormValue("email", applicant.Email); }).Body.AsString().Should().Be("\"Email address is already registered\"");
        }

        [TestMethod]
        public void RegistrationApi_CanValidatePasswords()
        {
            browser.Post($"/registration/password/validity", x => { x.FormValue("password", "123qwe"); }).Body.AsString().Should().Be("true");
            browser.Post($"/registration/password/validity", x => { x.FormValue("password", null); }).Body.AsString().Should().Be("\"The password cannot be empty\"");
            browser.Post($"/registration/password/validity", x => { x.FormValue("password", string.Empty); }).Body.AsString().Should().Be("\"The password cannot be empty\"");
            browser.Post($"/registration/password/validity", x => { x.FormValue("password", "    \t  "); }).Body.AsString().Should().Be("\"The password cannot be empty\"");
        }
    }
}
