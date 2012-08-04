using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Persistence.Contexts;
using System.Data.Entity;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Clients.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var command = Args.Configuration.Configure<CommandObject>().CreateAndBind(args);
                using (var rc = new ReleaseContext())
                {
                    command.Apply(rc);                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
