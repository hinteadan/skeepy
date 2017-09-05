using System.Web;
using System.Web.Mvc;

namespace H.Skeepy.Clients.Web.UserRegistrationWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
