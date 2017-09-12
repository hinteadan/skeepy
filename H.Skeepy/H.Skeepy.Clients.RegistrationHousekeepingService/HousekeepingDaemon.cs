using H.Skeepy.API.Housekeeping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace H.Skeepy.Clients.RegistrationHousekeepingService
{
    internal class HousekeepingDaemon
    {
        private readonly IEnumerable<ImAJanitor> janitors;
        private readonly Timer timer = new Timer() { AutoReset = false, Interval = 5 * 1000 };

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
            Task.WaitAll(janitors.Select(x => x.Clean()).ToArray());
        }

        public HousekeepingDaemon Start()
        {
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
