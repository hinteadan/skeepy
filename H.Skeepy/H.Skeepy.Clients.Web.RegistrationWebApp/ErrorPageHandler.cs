using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace H.Skeepy.Clients.Web.RegistrationWebApp
{
    public class ErrorPageHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public ErrorPageHandler(IViewFactory factory) : base(factory)
        {
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    context.Response = RenderView(context, "NotFound.html").WithStatusCode(HttpStatusCode.NotFound);
                    break;
                case HttpStatusCode.Gone:
                    context.Response = RenderView(context, "Expired.html").WithStatusCode(HttpStatusCode.Gone);
                    break;
            }
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound ||
                statusCode == HttpStatusCode.Gone;
        }
    }
}
