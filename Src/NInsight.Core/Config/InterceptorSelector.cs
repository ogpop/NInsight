using System.Linq;

using Castle.Core;
using Castle.MicroKernel.Proxy;

namespace NInsight.Core.Config
{
    public class InterceptorSelector : IModelInterceptorsSelector
    {
        public bool HasInterceptors(ComponentModel model)
        {
            return model.Interceptors.Any();
        }

        public InterceptorReference[] SelectInterceptors(ComponentModel model, InterceptorReference[] interceptors)
        {
            return interceptors;
        }
    }
}