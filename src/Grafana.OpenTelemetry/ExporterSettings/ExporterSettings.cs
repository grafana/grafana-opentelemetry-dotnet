using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

namespace Grafana.OpenTelemetry;

public interface ExporterSettings
{
    void Apply(TracerProviderBuilder builder);
    void Apply(MeterProviderBuilder builder);
}
