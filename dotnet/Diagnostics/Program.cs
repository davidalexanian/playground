class Program
{
    static async Task Main(string[] args)
    {
        //new Thread(MetricsDemo.Demo).Start();

        //new EventSourceListenerDemo();
        //EventSourceDemo.UseDemoEventSource();

        await ActiivitySourceDemo.Demo();

        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }
}