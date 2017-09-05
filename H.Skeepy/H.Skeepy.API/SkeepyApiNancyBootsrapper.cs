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

namespace H.Skeepy.API
{
    public class SkeepyApiNancyBootsrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var tokenStore = new InMemoryTokensStore();
            var registrationStore = new InMemoryRegistrationStore();
            var credentialStore = new InMemoryCredentialsStore();
            var individualStore = new InMemoryIndividualsStore();

            container.Register<ICanGenerateTokens<string>>(new JsonWebTokenGenerator(TimeSpan.FromHours(24)));
            container.Register<ICanManageSkeepyStorageFor<Token>>(tokenStore);
            container.Register<ICanManageSkeepyStorageFor<RegisteredUser>>(registrationStore);
            container.Register<ICanStoreSkeepy<Credentials>>(credentialStore);
            container.Register<ICanManageSkeepyStorageFor<Individual>>(individualStore);

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
