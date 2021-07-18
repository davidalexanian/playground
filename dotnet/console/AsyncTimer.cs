using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace console
{
    class AsyncTimer
    {
        private Timer t;

        public AsyncTimer() 
        {
            var callback = new TimerCallback(async (object? o) => await ThreadingTimerCallback(o));
            t = new System.Threading.Timer(callback, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }

        private static async Task ThreadingTimerCallback(object? state) {
            try
            {
                await Task.Delay(2000);
                //Thread.Sleep(1000);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {Thread.CurrentThread.Name}");
                //throw new Exception("a");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
