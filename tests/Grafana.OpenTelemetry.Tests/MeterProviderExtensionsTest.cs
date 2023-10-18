//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class MeterProviderExtensionsTest
    {
        [Fact]
        public void EnableDefaultInstrumentations()
        {
            Sdk
                .CreateMeterProviderBuilder()
                .UseGrafana()
                .Build();
        }
    }
}
