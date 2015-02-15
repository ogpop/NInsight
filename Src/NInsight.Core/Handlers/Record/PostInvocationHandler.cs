using Castle.DynamicProxy;

using Newtonsoft.Json;

using NInsight.Core.Context;
using NInsight.Core.Domain;
using NInsight.Core.Repositories;

namespace NInsight.Core.Handlers.Record
{
    internal class PostInvocationHandler
    {
        public INodeRepository NodeRepository { get; set; }

        public IGenericRepository<Point> PointRepository { get; set; }

        public void Handle(IInvocation invocation)
        {
            var point = RecordContext.Current.CurrentPoint;
            var result = invocation.ReturnValue;
            var jsonResult = JsonConvert.SerializeObject(result);
            var returnParameter = new Parameter
                                  {
                                      PointId = point.PointId,
                                      Name = string.Empty,
                                      IsReturn = true,
                                      TypeFullName = (invocation.Method).ReturnType.AssemblyQualifiedName,
                                      Value = jsonResult
                                  };
            point.ReturnValue = returnParameter;

            this.PointRepository.Add(point);
            this.NodeRepository.AddPoint(point);
            

            RecordContext.Current.EndInvocation();
        }
    }
}