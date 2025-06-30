//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace SampleTests;

public class SnippetTests
{
    public void Can_Register_Grafana_With_TracerProvider()
    {
        // Act
        #region snippet dotnet-configure-otel-sdk
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .UseGrafana()
            .Build();
        #endregion snippet dotnet-configure-otel-sdk

        // Assert
        Assert.NotNull(tracerProvider);
    }
}
