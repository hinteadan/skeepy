using H.Skeepy.API.Housekeeping;
using H.Skeepy.API.Infrastructure;
using Nancy.TinyIoc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Clients.RegistrationHousekeepingService
{
    class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            DefaultSkeepyApiBuildingBlocks.RegisterWithTinyIoc();
            TinyIoCContainer.Current.Register(typeof(HousekeepingDaemon)).AsSingleton();

            TinyIoCContainer.Current.Resolve<HousekeepingDaemon>().Start();

            log.Info("Registration Housekeeping Service running...");
            Console.ReadLine();

            TinyIoCContainer.Current.Resolve<HousekeepingDaemon>().Stop();
        }
    }
}
