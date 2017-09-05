using H.Skeepy.API.Authentication;
using H.Skeepy.API.Registration;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Core.Storage;
using H.Skeepy.Model;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.HTTP
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule(ICanManageSkeepyStorageFor<RegisteredUser> registrationStore, ICanStoreSkeepy<Credentials> credentialStore, ICanManageSkeepyStorageFor<Individual> individualStore, ICanManageSkeepyStorageFor<Token> tokenStore, ICanGenerateTokens<string> tokenGenerator)
            : base("/registration")
        {
            Post["/apply", true] = async (_, c) =>
            {
                await new RegistrationFlow(registrationStore, credentialStore, individualStore, tokenStore, tokenGenerator).Apply(this.Bind<ApplicantDto>());

                return HttpStatusCode.Accepted;
            };
        }
    }
}
