using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.HTTP
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule() 
            : base("/registration")
        {
            Post["/apply"] = _ => HttpStatusCode.Accepted;
        }
    }
}
