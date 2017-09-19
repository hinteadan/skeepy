using H.Skeepy.Clients.Web.RegistrationWebApp;
using H.Versioning.VersionProviders;
using NLog;
using System.Configuration;
using System.Web;

namespace H.Skeepy.Clients.Web.Hosts.ASP.RegistrationWebApp
{
    public class AspBootstrapper : Bootstrapper
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        static AspBootstrapper()
        {
            FileVersionProviderSettings.Default.VersionFilePath = @"D:\home\site\wwwroot\version.txt";

            log.Info($"Configured H.Versioning with version file path: {FileVersionProviderSettings.Default.VersionFilePath}");
        }
    }
}