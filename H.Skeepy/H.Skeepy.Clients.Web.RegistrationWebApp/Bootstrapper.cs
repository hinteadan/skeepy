using H.Skeepy.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using H.Skeepy.API.Infrastructure;

namespace H.Skeepy.Clients.Web.RegistrationWebApp
{
    public class Bootstrapper : SkeepyApiNancyBootstrapper
    {
        static Bootstrapper()
        {
            SkeepyApiConfiguration.BasePath = "/skeepy";
        }

    }
}
