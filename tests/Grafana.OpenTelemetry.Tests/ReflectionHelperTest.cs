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
        public class TestCounter
        {
            internal static int Counter = 0;
            public static void Increment(int increment)
            {
                Counter += increment;
            }
        }

        [Fact]
        public void CallStaticMethod()
        {
            ReflectionHelper.CallStaticMethod(
                typeof(ReflectionHelperTest).Assembly.GetName().Name,
                "Grafana.OpenTelemetry.Tests.ReflectionHelperTest.TestCounter",
                "Increment",
                new object[] { 4 });

            Assert.Equal(4, TestCounter.Counter);
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
