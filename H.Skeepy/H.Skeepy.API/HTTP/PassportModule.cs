using H.Skeepy.API.Authentication;
using Nancy;

namespace H.Skeepy.API.HTTP
{
    public class PassportModule : NancyModule
    {
        public PassportModule(LoginFlow loginFlow)
            : base($"{SkeepyApiConfiguration.BasePath}/passport")
        {
            Post["/", true] = (_, c) => 
        }
    }
}
