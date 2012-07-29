using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CloudDeploy.Persistence.Contexts;
using CloudDeploy.Model.Releases;

namespace CloudDeploy.Persistence.Initialisers
{
    public class ReleaseContextInitialiser : System.Data.Entity.IDatabaseInitializer<ReleaseContext>
    {
        public void InitializeDatabase(ReleaseContext context)
        {
            var releaseStatuses = new List<ReleaseStatus>()
            {
                new ReleaseStatus() { ReleaseStatusId = new Guid(""), ReleaseStatusName ="Pending"},
                new ReleaseStatus() { ReleaseStatusId = new Guid(""), ReleaseStatusName = "InProgress"},
                new ReleaseStatus() { ReleaseStatusId = new Guid(""), ReleaseStatusName = "Complete"},
                new ReleaseStatus() { ReleaseStatusId = new Guid(""), ReleaseStatusName = "Failed"},
                new ReleaseStatus() { ReleaseStatusId = new Guid(""), ReleaseStatusName = "Rollback"}
            };
            releaseStatuses.ForEach(r => context.ReleaseStatuses.Add(r));

        }
    }
}
