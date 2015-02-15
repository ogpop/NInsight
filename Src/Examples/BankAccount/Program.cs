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


            Container.Resolve<DepositHandler>().Handle( new DepositCommand
                                                        {
                                                            AccountId = "acc1", 
                                                            Amount = 500
                                                        });

        }
        private static void ConfigureNInsight()
        {
            Configuration.Configure.DefiningTracepointAs(t => true);
            Container.Install(new Installer());
            Configuration.Configure.AddPersiting();
        }
    }
}
