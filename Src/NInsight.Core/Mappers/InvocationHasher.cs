using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Castle.DynamicProxy;

namespace NInsight.Core.Mappers
{
    public class InvocationHasher
    {
        public string Do(IInvocation invocation)
        {
            var parameters = new ParameterMapper().Do(invocation);
            var parametersString = string.Format(
                "{0} {1} {2}",
                invocation.Method.Name,
                invocation.InvocationTarget,
                string.Join(string.Empty, parameters.Select(p => p.KeyString()).ToArray()));
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(parametersString);
            data = cryptoServiceProvider.ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}