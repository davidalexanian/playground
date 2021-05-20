using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Delay(100);
        }


        static async IAsyncEnumerable<int> GetEnum()
        {
            yield return 1;
            await Task.Delay(500);
            yield return 2;
            await Task.Delay(500);
            yield return 3;
            await Task.Delay(500);
            yield return 4;
        }
    }
}
