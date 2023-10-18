//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Linq;
using System.Reflection;

namespace Grafana.OpenTelemetry
{
    internal static class ReflectionHelper
    {
        internal static void CallStaticMethod(string assemblyName, string typeName, string methodName, object[] arguments)
        {
            var assembly = Assembly.Load(assemblyName);
            var type = assembly.GetType(typeName);
            var method = type.GetMethod(methodName, arguments.Select(obj => obj.GetType()).ToArray());
            method.Invoke(null, arguments);
        }
    }
}
