using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.Clients.Web.Hosts.Self.RegistrationWebApp
{
    class Program
    {
        private static readonly Uri hostUri = new Uri("http://localhost:9901");

        static void Main(string[] args)
        {
            using (var host = new NancyHost(hostUri))
            {
                host.Start();
                Console.WriteLine($"Running on {hostUri}");
                Process.Start(hostUri.ToString());
                Process.Start(".\\H.Skeepy.Clients.RegistrationHousekeepingService.exe");
                Console.ReadLine();
            }
        }
    }
}
