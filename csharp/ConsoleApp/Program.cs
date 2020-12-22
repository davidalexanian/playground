using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WaitCallback((obj) => {
                Console.WriteLine((obj));
            });
            ThreadPool.QueueUserWorkItem(wc, "state");
            Console.ReadLine();
        }
    }
}