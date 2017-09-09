using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Notifications
{
    public class NullNotifier : ICanNotify
    {
        public void Dispose()
        {
            
        }

        public Task Notify(NotificationDestination destination, string summary, string content)
        {
            return Task.CompletedTask;
        }
    }
}
