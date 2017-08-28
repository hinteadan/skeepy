using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using H.Skeepy.API.Authentication;

namespace H.Skeepy.API
{
    public class SkeepyApiNancyBootsrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            container.Register<ICanGenerateTokens<string>>(new JsonWebTokenGenerator(TimeSpan.FromHours(24)));

            pipelines.OnError.AddItemToEndOfPipeline((context, exception) =>
            {
                return new Response
                {
                    StatusCode = StatusForException(exception),
                    ReasonPhrase = exception.Message
                };
            });
        }

        private HttpStatusCode StatusForException(Exception exception)
        {
            if (exception is NotImplementedException) return HttpStatusCode.NotImplemented;
            if (exception is InvalidOperationException) return HttpStatusCode.UnprocessableEntity;
            return HttpStatusCode.InternalServerError;
        }
    }
}
