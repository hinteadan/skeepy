using H.Skeepy.API.Contracts.Housekeeping;
using H.Skeepy.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace H.Skeepy.Clients.RegistrationHousekeepingService
{
    internal class HousekeepingDaemon
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly IEnumerable<ImAJanitor> janitors;
        private readonly Timer timer = new Timer() { AutoReset = false, Interval = 10 * 60 * 1000 };

        public HousekeepingDaemon(IEnumerable<ImAJanitor> janitors)
        {
            this.janitors = janitors ?? throw new InvalidOperationException($"Must provide {nameof(janitors)}");
            timer.Elapsed += (sender, e) => RunJanitorsAndQueueAnother();
            timer.Start();
        }

        private void RunJanitorsAndQueueAnother()
        {
            RunJanitors();
            timer.Start();
        }

        private void RunJanitors()
        {
            using (log.Timing("Full Housekeeping", LogLevel.Info))
            {
                Task.WaitAll(janitors.Select(x => x.Clean()).ToArray());
            }
        }

        public HousekeepingDaemon Start()
        {
            RunJanitorsAndQueueAnother();
            return this;
        }

        public HousekeepingDaemon Stop()
        {
            timer.Stop();
            timer.Dispose();
            return this;
        }
    }
}
