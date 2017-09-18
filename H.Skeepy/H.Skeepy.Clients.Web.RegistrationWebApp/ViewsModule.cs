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
            Get["/"] = p => View["Index.html", ViewModel.None];
            Get["/application/success/{Token}", true] = async (p, c) =>
            {
                var token = await tokenStore.Get((string)p.Token);
                if (token == null || token.HasExpired())
                {
                    return Response.AsRedirect("/");
                }
                return View["ApplicationSuccessfull.html", ViewModel.None];
            };
            Get["/validate/{Token}"] = p => View["ValidateToken.html", ViewModel.With((string)p.Token)];
            Get["/password/success"] = _ => View["PasswordSetSuccessfull.html", ViewModel.None];
            Get["/password/{Token}"] = p => View["SetPassword.html", ViewModel.With((string)p.Token)];

            Get["/expired"] = _ => View["Expired.html", ViewModel.None];
            Get["/inexistent"] = _ => View["NotFound.html", ViewModel.None];
        }
    }
}
