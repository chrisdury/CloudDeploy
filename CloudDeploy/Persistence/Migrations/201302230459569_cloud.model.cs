namespace CloudDeploy.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cloudmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReleasePackages", "ReleaseStatus", c => c.Int(nullable: false));
            AddColumn("dbo.DeploymentUnits", "ReleaseStatus", c => c.Int(nullable: false));
            AddColumn("dbo.HostDeployments", "ReleaseStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HostDeployments", "ReleaseStatus");
            DropColumn("dbo.DeploymentUnits", "ReleaseStatus");
            DropColumn("dbo.ReleasePackages", "ReleaseStatus");
        }
    }
}
