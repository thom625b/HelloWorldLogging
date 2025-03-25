using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using ILogger = Serilog.ILogger;

namespace API.Services;
public static class MonitorService
{
    public static readonly string ServiceName = Assembly.GetExecutingAssembly().GetName().Name ?? "Unknown";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
    public static TracerProvider TracerProvider;
    public static ILogger Log => Serilog.Log.Logger;

    static MonitorService()
    {
        // Set up explicit sampling to ensure all traces are collected
        TracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddConsoleExporter()
            .AddZipkinExporter()
            .AddSource(ActivitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
            .SetSampler(new AlwaysOnSampler()) // Explicitly sample everything
            .Build();
            
        // Configure Serilog as before
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .Enrich.WithSpan()
            .CreateLogger();
      
        
    }
}