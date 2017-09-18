using H.Skeepy.API;
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
