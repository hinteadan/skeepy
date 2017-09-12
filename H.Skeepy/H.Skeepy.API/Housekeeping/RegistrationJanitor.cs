using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Housekeeping
{
    public class RegistrationJanitor : ImAJanitor
    {
        public Task Clean()
        {
            return Task.CompletedTask;
        }
    }
}
