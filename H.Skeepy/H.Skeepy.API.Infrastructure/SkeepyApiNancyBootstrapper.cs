using Nancy.TinyIoc;

namespace H.Skeepy.API.Infrastructure
{
    public class SkeepyApiNancyBootstrapper : SkeepyApiInMemoryNancyBootsrapper
    {
        protected override void RegisterSkeepyBuildingBlocks(TinyIoCContainer container)
        {
            base.RegisterSkeepyBuildingBlocks(container);

            DefaultSkeepyApiBuildingBlocks.RegisterWithTinyIoc(container);
        }
    }
}
