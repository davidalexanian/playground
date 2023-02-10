<<<<<<< HEAD
﻿using System.Collections;
using System.Threading;

await CallMethodAsync();

static async Task CallMethodAsync() {
    int result = await MethodAsync(1, 2);
    Console.WriteLine(result);
}

static async Task<int> MethodAsync(int arg1, int arg2)
{
    // control returned when reaching the line with await keyword.
    // If does not contain await keyword, runs synchronously
    await Task.Delay(3000);
    return arg1 + arg2;
}


=======
﻿using System;
using System.Threading;

namespace dotNetNew
{
    class Program
    {
        // initially allow 0, max allow 3
        static Semaphore pool = new Semaphore(0, 3);     

        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Worker));
                t.Start(i);
            }

            Thread.Sleep(1000);
            Console.WriteLine("Allowing 3 threads in");
            pool.Release(3);    // allow 3 threads in
        }

        static void Worker(object num)
        {
            // Each worker thread begins by requesting the semaphore.
            pool.WaitOne();
            Console.WriteLine("Thread {0} enters the semaphore.", num);

            Thread.Sleep(3000);
            var countBeforeReleasing = pool.Release();
            Console.WriteLine("Thread {0} released semaphore, prev. count: {1}", num, countBeforeReleasing);
        }
    }
}
>>>>>>> 352521e528d4a81cdec2108bbe10123f1c5abc60
