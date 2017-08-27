using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace H.Skeepy.API
{
    public class SkeepyApiNancyBootsrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
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
