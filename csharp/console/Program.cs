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
        private static SemaphoreSlim concurrency = new SemaphoreSlim(150, 150);
        private static string directoryPath = @"C:\Workspace\Temp\images\";
        private static Process process = Process.GetCurrentProcess();

        static async Task Main(string[] args)
        {

            //ThreadPool.GetMinThreads(out var w, out var s);
            ThreadPool.GetMaxThreads(out var wt, out var st);

            Console.WriteLine();


            Directory.Delete(directoryPath, true);
            Directory.CreateDirectory(directoryPath);
            var urls = GetURLs_Categories().Where(x => x != null && x.Any()).ToList();

            Console.WriteLine($"Loaded {urls.Count()} urls, unique {urls.Distinct().Count()}");
            var start = DateTime.Now;
            await Task.WhenAll(urls.Select(url => DownloadAsync(url)));

            Console.WriteLine((DateTime.Now - start).TotalSeconds.ToString() + $" total seconds");
        }

        static private string[] GetURLs_Categories()
        {
            var text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "categories_1000.json"));
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
            try
            {
                using (var client = new WebClient())
                {
                    var str = await client.OpenReadTaskAsync(url);
                    using (var inputStream = new FileStream(directoryPath + Guid.NewGuid().ToString("N") + ".jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await str.CopyToAsync(inputStream);
                    }
                }
                //ThreadPool.GetAvailableThreads(out var w, out var c);
                //Console.WriteLine($"{ThreadPool.ThreadCount} - {ThreadPool.PendingWorkItemCount} - {concurrency.CurrentCount} - {w}, {c}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
