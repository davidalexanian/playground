using System;
using System.Threading.Tasks.Dataflow; 

namespace dataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            var throwIfNegative = new ActionBlock<int>(n =>
            {
                Console.WriteLine("n = {0}", n);
                if (n < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            });

            throwIfNegative.Post(1);
            throwIfNegative.Post(2);
            throwIfNegative.Completion.Wait();
            
        }
    }
}