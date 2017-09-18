using Nancy;

namespace H.Skeepy.API.HTTP
{
    public class VersionModule : NancyModule
    {
        public VersionModule() : base($"{SkeepyApiConfiguration.BasePath}/version")
        {
            Get["/"] = _ => Response.AsText(Versioning.Version.Self.GetCurrent().ToString());
            Get["/json"] = _ => Response.AsJson(Versioning.Version.Self.GetCurrent());
        }
    }
}
