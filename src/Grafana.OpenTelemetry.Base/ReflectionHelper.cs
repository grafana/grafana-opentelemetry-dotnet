//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System.Linq;
using System.Reflection;

namespace Grafana.OpenTelemetry
{
    internal static class ReflectionHelper
    {
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.RequiresUnreferencedCode("Types might be removed")]
#endif
        internal static void CallStaticMethod(string assemblyName, string typeName, string methodName, object[] arguments)
        {
            var assembly = Assembly.Load(assemblyName);
            var type = assembly.GetType(typeName);
            var method = type.GetMethod(methodName, [.. arguments.Select(obj => obj?.GetType())]);
            method.Invoke(null, arguments);
        }
    }
}
