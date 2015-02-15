using System.Collections.Generic;

using Castle.DynamicProxy;

using Newtonsoft.Json;

using NInsight.Core.Domain;

namespace NInsight.Core.Mappers
{
    public class ParameterMapper
    {
        public List<Parameter> Do(IInvocation invocation)
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
    }
}