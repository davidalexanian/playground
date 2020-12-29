using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr  = new object[2] { 
                new object(),
                new object()
            };
            Array.Sort(arr);
        }

        class y : xx {
            public override void methdo()
            {
                base.methdo();
            }
        }
        class xx : x
        {
            public virtual void methdo()
            {
                throw new NotImplementedException();
            }
        }
        interface x {

            void methdo();
        }
    }
}