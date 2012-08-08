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
        public enum ActionEnum { List, Add, Update, Delete, AddArtefact, RemoveArtefact, Deploy };

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
                            //rc.Hosts.ToList().ForEach(h => Console.WriteLine(h.ToString()));
                            rc.GetHosts().ToList().ForEach(h => Console.WriteLine(h));
                            break;

                        case ActionEnum.Add:
                            CheckArguments(arguments, "HostName", "HostRole", "Environment");
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
                        default:
                            throw new Exception("Action not valid for this Noun");
                    }
                    break;

                case NounEnum.Build:
                case NounEnum.Builds:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.GetBuilds().ToList().ForEach(b => Console.WriteLine(b.ToString()));
                            break;

                        case ActionEnum.Add:
                            CheckArguments(arguments, "BuildLabel", "BuildDate", "DropLocation");
                            rc.AddBuild(arguments[0], DateTime.Parse(arguments[1]), arguments[2]);
                            Console.WriteLine("Build added");
                            break;

                        case ActionEnum.Update:
                            throw new NotImplementedException();
                            break;

                        case ActionEnum.Delete:
                            CheckArguments(arguments, "BuildLabel");
                            rc.DeleteBuild(arguments[0]);
                            Console.WriteLine("Build deleted");
                            break;
                        default:
                            throw new Exception("Action not valid for this Noun");
                    }
                    break;

                case NounEnum.DeploymentUnit:
                case NounEnum.DeploymentUnits:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            //rc.DeploymentUnits.Include("DeployableArtefact").Include("Build").ToList().ForEach(du => Console.WriteLine(du));
                            break;
                        case ActionEnum.Add:
                            CheckArguments(arguments, "ArtefactName", "BuildLabel");
                            rc.AddDeploymentUnit(arguments[0], arguments[1]);
                            Console.WriteLine("Deployment Unit added");
                            break;
                        case ActionEnum.Update:
                            break;                        
                        case ActionEnum.Delete:
                            break;
                        default:
                            throw new Exception("Action not valid for this Noun");
                    }
                    break;

                case NounEnum.Package:
                case NounEnum.Packages:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.GetReleasePackages().ToList().ForEach(rp => Console.WriteLine(rp));
                            break;
                        case ActionEnum.Add:
                            CheckArguments(arguments, "ReleaseName", "ReleaseDate", "Environment");
                            var releasePackage = rc.AddReleasePackage(arguments[0], DateTime.Parse(arguments[1]), arguments[2]);
                            Console.WriteLine("Added Release Package: " + releasePackage);
                            break;

                        case ActionEnum.AddArtefact:
                            CheckArguments(arguments, "PackageName", "ArtefactName", "BuildLabel");
                            var deploymentUnit = rc.AddArtefactToPackage(arguments[0], arguments[1], arguments[2]);
                            Console.WriteLine("Added Deployment Unit:" + deploymentUnit.ToString());
                            break;

                        case ActionEnum.RemoveArtefact:
                            CheckArguments(arguments, "packageName", "artefactName");
                            rc.RemoveArtefactFromPackage(arguments[0], arguments[1]);
                            Console.WriteLine("Artefact removed");
                            break;

                        case ActionEnum.Deploy:
                            CheckArguments(arguments, "PackageName", "EnvironmentName");
                            var releasingPackage = rc.DeployPackageToEnvironment(arguments[0], arguments[1]);
                            Console.WriteLine(releasingPackage);
                            break;
                        default:
                            throw new Exception("Action not valid for this Noun");
                    }
                    break;

                case NounEnum.Artefact:
                case NounEnum.Artefacts:
                    switch (Action)
                    {
                        case ActionEnum.List:
                            rc.GetDeployableArtefacts().ToList().ForEach(da => Console.WriteLine(da));
                            break;
                        case ActionEnum.Add:
                            CheckArguments(arguments, "ArtefactName", "FileName", "HostRole");
                            rc.AddArtefact(arguments[0], arguments[1], arguments[2]);
                            Console.WriteLine("Artefact added");
                            break;
                        case ActionEnum.Update:
                            throw new NotImplementedException();
                            break;
                        case ActionEnum.Delete:
                            CheckArguments(arguments, "ArtefactName");
                            rc.DeleteArtefact(arguments[0]);
                            Console.WriteLine("Artefact deleted");
                            break;
                        default:
                            throw new Exception("Action not valid for this Noun");
                    }
                    break;

            }
        }




        private void CheckArguments(string[] args, params string[] expectedArgs)
        {
            if (args.Length != expectedArgs.Length)
            {
                var argsList = String.Join(" ",expectedArgs);
                throw new ArgumentException("Expecting exactly " + expectedArgs.Length + " arguments: " + argsList);
            }
        }

    }
}
