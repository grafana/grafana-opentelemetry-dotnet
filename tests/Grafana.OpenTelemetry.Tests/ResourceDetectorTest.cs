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
    public class ResourceDetectorTest
    {
        [Fact]
        public void EnumMatchesInitializers()
        {
            var expected = new HashSet<ResourceDetector>((ResourceDetector[])Enum.GetValues(typeof(ResourceDetector)));
#if NETSTANDARD
            expected.Remove(ResourceDetector.AWSEBS);
            expected.Remove(ResourceDetector.AWSEC2);
#endif
#if NETSTANDARD || NETFRAMEWORK
            expected.Remove(ResourceDetector.AWSECS);
            expected.Remove(ResourceDetector.AWSEKS);
#endif
#if !NET8_0_OR_GREATER
            expected.Remove(ResourceDetector.Container);
#endif
#if NETSTANDARD
            expected.Remove(ResourceDetector.Host);
            expected.Remove(ResourceDetector.OperatingSystem);
            expected.Remove(ResourceDetector.Process);
            expected.Remove(ResourceDetector.ProcessRuntime);
#endif
            var actual = new HashSet<ResourceDetector>(ResourceDetectorInitializer.Initializers.Select(x => x.Id));
            Assert.Equivalent(expected, actual, strict: true);
        }
    }
}
