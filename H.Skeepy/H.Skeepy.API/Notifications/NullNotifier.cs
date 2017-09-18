using H.Skeepy.API.Contracts.Notifications;
using NLog;
using System.Threading.Tasks;

namespace H.Skeepy.API.Notifications
{
    public class NullNotifier : ICanNotify
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public void Dispose()
        {

        }

        public Task Notify(NotificationDestination destination, string summary, string content)
        {
            log.Info($"Notification to {destination} about \"{summary}\" would be sent by an actual notifier");
            return Task.CompletedTask;
        }
    }
}
