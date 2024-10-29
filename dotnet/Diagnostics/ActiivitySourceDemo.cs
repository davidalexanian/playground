using OpenTelemetry.Resources;
using OpenTelemetry;
using System.Diagnostics;
using OpenTelemetry.Trace;

static class ActiivitySourceDemo
{
    static ActivitySource source = new ActivitySource("TempConsole.DoSomeWorkActviity", "1.0.0");

    public static async Task Demo()
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                .AddAttributes([
                    new KeyValuePair<string, object>("key1","value1"),
                    new KeyValuePair<string, object>("key2","value2")
                ])
                .AddService("MyService", "MyNameSpace", "1.0.0.0", true, "ServiceInstanceId"))
            .AddSource("TempConsole.DoSomeWorkActviity")
            .AddConsoleExporter()
            .Build();

        await DoSomeWork("banana", 8);
    }

    static async Task DoSomeWork(string foo, int bar)
    {
        using (Activity? activity = source.StartActivity("ParentActivity"))
        {
            activity?.SetStartTime(DateTime.UtcNow);
            activity?.SetTag("foo", foo);
            activity?.SetTag("bar", bar);
            
            await StepOne();
            await StepTwo();
            
            activity?.AddEvent(new ActivityEvent("Done now"));

            // Pretend something went wrong
            activity?.SetTag("otel.status_code", "404");
            activity?.SetTag("otel.status_description", "error desc");
            activity?.SetStatus(Status.Error);
        }
    }
    static async Task StepOne()
    {
        using (Activity? activity = source.StartActivity("StepOne", ActivityKind.Internal))
        {
            // no need, its set automatically
            activity?.SetParentId(Activity.Current?.Parent?.Id);
            await Task.Delay(500);
        }
    }
    static async Task StepTwo()
    {
        using (Activity? activity = source.StartActivity("StepTwo"))
        {
            await Task.Delay(500);
        }
    }
}
