using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Args;
using CloudDeploy.Persistence.Contexts;

namespace CloudDeploy.Clients.ConsoleApp
{
    public class CommandObject
    {
        [ArgsMemberSwitch(0)]
        public string Action { get; set; }
        [ArgsMemberSwitch(1)]
        public string Noun { get; set; }
        
        public string Arguments { get; set; }



        public void Apply(ReleaseContext rc)
        {
            switch (Noun)
            {
                case "host":
                case "hosts":

                    var arguments = Arguments != null ? Arguments.Split(' ') : new string[] { "" };

                    switch (Action)
                    {
                        case "list":
                            rc.Hosts.ToList().ForEach(h => Console.WriteLine(h.ToString()));
                            break;

                        case "add":
                            if (arguments.Length != 3) throw new ArgumentException("Requires 3 arguments [HostName] [HostRole] [Environment]");
                            rc.AddHost(arguments[0], arguments[1], arguments[2]);
                            Console.WriteLine("Host added");
                            break;

                        case "update":


                            break;

                        case "delete":
                            rc.DeleteHost(arguments[0]);
                            Console.WriteLine("Host deleted");
                            break;
                    }
                    break;

                case "build":

                    break;
                case "deploymentunit":

                    break;

                case "releasepackage":

                    break;

                case "arteface":

                    break;

            }
        }







    }
}
