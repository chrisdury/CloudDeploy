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
        public enum ActionEnum { List, Add, Update, Delete, AddArtefact, RemoveArtefact, Deploy, Confirm, Rollback };

        [ArgsMemberSwitch(1)]
        public NounEnum Noun { get; set; }
        public enum NounEnum { Host, Hosts, Build, Builds, Artefact, Artefacts, Package, Packages, DeploymentUnit, DeploymentUnits }
        
        public string Arguments { get; set; }
        private string[] arguments 
        { 
            get
            {
                return Arguments != null ? Arguments.Split(' ') : new string[] { "" };
            }
        }

        public void Apply(ReleaseContext rc)
        {
            switch (Noun)
            {
                case NounEnum.Host:
                case NounEnum.Hosts:
                    HostAction(rc);
                    break;

                case NounEnum.Build:
                case NounEnum.Builds:
                    HostAction(rc);
                    break;

                case NounEnum.DeploymentUnit:
                case NounEnum.DeploymentUnits:
                    DeploymentUnitAction(rc);
                    break;

                case NounEnum.Package:
                case NounEnum.Packages:
                    PackageAction(rc);
                    break;

                case NounEnum.Artefact:
                case NounEnum.Artefacts:
                    ArtefactAction(rc);
                    break;
            }
        }

        private void HostAction(ReleaseContext rc)
        {
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
        }

        private void BuildAction(ReleaseContext rc)
        {
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
        }

        private void DeploymentUnitAction(ReleaseContext rc)
        {
            throw new Exception("Directly manipulating Deployment Units is not allowed!");
        }

        private void PackageAction(ReleaseContext rc)
        {
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
        }

        private void ArtefactAction(ReleaseContext rc)
        {
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
