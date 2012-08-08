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
                    rc.Configuration.LazyLoadingEnabled = true;
                    rc.Configuration.ProxyCreationEnabled = true;
                    command.Apply(rc);                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
#if DEBUG
                Console.WriteLine(ex.StackTrace);
                var ex1 = ex.InnerException;
                while (ex1 != null)
                {
                    Console.WriteLine(ex1.Message);
                    Console.WriteLine(ex1.StackTrace);
                    ex1 = ex1.InnerException;
                }
#endif            
            }
        }
    }
}
