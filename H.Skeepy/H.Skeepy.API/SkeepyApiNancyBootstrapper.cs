using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using H.Skeepy.Core.Storage;
using H.Skeepy.API.Authentication;
using H.Skeepy.API.Registration.Storage;
using H.Skeepy.Model;
using H.Skeepy.API.Registration;
using H.Skeepy.API.Authentication.Storage;
using H.Skeepy.Core.Storage.Individuals;
using H.Skeepy.API.Notifications;
using H.Skeepy.API.Infrastructure;

namespace H.Skeepy.API
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
