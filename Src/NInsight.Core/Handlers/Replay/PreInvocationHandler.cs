using System.Linq;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using NInsight.Core.Config;
using NInsight.Core.Context;
using NInsight.Core.Mappers;

namespace NInsight.Core.Handlers.Replay
{
    internal class PreInvocationHandler
    {
        public bool Handle(IInvocation invocation)
        {
            var run = ReplayContext.Run;
            var pointHashKey = new InvocationHasher().Do(invocation);

            if (Configuration.Configure.Conventions.IsEndpointType(invocation.InvocationTarget.GetType()))
            {
                var retParam = run.Points.FirstOrDefault(p => p.HashKey == pointHashKey).ReturnValue;
                invocation.ReturnValue = JsonConvert.DeserializeObject(retParam.Value, retParam.GetType());
                return false;
            }
            return true;
        }
    }
}