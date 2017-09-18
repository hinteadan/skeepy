using FluentAssertions;
using H.Skeepy.API.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class PingEndpoints
    {
        private readonly Browser browser = new Browser(new SkeepyApiInMemoryNancyBootsrapper());

        [TestMethod]
        public void Ping_GetsPongResponse()
        {
            var result = browser.Get("/ping");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ContentType.Should().Be("text/plain");
            result.Body.AsString().Should().StartWith("pong");
        }
    }
}
