using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Contracts.Notifications
{
    public interface ICanNotify : IDisposable
    {
        Task Notify(NotificationDestination destination, string summary, string content);
    }
}
