using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common.Logging;
using NInsight.Core.Interceptors;

namespace NInsight.Core.Config
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            try
            {
                var log = LogManager.GetLogger(this.GetType());
                Configuration.Configure.Container = container;
                Configuration.Configure.Container.Register(
                    Classes.FromThisAssembly()
                        .IncludeNonPublicTypes()
                        .Where(p => p.Namespace != null && p.Namespace.StartsWith("NInsight"))
                        .WithService.DefaultInterfaces()
                        .LifestyleTransient());

                Configuration.Configure.Container.Register(
                      Component.For<ILog>().Instance(log));
                
                Configuration.Configure.Container.Register(
                    Component.For<RecordInterceptor>().Named("RecordInterceptor").LifestyleTransient());
                
                
                Configuration.Configure.Container.Register(
                   Component.For<ReplayInterceptor>().Named("ReplayInterceptor").LifestyleTransient());

                Configuration.Configure.Container.Kernel.ProxyFactory.AddInterceptorSelector(new InterceptorSelector());


                new InterceptorInstaller().Do(container);


            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger(this.GetType());
                log.Error(ex);
            }
        }

        
    }
}