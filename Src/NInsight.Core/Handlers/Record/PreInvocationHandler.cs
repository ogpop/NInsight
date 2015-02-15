using Castle.DynamicProxy;

using NInsight.Core.Context;
using NInsight.Core.Mappers;

namespace NInsight.Core.Handlers.Record
{
    internal class PreInvocationHandler
    {
        public void Handle(IInvocation invocation)
        {
            var point = new PointMapper().Do(invocation);
            RecordContext.Current.BeginInvocation(point);
        }
    }
}