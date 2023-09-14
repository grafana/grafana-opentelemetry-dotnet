using System;
using System.Linq;
using System.Reflection;

namespace Grafana.OpenTelemetry
{
    internal static class ReflectionHelper
    {
        internal static void CallStaticMethod(string assemblyName, string typeName, string methodName, object[] arguments)
        {
            try
            {
                Assembly
                    .Load(assemblyName)
                    .GetType(typeName)
                    .GetMethod(methodName, arguments.Select(obj => obj.GetType()).ToArray())
                    .Invoke(null, arguments);
            }
            catch (Exception _)
            { }
        }
    }
}
