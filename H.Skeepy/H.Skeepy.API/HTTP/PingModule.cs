using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.HTTP
{
    public class PingModule : NancyModule
    {
        public PingModule()
            : base("/ping")
        {
            Get["/"] = _ => Response.AsText($"pong @ {DateTime.Now}");
        }
    }
}
