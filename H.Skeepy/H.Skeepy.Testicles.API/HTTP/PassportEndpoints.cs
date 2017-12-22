using FluentAssertions;
using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class PassportEndpoints
    {
        private Browser browser;
        private static readonly Credentials invalidCredentials = new Credentials("Invalid", "Credentials");

        [TestInitialize]
        public void Init()
        {
            browser = new Browser(new SkeepyApiInMemoryNancyBootsrapper(), x =>
            {
                x.AjaxRequest();
            });
        }

        [TestMethod]
        public void PassportApi_Responds_With_Unauthorized_If_Login_Is_Invalid()
        {
            var result = browser.Post("/passport", x =>
            {
                x.JsonBody(invalidCredentials);
            });
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
