using System;
using System.Collections.Generic;
using System.Linq;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using NInsight.Core.Config;
using NInsight.Core.Context;
using NInsight.Core.Domain;

namespace NInsight.Core.Mappers
{
    internal class PointMapper
    {
        public Point Do(IInvocation invocation)
        {
            var classType = new ClassType
                            {
                                RunId = RecordContext.Current.Run.Id,
                                TypeFullName =
                                    RegisteredType(invocation.InvocationTarget.GetType())
                                    .AssemblyQualifiedName,
                                FriendlyName =
                                    RegisteredType(invocation.InvocationTarget.GetType()).FullName
                            };
            var point = new Point
                        {
                            TypeFullName =
                                RegisteredType(invocation.InvocationTarget.GetType()).AssemblyQualifiedName,
                            FriendlyName =
                                string.Format(
                                    "{0}.{1}",
                                    RegisteredType(invocation.InvocationTarget.GetType()).FullName,
                                    invocation.Method.Name),
                            MethodName = invocation.Method.Name,
                            Run1Id = RecordContext.Current.Run.Id,
                            Class1Id = classType.ClassId,
                            Class = classType
                        };

            point.Parameters = CreateParameterList(invocation, point.PointId);
            point.Parameters.ForEach(p => p.PointId = point.PointId);
            point.HashKey = new InvocationHasher().Do(invocation);
            if (Configuration.Configure.Conventions.IsStartpointType(invocation.InvocationTarget.GetType()))
            {
                point.IsStartPoint = true;
            }
            return point;
        }

        public static List<Parameter> CreateParameterList(IInvocation invocation, Guid pointId)
        {
            var method = invocation.Method;
            var parameters = new List<Parameter>();
            for (var paramIndex = 0; paramIndex < invocation.Arguments.Length; paramIndex++)
            {
                var paramValue = invocation.Arguments[paramIndex];
                string jsonValue = null;
                // if (Configuration.Configure.Conventions.IsCollectableArguments) {
                jsonValue = JsonConvert.SerializeObject(invocation.Arguments[paramIndex]);
                // }
                var parameter = new Parameter
                                {
                                    PointId = pointId,
                                    Name = method.GetParameters()[paramIndex].Name,
                                    Position = method.GetParameters()[paramIndex].Position,
                                    TypeFullName = paramValue.GetType().AssemblyQualifiedName,
                                    Value = jsonValue,
                                    IsReturn = false
                                };
                parameters.Add(parameter);
                //Console.WriteLine(
                //    string.Format("Name {0}, Type {1}, Value {2}", parameter.Name, parameter.Type, parameter.Value));
            }
            return parameters;
        }

        private static Type RegisteredType(Type type)
        {
            if (Configuration.Configure.Container.Kernel.HasComponent(type))
            {
                return type;
            }

            foreach (var handler in Configuration.Configure.Container.Kernel.GetAssignableHandlers(typeof(object)))
            {
                if (handler.ComponentModel.Implementation == type)
                {
                    return handler.ComponentModel.Services.FirstOrDefault();
                }
            }

            return type;
        }
    }
}