using System;

namespace temp
{
    class c {
        public override string ToString() => "abc";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{new c()}");
        }
    }
}
