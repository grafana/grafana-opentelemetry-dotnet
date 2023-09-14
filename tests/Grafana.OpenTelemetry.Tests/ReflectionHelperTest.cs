using System;
using Grafana.OpenTelemetry;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class ReflectionHelperTest
    {
        internal static int Counter = 0;
        public static void Increment(int increment)
        {
            Counter += increment;
        }

        [Fact]
        public void CallStaticMethod()
        {
            ReflectionHelper.CallStaticMethod(
                typeof(ReflectionHelperTest).Assembly.GetName().Name,
                "Grafana.OpenTelemetry.Tests.ReflectionHelperTest",
                "Increment",
                new object[] { 4 });

            Assert.Equal(4, Counter);
        }

        [Fact]
        public void CallStaticMethodNoThrow()
        {
            ReflectionHelper.CallStaticMethod(
                typeof(ReflectionHelperTest).Assembly.GetName().Name,
                "Not-exist",
                "Not-exist",
                new object[] { 4 });
        }
    }
}
