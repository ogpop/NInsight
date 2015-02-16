using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using NInsight.Core.Config;
using NInsight.Persistence;

namespace BankAccount
{
    class Program
    {
        private static IWindsorContainer Container;
        static void Main(string[] args)
        {

            Container = new WindsorContainer();
            Container.Register(
                Classes.FromThisAssembly()
                    .Where(p => true)
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient());
            ConfigureNInsight();


            Container.Resolve<ITransferHandler>().Handle( new TransferCommand
                                                        {
                                                            DebitAccountId = "acc1",
                                                            CreditAccountId = "acc2", 
                                                            Amount = 500
                                                        });

        }
        private static void ConfigureNInsight()
        {
            Configuration.Configure.DefiningStartpointAs(t => t.Name.EndsWith("Handler"));
            Configuration.Configure.DefiningEndpointAs(t => t.Name.EndsWith("Repository"));
           
            Container.Install(new Installer());
            //Configuration.Configure.AddEntityFrameworkPersiting();

            Configuration.Configure.AddNeo4jPersiting();


        }
    }
}
