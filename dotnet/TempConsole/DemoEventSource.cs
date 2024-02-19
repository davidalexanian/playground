using System.Diagnostics.Tracing;

namespace TempConsole
{
    [EventSource(Name = "Demo")]
    class DemoEventSource : EventSource
    {
        public static DemoEventSource Log { get; } = new DemoEventSource();

        [Event(1, Keywords = Keywords.Startup)]
        public void AppStarted(string message, int favoriteNumber) => WriteEvent(1, message, favoriteNumber);

        [Event(2, Keywords = Keywords.Requests)]
        public void RequestStart(int requestId) => WriteEvent(2, requestId);
        
        [Event(3, Keywords = Keywords.Requests)]
        public void RequestStop(int requestId) => WriteEvent(3, requestId);

        public class Keywords   // This is a bitvector
        {
            public const EventKeywords Startup = (EventKeywords)0x0001;
            public const EventKeywords Requests = (EventKeywords)0x0002;
        }

        public static void UseDemoEventSource() 
        {
            DemoEventSource.Log.AppStarted("app started", 0);
            DemoEventSource.Log.RequestStart(1);
            DemoEventSource.Log.RequestStop(2);
        }
    }
}
