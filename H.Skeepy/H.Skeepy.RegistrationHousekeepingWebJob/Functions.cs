using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using H.Skeepy.API.Housekeeping;
using Nancy.TinyIoc;
using H.Skeepy.API.Contracts.Housekeeping;

namespace H.Skeepy.RegistrationHousekeepingWebJob
{
    public class Functions
    {
        private static readonly IEnumerable<ImAJanitor> janitors;

        static Functions()
        {
            janitors = TinyIoCContainer.Current.Resolve<IEnumerable<ImAJanitor>>();
        }

        public static void RunHousekeeping([TimerTrigger("00:30:00", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log)
        {
            log.WriteLine($"Running Housekeeping on Registration Web App @ {DateTime.Now}");
            Task.WaitAll(janitors.Select(x => x.Clean()).ToArray());
            log.WriteLine($"Finished Running Housekeeping on Registration Web App @ {DateTime.Now}");
        }
    }
}
