//
// Copyright Grafana Labs
// SPDX-License-Identifier: Apache-2.0
//

using System;
using System.Collections.Generic;
using Xunit;

namespace Grafana.OpenTelemetry.Tests
{
    public class GrafanaOpenTelemetryResourceDetectorTest
    {
        [Fact]
	public void DefaultDeploymentEnvironment()
	{
            var settings = new GrafanaOpenTelemetrySettings();
	    var resource = new GrafanaOpenTelemetryResourceDetector(settings).Detect();
	    var resourceAttributes = new Dictionary<string, object>();

	    foreach (var attribute in resource.Attributes) 
	    { 
	        resourceAttributes[attribute.Key] = attribute.Value;
	    }

            Assert.Equal("production", resourceAttributes["deployment.environment"]);
	}

        [Fact]
	public void CustomDeploymentEnvironment()
	{
            var settings = new GrafanaOpenTelemetrySettings();
	    settings.DeploymentEnvironment = "Custom";

	    var resource = new GrafanaOpenTelemetryResourceDetector(settings).Detect();
	    var resourceAttributes = new Dictionary<string, object>();

	    foreach (var attribute in resource.Attributes) 
	    { 
	        resourceAttributes[attribute.Key] = attribute.Value;
	    }

            Assert.Equal("Custom", resourceAttributes["deployment.environment"]);
	}

        [Fact]
	public void DeploymentEnvironmentFromEnv()
	{
            Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "CustomEnv");

            var settings = new GrafanaOpenTelemetrySettings();
	    var resource = new GrafanaOpenTelemetryResourceDetector(settings).Detect();
	    var resourceAttributes = new Dictionary<string, object>();

	    foreach (var attribute in resource.Attributes) 
	    { 
	        resourceAttributes[attribute.Key] = attribute.Value;
	    }

            Assert.Equal("CustomEnv", resourceAttributes["deployment.environment"]);
	}
    }
}
