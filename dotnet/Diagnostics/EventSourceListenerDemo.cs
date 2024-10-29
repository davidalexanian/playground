using System.Diagnostics.Tracing;

class EventSourceListenerDemo : EventListener
{
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name == EventSourceDemo.ProviderName)
        {
            EnableEvents(eventSource, EventLevel.Informational);
        }
    }
    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        if (eventData.EventSource.Name == EventSourceDemo.ProviderName)
        {
            Console.WriteLine($"{nameof(EventSourceListenerDemo)}:{eventData.TimeStamp}-{eventData.EventName}-{eventData.EventSource}-{eventData.Level}-{eventData.Message}");
        }
    }
}