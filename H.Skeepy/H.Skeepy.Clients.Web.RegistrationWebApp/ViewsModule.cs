using H.Skeepy.API.Authentication;
using H.Skeepy.Core.Storage;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Get["/password/{Token}"] = p => View["SetPassword.html", (string)p.Token];

            Get["/expired"] = _ => View["Expired.html"];
            Get["/inexistent"] = _ => View["NotFound.html"];
        }
    }
}
