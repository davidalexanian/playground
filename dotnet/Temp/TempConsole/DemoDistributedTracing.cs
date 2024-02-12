using OpenTelemetry.Resources;
using OpenTelemetry;
using System.Diagnostics.Tracing;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace TempConsole {
    class DemoDistributedTracing
    {
        private static ActivitySource source = new ActivitySource("TempConsole.DoSomeWorkActviity", "1.0.0");

        public static async Task DoSomeWork(string foo, int bar)
        {
            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MySample"))
                .AddSource("TempConsole.DoSomeWorkActviity")
                .AddConsoleExporter()
                .Build();

            using (Activity activity = source.StartActivity("ParentActivity"))
            {
                activity?.SetTag("foo", foo);
                activity?.SetTag("bar", bar);
                await StepOne();
                await StepTwo();
                activity?.AddEvent(new ActivityEvent("Done now"));

                // Pretend something went wrong
                activity?.SetTag("otel.status_code", "ERROR");
                activity?.SetTag("otel.status_description", "Use this text give more information about the error");
            }
        }
        static async Task StepOne()
        {
            using (Activity activity = source.StartActivity("StepOne"))
            {
                await Task.Delay(500);
            }
        }
        static async Task StepTwo()
        {
            using (Activity activity = source.StartActivity("StepTwo"))
            {
                await Task.Delay(500);
            }
        }
    }
}
