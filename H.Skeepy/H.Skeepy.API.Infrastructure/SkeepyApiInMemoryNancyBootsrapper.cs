using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Notifications;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Notifications;
using H.Skeepy.API.Registration;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;

namespace H.Skeepy.API.Infrastructure
{
    public class SkeepyApiInMemoryNancyBootsrapper : DefaultNancyBootstrapper
    {
        protected virtual void RegisterSkeepyBuildingBlocks(TinyIoCContainer container)
        {
            DefaultSkeepyApiBuildingBlocks.RegisterWithTinyIoc(container);

            container.Register<ICanNotify>(new NullNotifier());
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            RegisterSkeepyBuildingBlocks(container);

            container.Register(new RegistrationFlow(
                container.Resolve<ICanManageSkeepyStorageFor<RegisteredUser>>(),
                container.Resolve<ICanStoreSkeepy<Credentials>>(),
                container.Resolve<ICanManageSkeepyStorageFor<Individual>>(),
                container.Resolve<ICanManageSkeepyStorageFor<Token>>(),
                container.Resolve<ICanGenerateTokens<string>>(),
                container.Resolve<ICanNotify>()
                ));

            pipelines.OnError.AddItemToEndOfPipeline((context, exception) =>
            {
                return new Response
                {
                    StatusCode = StatusForException(exception),
                    ReasonPhrase = exception.Message
                };
            });
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,PATCH,OPTIONS");
                //ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                //ctx.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                //ctx.Response.Headers.Add("Access-Control-Expose-Headers", "Accept,Origin,Content-type");
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
