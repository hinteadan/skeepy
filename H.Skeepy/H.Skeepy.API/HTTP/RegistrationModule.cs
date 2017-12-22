using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.API.Contracts.Registration;
using H.Skeepy.API.Registration;
using H.Skeepy.Core.Storage;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace H.Skeepy.API.HTTP
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule(RegistrationFlow registrationFlow, ICanManageSkeepyStorageFor<RegisteredUser> userStore)
            : base($"{SkeepyApiConfiguration.BasePath}/registration")
        {
            Post["/apply", true] = async (_, c) => Response.AsText((await registrationFlow.Apply(this.Bind<ApplicantDto>())).Public).WithStatusCode(HttpStatusCode.Accepted);
            Get["/validate/{Token}", true] = async (p, c) => TokenToHttpStatusCode(await registrationFlow.Validate((string)p.Token));
            Post["/pass/{Token}", true] = async (p, c) =>
            {
                await registrationFlow.SetPassword((string)p.Token, Request.Body.AsString());
                return HttpStatusCode.OK;
            };
            Post["/email/availability", true] = async (p, c) =>
            {
                if (await userStore.Get((string)Request.Form.email) != null)
                {
                    return Response.AsJson("Email address is already registered. You can link your SKeepy account with your social media accounts from your SKeepy profile page.");
                }
                return Response.AsJson(true);
            };
            Post["password/validity"] = _ =>
            {
                try { PasswordPolicy.Validate((string)Request.Form.password); }
                catch (SkeepyApiException ex) { return Response.AsJson(ex.DisplayMessage); }

                return Response.AsJson(true);
            };
        }

        private static HttpStatusCode TokenToHttpStatusCode(Token token)
        {
            if (token == null) return HttpStatusCode.NotFound;
            if (token.HasExpired()) return HttpStatusCode.Gone;
            return HttpStatusCode.OK;
        }
    }
}
