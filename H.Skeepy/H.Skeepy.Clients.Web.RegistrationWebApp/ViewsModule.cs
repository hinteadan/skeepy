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
        public ViewsModule()
            : base()
        {
            Get["/"] = p => View["Index.html"];
        }
    }
}
