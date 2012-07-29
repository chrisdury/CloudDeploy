using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Releases;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Tests.Initial
{
    internal class BaseModelTest
    {
        protected List<Build> Builds { get; set; }
        protected List<DeploymentTarget> DeploymentTargets { get; set; }
        protected List<DeploymentUnit> DeploymentUnits { get; set; }
        protected List<DeployableArtefact> DeployableArtefacts { get; set; }


        protected void PopulateTestData()
        {
            Builds = new List<Build>()
            {
                new Build() { BuildDate = DateTime.Now.AddDays(-10), BuildID = Guid.NewGuid(), BuildLabel="1.0.100.0", BuildName="1.0.100.0", DropLocation="c:\\builds"},
                new Build() { BuildDate = DateTime.Now.AddDays(-9), BuildID = Guid.NewGuid(), BuildLabel="1.0.200.0", BuildName="1.0.200.0", DropLocation="c:\\builds"},
                new Build() { BuildDate = DateTime.Now.AddDays(-8), BuildID = Guid.NewGuid(), BuildLabel="1.0.300.0", BuildName="1.0.300.0", DropLocation="c:\\builds"},
                new Build() { BuildDate = DateTime.Now.AddDays(-7), BuildID = Guid.NewGuid(), BuildLabel="1.0.400.0", BuildName="1.0.400.0", DropLocation="c:\\builds"},
                new Build() { BuildDate = DateTime.Now.AddDays(-6), BuildID = Guid.NewGuid(), BuildLabel="1.0.500.0", BuildName="1.0.500.0", DropLocation="c:\\builds"},
                new Build() { BuildDate = DateTime.Now.AddDays(-5), BuildID = Guid.NewGuid(), BuildLabel="1.0.600.0", BuildName="1.0.600.0", DropLocation="c:\\builds"}
            };

            DeploymentTargets = new List<DeploymentTarget>()
            {
                new DeploymentTarget() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = new Platform.PlatformEnvironment() { Name = "TEST" },
                    HostName="TESTWBRTDB",
                    HostRoles = new List<Platform.HostRole>() { new HostRole("SQL Server")}
                },
                new DeploymentTarget() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = new Platform.PlatformEnvironment() { Name = "TEST" },
                    HostName="TESTWBRTSP",
                    HostRoles = new List<Platform.HostRole>() { new HostRole("SharePoint")}
                },
                new DeploymentTarget() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = new Platform.PlatformEnvironment() { Name = "TEST" },
                    HostName="TESTWBRTCRM",
                    HostRoles = new List<Platform.HostRole>() { new HostRole("CRM")}
                },
                new DeploymentTarget() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = new Platform.PlatformEnvironment() { Name = "TEST" },
                    HostName="CHRISWBRT",
                    HostRoles = new List<Platform.HostRole>() { new HostRole("SQL Server"), new HostRole("SharePoint"), new HostRole("CRM")}
                }
            };

            DeployableArtefacts = new List<DeployableArtefact>()
            {
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Configuration", FileName="WBRT.Configuration.msi", HostRole = new HostRole("ALL"), InstallationScript="msiexec /i wbrt.configuration.msi", RollbackScript="msiexec /x wbrt.configuration.msi"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Security.AccessControl", FileName="WBRT.Security.AccessControl.msi", HostRole = new HostRole("SQL Server"), InstallationScript="WBRT.Security.AccessControl", RollbackScript="Uninstall WBRT.Security.AccessControl"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Security.AccessControl.DB", FileName="WBRT.Security.AccessControl.DB", HostRole = new HostRole("SQL Server"), InstallationScript="asfdasfd", RollbackScript="asdfasdf"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="SPClaimProvider", FileName="WBRT.Security.AccessControl.SPClaimProvider.wsp", HostRole = new HostRole("SharePoint"), InstallationScript="", RollbackScript=""},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.HealthServices.HealthServiceInfo", FileName="WBRT.HealthServices.HealthServiceInfo.msi", HostRole = new HostRole("CRM"), InstallationScript="", RollbackScript=""},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="ProgramReportSubmissionProxy", FileName="ProgramReportSubmissionProxy", HostRole = new HostRole("SharePoint"), InstallationScript="", RollbackScript=""}
            };



        }

    }
}
