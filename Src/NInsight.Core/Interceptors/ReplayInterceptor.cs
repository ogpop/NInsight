using Castle.DynamicProxy;

using NInsight.Core.Handlers.Replay;

namespace NInsight.Core.Interceptors
{
    internal class ReplayInterceptor : IInterceptor
    {
        internal PreInvocationHandler PreInvocationHandler { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (this.PreInvocationHandler.Handle(invocation))
            {
                invocation.Proceed();
            }
        }
    }
}