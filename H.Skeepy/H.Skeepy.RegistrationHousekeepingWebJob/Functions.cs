using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.Logging;
using Microsoft.Azure.WebJobs;
using Nancy.TinyIoc;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace H.Skeepy.RegistrationHousekeepingWebJob
{
    public class Functions
    {
        private static Logger nlog = LogManager.GetCurrentClassLogger();

        private static readonly IEnumerable<ImAJanitor> janitors;

        static Functions()
        {
            janitors = TinyIoCContainer.Current.Resolve<IEnumerable<ImAJanitor>>();
        }

        public static void RunHousekeeping([TimerTrigger("00:30:00", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log)
        {
            using (nlog.Timing("Full Housekeeping", LogLevel.Info))
            {
                log.WriteLine($"Running Housekeeping on Registration Web App @ {DateTime.Now}");
                Task.WaitAll(janitors.Select(x => x.Clean()).ToArray());
                log.WriteLine($"Finished Running Housekeeping on Registration Web App @ {DateTime.Now}");
            }
        }
    }
}
