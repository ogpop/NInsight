using Castle.DynamicProxy;

using NInsight.Core.Handlers.Record;

namespace NInsight.Core.Interceptors
{
    internal class RecordInterceptor : IInterceptor
    {
        public PreInvocationHandler PreInvocationHandler { get; set; }

        public PostInvocationHandler PostInvocationHandler { get; set; }

        public void Intercept(IInvocation invocation)
        {
            this.PreInvocationHandler.Handle(invocation);
            invocation.Proceed();
            this.PostInvocationHandler.Handle(invocation);
        }
    }
}