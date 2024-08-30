//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class InstrumentationTest
    {
        [Fact]
        public void EnumMatchesInitializers()
        {
            var expected = new HashSet<Instrumentation>((Instrumentation[])Enum.GetValues(typeof(Instrumentation)));
#if !NETFRAMEWORK
            expected.Remove(Instrumentation.AspNet);
            expected.Remove(Instrumentation.Owin);
#endif
            var actual = new HashSet<Instrumentation>(InstrumentationInitializer.Initializers.Select(x => x.Id));
            Assert.Equivalent(expected, actual, strict: true);
        }
    }
}
