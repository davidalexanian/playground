using System.Collections;
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


