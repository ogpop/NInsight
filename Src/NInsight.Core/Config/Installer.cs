using System;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Common.Logging;

using Neo4jClient;

using NInsight.Core.Domain;
using NInsight.Core.Interceptors;
using NInsight.Core.Repositories.EF;

namespace NInsight.Core.Config
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                Configuration.Configure.Container = container;
                Configuration.Configure.Container.Register(
                    Classes.FromThisAssembly()
                        .IncludeNonPublicTypes()
                        .Where(p => p.Namespace != null && p.Namespace.StartsWith("BeyondTest"))
                        .WithService.DefaultInterfaces()
                        .LifestyleTransient());
               
                Configuration.Configure.Container.Register(
                    Component.For<IGenericRepository<Application>>()
                        .Instance(new GenericRepository<Application>(new BeyondTestContext())));
                Configuration.Configure.Container.Register(
                    Component.For<IGenericRepository<Point>>()
                        .Instance(new GenericRepository<Point>(new BeyondTestContext())));
                Configuration.Configure.Container.Register(
                    Component.For<RecordInterceptor>().Named("RecordInterceptor").LifestyleTransient());
                Configuration.Configure.Container.Register(
                   Component.For<ReplayInterceptor>().Named("ReplayInterceptor").LifestyleTransient());

                Configuration.Configure.Container.Kernel.ProxyFactory.AddInterceptorSelector(new InterceptorSelector());


                new InterceptorInstaller().Do(container);


                if (NInsightSettings.Settings.Neo4j.Use)
                {
                      var graphClient = new GraphClient(new Uri(NInsightSettings.Settings.Neo4j.Url));
                      graphClient.Connect();
                      Configuration.Configure.Container.Register(
                  Component.For<GraphClient>().Instance(graphClient));
                     
                }


            }
            catch (Exception ex)
            {
                var log = LogManager.GetCurrentClassLogger();
                log.Error(ex);
            }
        }

        
    }
}