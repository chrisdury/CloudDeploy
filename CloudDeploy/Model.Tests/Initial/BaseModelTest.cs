using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Releases;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Tests.Initial
{
    public class BaseModelTest
    {
        public List<Build> Builds { get; set; }
        public List<Host> Hosts { get; set; }
        public List<DeploymentUnit> DeploymentUnits { get; set; }
        public List<DeployableArtefact> DeployableArtefacts { get; set; }


        public void PopulateTestData()
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

            Hosts = new List<Host>()
            {
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "TEST",
                    HostName="TESTWBRTDB",
                    HostRole = "SQL Server"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "TEST",
                    HostName="TESTWBRTSP",
                    HostRole = "SharePoint"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "TEST",
                    HostName="TESTWBRTCRM",
                    HostRole = "CRM"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "DEV",
                    HostName="CHRISWBRT",
                    HostRole = "SQL Server,SharePoint,CRM"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "STAGE",
                    HostName="OBSIFASTGSQL01",
                    HostRole = "SQL Server"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "STAGE",
                    HostName="OBSIFASTGSPS01",
                    HostRole = "SharePoint,CRM"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "PROD",
                    HostName="OBSIFASQL01",
                    HostRole = "SQL Server"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "PROD",
                    HostName="OBSIFASPS01",
                    HostRole = "SharePoint"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "PROD",
                    HostName="OBSIFASPS02",
                    HostRole = "SharePoint"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "PROD",
                    HostName="OBSIFACRM01",
                    HostRole = "CRM"
                },
                new Host() { 
                    DeploymentTargetID = Guid.NewGuid(), 
                    Environment = "PROD",
                    HostName="OBSIFACRM02",
                    HostRole = "CRM"
                },

                
                
            };

            DeployableArtefacts = new List<DeployableArtefact>()
            {
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Configuration", FileName="WBRT.Configuration.msi", HostRole = "ALL", InstallationScript="msiexec /i wbrt.configuration.msi", RollbackScript="msiexec /x wbrt.configuration.msi"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Security.AccessControl", FileName="WBRT.Security.AccessControl.msi", HostRole = "SQL Server", InstallationScript="WBRT.Security.AccessControl", RollbackScript="Uninstall WBRT.Security.AccessControl"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.Security.AccessControl.DB", FileName="WBRT.Security.AccessControl.DB", HostRole = "SQL Server", InstallationScript="asfdasfd", RollbackScript="asdfasdf"},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="SPClaimProvider", FileName="WBRT.Security.AccessControl.SPClaimProvider.wsp", HostRole = "SharePoint", InstallationScript="", RollbackScript=""},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="WBRT.HealthServices.HealthServiceInfo", FileName="WBRT.HealthServices.HealthServiceInfo.msi", HostRole = "CRM", InstallationScript="", RollbackScript=""},
                new DeployableArtefact() { DeployableArtefactID = Guid.NewGuid(), DeployableArtefactName="ProgramReportSubmissionProxy", FileName="ProgramReportSubmissionProxy", HostRole = "SharePoint", InstallationScript="", RollbackScript=""}
            };



        }

    }
}
