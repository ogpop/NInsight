using Castle.Core;
using Castle.Windsor;

namespace NInsight.Core.Config
{
     internal class InterceptorInstaller
    {
         public void Do(IWindsorContainer container)
         {
             InterceptorReference interceptor = null;

             if (NInsightSettings.Settings.Record)
             {
                 interceptor = InterceptorReference.ForKey("RecordInterceptor");
             }
             else
             {
                 interceptor = InterceptorReference.ForKey("ReplayInterceptor");
             }
             foreach (var handler in container.Kernel.GetAssignableHandlers(typeof(object)))
             {
                 //if (Configuration.Configure.Conventions.IsEndpointType(handler.ComponentModel.Implementation)
                 //    || Configuration.Configure.Conventions.IsStartpointType(handler.ComponentModel.Implementation)
                 //    || Configuration.Configure.Conventions.IsTracepointType(handler.ComponentModel.Implementation))
                 //{
                 //   // handler.ComponentModel.Interceptors.BeginInvocation(interceptor);
                 //}
                 //else
                 {
                     foreach (var service in handler.ComponentModel.Services)
                     {
                         if (Configuration.Configure.Conventions.IsEndpointType(service)
                             || Configuration.Configure.Conventions.IsStartpointType(service)
                             || Configuration.Configure.Conventions.IsTracepointType(service))
                         {
                             
                            if (service.IsInterface )
                             {
                                 handler.ComponentModel.Interceptors.Add(interceptor);
                             }
                         }
                     }
                 }
             }
         }
    }
}
