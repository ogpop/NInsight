using Castle.MicroKernel.Registration;
using Castle.Windsor;

using NInsight.Core.Config;
using NInsight.Persistence;

namespace BankAccount
{
    internal class Program
    {
        private static IWindsorContainer container;

        private static void Main(string[] args)
        {
            container = new WindsorContainer();
            container.Register(
                Classes.FromThisAssembly().Where(p => true).WithService.DefaultInterfaces().LifestyleTransient());
            ConfigureNInsight();

            container.Resolve<ITransferHandler>()
                .Handle(new TransferCommand { DebitAccountId = "acc1", CreditAccountId = "acc2", Amount = 500 });
        }

        private static void ConfigureNInsight()
        {
            Configuration.Configure.DefiningStartpointAs(t => t.Name.EndsWith("Handler"));
            Configuration.Configure.DefiningEndpointAs(t => t.Name.EndsWith("Repository"));

            container.Install(new Installer());
            Configuration.Configure.AddEntityFrameworkPersiting();

            //Configuration.Configure.AddNeo4jPersiting();
        }
    }
}