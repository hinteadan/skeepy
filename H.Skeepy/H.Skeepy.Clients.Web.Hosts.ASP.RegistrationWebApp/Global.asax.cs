using H.Versioning.VersionProviders;
using NLog;
using System;
using System.Web;

namespace H.Skeepy.Clients.Web.Hosts.ASP.RegistrationWebApp
{
    public class Global : System.Web.HttpApplication
    {

        private static Logger log = LogManager.GetCurrentClassLogger();

        static Global()
        {
            FileVersionProviderSettings.Default.VersionFilePath = MapPath("~/version.txt");

            log.Info($"Configured H.Versioning with version file path: {FileVersionProviderSettings.Default.VersionFilePath}");
        }

        private static string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(path);
            }

            return $"{HttpRuntime.AppDomainAppPath}{path.Replace("~", string.Empty).Replace('/', '\\')}";
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}