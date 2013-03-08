using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDeploy.Model.Agent;

namespace CloudDeploy.Clients.RemoteAgent
{
    class CommandLine
    {
        static void Main(string[] args)
        {
            Trace.WriteLine("Initialising");
            var self = new Agent();
            self.Host = new Model.Platform.Host() { Environment = "TEST", HostName = "TESTWBRTSP", HostRole = "SharePoint" };

            Trace.WriteLine(String.Format("RemoteAgent for: {0} in environment: {1}", self.Host.HostName, self.Host.Environment));

            Trace.WriteLine("Checking for work to do");

            // TODO: everything!



            Console.ReadKey();

        }


    }
}
