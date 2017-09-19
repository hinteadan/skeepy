using H.Skeepy.Clients.Web.RegistrationWebApp;
using H.Versioning.VersionProviders;
using System.Configuration;
using System.Web;

namespace H.Skeepy.Clients.Web.Hosts.ASP.RegistrationWebApp
{
    public class AspBootstrapper : Bootstrapper
    {
        static AspBootstrapper()
        {
            FileVersionProviderSettings.Default.VersionFilePath = MapPath("~/version.txt");
        }

        private static string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }

            return $"{HttpRuntime.AppDomainAppPath}{path.Replace("~", string.Empty).Replace('/', '\\')}";
        }
    }
}