using Nancy;
using System;

namespace H.Skeepy.API.HTTP
{
    public class PingModule : NancyModule
    {
        public PingModule()
            : base($"{SkeepyApiConfiguration.BasePath}/ping")
        {
            Get["/"] = _ => Response.AsText($"pong @ {DateTime.Now}");
        }
    }
}
