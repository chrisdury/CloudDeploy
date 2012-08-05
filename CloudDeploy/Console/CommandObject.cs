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
        public ActionEnum Action { get; set; }
        public enum ActionEnum { List, Add, Update, Delete };

        [ArgsMemberSwitch(1)]
        public NounEnum Noun { get; set; }
        public enum NounEnum { Host, Hosts, Build, Builds, Artefact, Artefacts, Package, Packages, DeploymentUnit, DeploymentUnits }
        
        public string Arguments { get; set; }

        public void Apply(ReleaseContext rc)
        {
            var arguments = Arguments != null ? Arguments.Split(' ') : new string[] { "" };

            switch (Noun)
            {
                case NounEnum.Host:
                case NounEnum.Hosts:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.Hosts.ToList().ForEach(h => Console.WriteLine(h.ToString()));
                            break;

                        case ActionEnum.Add:
                            if (arguments.Length != 3) throw new ArgumentException("Requires 3 arguments [HostName] [HostRole] [Environment]");
                            rc.AddHost(arguments[0], arguments[1], arguments[2]);
                            Console.WriteLine("Host added");
                            break;

                        case ActionEnum.Update:
                            throw new NotImplementedException();
                            break;

                        case ActionEnum.Delete:
                            rc.DeleteHost(arguments[0]);
                            Console.WriteLine("Host deleted");
                            break;
                    }
                    break;

                case NounEnum.Build:
                case NounEnum.Builds:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.Builds.ToList().ForEach(b => Console.WriteLine(b.ToString()));
                            break;

                        case ActionEnum.Add:
                            if (arguments.Length != 3) throw new ArgumentException("Requires 3 arguments [BuildLabel] [BuildDate] [DropLocation]");
                            rc.AddBuild(arguments[0], DateTime.Parse(arguments[1]), arguments[2]);
                            Console.WriteLine("Build added");
                            break;

                        case ActionEnum.Update:
                            throw new NotImplementedException();
                            break;

                        case ActionEnum.Delete:
                            if (arguments.Length != 1) throw new ArgumentException("Requires 1 arguments [BuildLabel]");
                            rc.DeleteBuild(arguments[0]);
                            Console.WriteLine("Build deleted");
                            break;
                    }
                    break;

                case NounEnum.DeploymentUnit:
                case NounEnum.DeploymentUnits:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.DeploymentUnits.Include("DeployableArtefact").Include("Build").ToList().ForEach(du => Console.WriteLine(du));
                            break;
                        case ActionEnum.Add:
                            if (arguments.Length != 2) throw new ArgumentException("Requires 2 arguments [ArtefactName] [BuildLabel]");
                            rc.AddDeploymentUnit(arguments[0], arguments[1]);
                            Console.WriteLine("Deployment Unit added");
                            break;
                        case ActionEnum.Update:
                            break;                        
                        case ActionEnum.Delete:
                            break;
                    }
                    break;

                case NounEnum.Package:
                case NounEnum.Packages:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            break;
                        case ActionEnum.Add:
                            break;
                        case ActionEnum.Update:
                            break;
                        case ActionEnum.Delete:
                            break;
                    }
                    break;

                case NounEnum.Artefact:
                case NounEnum.Artefacts:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.DeployableArtefacts.ToList().ForEach(da => Console.WriteLine(da));
                            break;
                        case ActionEnum.Add:
                            if (arguments.Length != 3) throw new ArgumentException("Requires 3 arguments [ArtefactName] [FileName] [HostRole]");
                            rc.AddArtefact(arguments[0], arguments[1], arguments[2]);
                            Console.WriteLine("Artefact added");
                            break;
                        case ActionEnum.Update:
                            throw new NotImplementedException();
                            break;
                        case ActionEnum.Delete:
                            if (arguments.Length != 1) throw new ArgumentException("Requires 1 arguments [ArtefactName]");
                            rc.DeleteArtefact(arguments[0]);
                            Console.WriteLine("Artefact deleted");
                            break;
                    }
                    break;

            }
        }







    }
}
