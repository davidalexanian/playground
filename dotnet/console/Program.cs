using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace console
{
    class Program
    {
        private static SemaphoreSlim concurrency = new SemaphoreSlim(80, 80);
        private static string directoryPath = @"C:\Workspace\Temp\images\";
        private static Process process = Process.GetCurrentProcess();        
        private static ConcurrentDictionary<string, double> durationByUrl = new ConcurrentDictionary<string, double>();
        private static ConcurrentDictionary<string, string> errorsByUrl = new ConcurrentDictionary<string, string>();

        static async Task Main(string[] args)
        {
            if (Directory.Exists(directoryPath)) Directory.Delete(directoryPath, true);
            Directory.CreateDirectory(directoryPath);
            var urls = GetURLs_Categories().Where(x => x != null && x.Any()).ToList();

            Console.WriteLine($"Loaded {urls.Count()} urls, unique {urls.Distinct().Count()}");
            var start = DateTime.Now;
            await Task.WhenAll(urls.Distinct().Select(url => DownloadAsync(url)));

            Console.WriteLine();
            Console.WriteLine("Completed " + (DateTime.Now - start).TotalSeconds.ToString() + $" total seconds");
            Console.WriteLine($"Average duration - {durationByUrl.Values.Average()} seconds, Max duration - {durationByUrl.Values.Max()} seconds, Min duration - {durationByUrl.Values.Min()} seconds");

            int durationCount = 100;
            Console.WriteLine($"Top {durationCount} slowest urls");
            foreach (var item in durationByUrl.OrderByDescending(x => x.Value).Take(durationCount)) {
                Console.WriteLine($"{item.Key} - {item.Value} seconds");
            }
            Console.WriteLine();
        }

        static private string[] GetURLs_Categories()
        {
            var text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "categories-small.json"));
            var data = JsonSerializer.Deserialize<MasterPricebookCategoryInModel[]>(text,new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data.SelectMany(x => x.Images ?? new string[0]).ToArray();
        }

        static private string[] GetURLs_Equipments()
        {
            var text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "equipments_3300.json"));
            var data = JsonSerializer.Deserialize<MasterPricebookCategoryInModel[]>(text, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data.SelectMany(x => x.Images ?? new string[0]).ToArray();
        }
        
        private async static Task DownloadAsync(string url)
        {
            await concurrency.WaitAsync();
            DateTime start = DateTime.Now;

            try
            {
                using (var client = new WebClient())
                {
                    //client.Headers.Add(HttpRequestHeader.KeepAlive, "Close");
                    var str = await client.OpenReadTaskAsync(url);
                    using (var inputStream = new FileStream(directoryPath + Guid.NewGuid().ToString("N") + ".jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await str.CopyToAsync(inputStream);
                    }
                }
                var duration = (DateTime.Now - start).TotalSeconds;
                //durations.Add(duration);
                durationByUrl.AddOrUpdate(url, duration, (k,v)=> duration);
            }
            catch (Exception ex)
            {
                var duration = (DateTime.Now - start).TotalSeconds;
                errorsByUrl.AddOrUpdate(url, $"FAILED after {duration} seconds:" + ex.Message + ", internal error:" + ex.InnerException?.Message ?? "", (k, v) => ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }
            finally {
                concurrency.Release();
            }
        }

        private async static Task DownloadAsyncHttpClient(string url)
        {
            //await concurrency.WaitAsync();

            try
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var inputStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (var targetStream = new FileStream(directoryPath + Guid.NewGuid().ToString("N") + ".jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                await inputStream.CopyToAsync(targetStream);
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
               // concurrency.Release();
            }
        }
    }

    public class MasterPricebookCategoryInModel 
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryType { get; set; }

        public string Description { get; set; }

        public string ParentId { get; set; }

        public string[] Images { get; set; }

        public string[] Videos { get; set; }

        public bool? Active { get; set; }
    }
}
