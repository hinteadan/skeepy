using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Clients.Web.RegistrationWebApp
{
    public class ViewModel
    {
        public static ViewModel None = new ViewModel();
        public static ViewModel<T> With<T>(T payload)
        {
            return new ViewModel<T>(payload);
        }

        public string VersionNumber
        {
            get
            {
                return Versioning.Version.Self.GetCurrent().Number.ToString();
            }
        }

        public string Version
        {
            get
            {
                return Versioning.Version.Self.GetCurrent().ToString();
            }
        }
    }

    public class ViewModel<T> : ViewModel
    {
        public ViewModel(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; }
    }
}
