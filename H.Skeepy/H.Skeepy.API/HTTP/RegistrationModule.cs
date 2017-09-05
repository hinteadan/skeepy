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
        public RegistrationModule(RegistrationFlow registrationFlow)
            : base("/registration")
        {
            Post["/apply", true] = async (_, c) => Response.AsText((await registrationFlow.Apply(this.Bind<ApplicantDto>())).Public).WithStatusCode(HttpStatusCode.Accepted);
            Get["/validate/{Token}", true] = async (p, c) => TokenToHttpStatusCode(await registrationFlow.Validate((string)p.Token));
        }

        private static HttpStatusCode TokenToHttpStatusCode(Token token)
        {
            if (token == null) return HttpStatusCode.NotFound;
            if (token.HasExpired()) return HttpStatusCode.Gone;
            return HttpStatusCode.OK;
        }
    }
}
