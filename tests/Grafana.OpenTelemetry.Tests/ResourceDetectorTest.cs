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
            expected.Remove(ResourceDetector.AWSEBSDetector);
            expected.Remove(ResourceDetector.AWSEC2Detector);
#endif
#if NETSTANDARD || NETFRAMEWORK
            expected.Remove(ResourceDetector.AWSECSDetector);
            expected.Remove(ResourceDetector.AWSEKSDetector);
#endif
#if !NET6_0_OR_GREATER
            expected.Remove(ResourceDetector.Container);
#endif
#if NETSTANDARD
            expected.Remove(ResourceDetector.Host);
            expected.Remove(ResourceDetector.Process);
            expected.Remove(ResourceDetector.ProcessRuntime);
#endif
            var actual = new HashSet<ResourceDetector>(ResourceDetectorInitializer.Initializers.Select(x => x.Id));
            Assert.Equivalent(expected, actual, strict: true);
        }
    }
}
