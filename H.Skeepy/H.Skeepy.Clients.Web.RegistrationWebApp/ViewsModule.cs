using H.Skeepy.API.Contracts.Authentication;
using H.Skeepy.Core.Storage;
using Nancy;

namespace H.Skeepy.Clients.Web.RegistrationWebApp
{
    public class ViewsModule : NancyModule
    {
        public ViewsModule(ICanManageSkeepyStorageFor<Token> tokenStore)
            : base()
        {
            Get["/"] = p => View["Index.html"];
            Get["/application/success/{Token}", true] = async (p, c) =>
            {
                var token = await tokenStore.Get((string)p.Token);
                if (token == null || token.HasExpired())
                {
                    return Response.AsRedirect("/");
                }
                return View["ApplicationSuccessfull.html"];
            };
            Get["/validate/{Token}"] = p => View["ValidateToken.html", (string)p.Token];
            Get["/password/success"] = _ => View["PasswordSetSuccessfull.html"];
            Get["/password/{Token}"] = p => View["SetPassword.html", (string)p.Token];

            Get["/expired"] = _ => View["Expired.html"];
            Get["/inexistent"] = _ => View["NotFound.html"];
        }
    }
}
