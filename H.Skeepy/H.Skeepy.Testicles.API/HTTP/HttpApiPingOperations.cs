using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Nancy;
using FluentAssertions;

namespace H.Skeepy.Testicles.API.HTTP
{
    [TestClass]
    public class HttpApiPingOperations
    {
        private readonly Browser browser = new Browser(new DefaultNancyBootstrapper());

        [TestMethod]
        public void Ping_GetsPongResponse()
        {
            var result = browser.Get("/ping", ctx => { ctx.HttpRequest(); });
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ContentType.Should().Be("text/plain");
            result.Body.AsString().Should().StartWith("pong");
        }
    }
}
