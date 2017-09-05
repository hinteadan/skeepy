using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.Model;
using H.Skeepy.API.Registration;

namespace H.Skeepy.API
{
    public class SkeepyApiNancyBootsrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            container.Register<ICanGenerateTokens<string>>(new JsonWebTokenGenerator(TimeSpan.FromHours(24)));
            container.Register<ICanManageSkeepyStorageFor<Token>>(new InMemoryTokensStore());
            container.Register<ICanManageSkeepyStorageFor<RegisteredUser>>(new InMemoryRegistrationStore());
            container.Register<ICanStoreSkeepy<Credentials>>(new InMemoryCredentialsStore());
            container.Register<ICanManageSkeepyStorageFor<Individual>>(new InMemoryIndividualsStore());

            container.Register(new RegistrationFlow(
                container.Resolve<ICanManageSkeepyStorageFor<RegisteredUser>>(),
                container.Resolve<ICanStoreSkeepy<Credentials>>(),
                container.Resolve<ICanManageSkeepyStorageFor<Individual>>(),
                container.Resolve<ICanManageSkeepyStorageFor<Token>>(),
                container.Resolve<ICanGenerateTokens<string>>()
                ));

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
