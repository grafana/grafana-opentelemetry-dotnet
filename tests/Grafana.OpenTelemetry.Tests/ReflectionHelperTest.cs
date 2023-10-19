//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using Grafana.OpenTelemetry;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class ReflectionHelperTest
    {
        internal static int Counter = 0;

        #pragma warning disable xUnit1013 // Allow public method that's not a [Theory]
        public static void Increment(int increment)
        {
            Counter += increment;
        }
        #pragma warning restore xUnit1013 // Allow public method that's not a [Theory]

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
        public void CallStaticMethodThrow()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                ReflectionHelper.CallStaticMethod(
                    typeof(ReflectionHelperTest).Assembly.GetName().Name,
                    "Not-exist",
                    "Not-exist",
                    new object[] { 4 });
            });
        }
    }
}
