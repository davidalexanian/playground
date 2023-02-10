using System.Linq; 

namespace DotNetNew
{
    public static class SpanAndMemory
    {
        public static void Play() {
            SpanOverArrayDemo();
            SpanOverStack();
        }

        public static void SpanOverArrayDemo() {
            // Create a span over an array.
            var array = new byte[100];
            var arraySpan = new Span<byte>(array);

            byte data = 0;
            for (int ctr = 0; ctr < arraySpan.Length; ctr++) {
                arraySpan[ctr] = data++;
            }
            Console.WriteLine($"The sum is {array.Sum(b => b)}");
        } 

        public static void SpanOverStack() {
            // Create a span on the stack.
            byte data = 0;
            Span<byte> stackSpan = stackalloc byte[100];
            for (int ctr = 0; ctr < stackSpan.Length; ctr++) {
                stackSpan[ctr] = data++;
            }

            int stackSum = 0;
            foreach (var value in stackSpan) {
                stackSum += value;
            }

            Console.WriteLine($"The sum is {stackSum}");
        }
    }
}