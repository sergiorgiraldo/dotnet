using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppBot
{
    class Category
    {
        public string Endpoint { get; set; }
        public int Start { get; set; }
        public int Offset { get; set; }
        public int PagesRetrieved { get; set; }
        public int NoPages { get; set; }
        public string Which { get; set; }
        public string InitialDate { get; set; }
        public string FinalDate { get; set; }
        public bool Finished { get; set; }
    }
    
    class Program
    {
        private const string PATH = @"C:\SOME\PATH";
        private static readonly List<Category> Categories = new List<Category>();
        private static int CUT = 2000; //there is a limit of 2500 pages to be retrieved

        static void Main(string[] args)
        {
            Setup();

            var categories = Categories.Where(c => !c.Finished);
            var lstCategory = categories.ToList();

            while (lstCategory.Any())
            {
                foreach (var category in lstCategory)
                {
                    Console.WriteLine(category.Which);

                    for (var page = category.Start; page <= Math.Min(category.NoPages, CUT); page++)
                    {
                        Console.WriteLine("Page " + page + " of " + category.NoPages);
                        GetReviews(category, page).Wait();
                        category.PagesRetrieved += 1;
                    }

                    category.Offset += category.PagesRetrieved;
                    category.Finished = (category.PagesRetrieved == category.NoPages);
                    category.PagesRetrieved = 0;
                }

                categories = Categories.Where(c => !c.Finished);
                lstCategory = categories.ToList();
                Reconfigure(lstCategory).Wait();
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private static void Setup()
        {
            var finalDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var c1 = new Category
            {
                Endpoint =
                    "https://api.appbot.co/api/v2/apps/APP1_ID_IN_APPBOT/reviews?page={0}&start={1}&end={2}",
                Which = "app1",
                Offset = 0,
                PagesRetrieved = 0,
                Start = 1,
                FinalDate = finalDate
            };
            var c2 = new Category
            {
                Endpoint =
                    "https://api.appbot.co/api/v2/apps/APP2_ID_IN_APPBOT/reviews?page={0}&start={1}&end={2}",
                Which = "app2",
                Offset = 0,
                PagesRetrieved = 0,
                Start = 1,
                FinalDate = finalDate
            };
            Categories.Add(c1);
            Categories.Add(c2);

            Configure().Wait();
        }

        private static async Task Configure()
        {
            using (var sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "control.csv")))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var parts = line.Split(";");
                    Categories
                        .FirstOrDefault(c => c.Which == parts[0])
                        .InitialDate = parts[1];
                    line = sr.ReadLine();
                }
            }

            foreach (var category in Categories)
            {
                Console.WriteLine("Configuring:" + category.Which);

                var handler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential("YOUR_USERNAME", "YOUR_SECRET")
                };

                using (var client = new HttpClient(handler))
                {
                    var endpoint = category.Endpoint
                        .Replace("page={0}&", "")
                        .Replace("{1}", category.InitialDate)
                        .Replace("{2}", category.FinalDate); ;
                    var stringTask = client.GetStringAsync(endpoint);

                    var result = await stringTask;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    category.NoPages = json.total_pages;
                    Console.WriteLine("\tDone.");
                }
            }
        }

        private static async Task Reconfigure(List<Category> categories)
        {
            foreach (var category in categories)
            {
                Console.WriteLine("Reconfiguring:" + category.Which);

                //we have to execute twice
                //first pass, we get the final date
                //second pass, we get the number of pages
                for (int i = 1; i <= 2; i++)
                {
                    var handler = new HttpClientHandler
                    {
                        Credentials = new NetworkCredential("YOUR_USERNAME", "YOUR_SECRET")
                    };

                    using (var client = new HttpClient(handler))
                    {
                        var endpoint = category.Endpoint
                            .Replace("{0}", CUT.ToString())
                            .Replace("{1}", category.InitialDate)
                            .Replace("{2}", category.FinalDate);
                        var stringTask = client.GetStringAsync(endpoint);

                        var result = await stringTask;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        if (i == 1) category.FinalDate = json.results.Last.published_at.ToString();
                        if (i == 2) category.NoPages = json.total_pages;
                    }
                }
                Console.WriteLine("\tDone.");
            }
        }

        private static async Task GetReviews(Category category, int page)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential("YOUR_USERNAME", "YOUR_SECRET")
                };

                using (var client = new HttpClient(handler))
                {
                    var endpoint = category.Endpoint
                        .Replace("{0}", page.ToString())
                        .Replace("{1}", category.InitialDate)
                        .Replace("{2}", category.FinalDate);
                    var stringTask = client.GetStringAsync(endpoint);

                    var result = await stringTask;
                    SaveFile(result, category, page);
                    Console.WriteLine("\tSaved.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting page " + page + " from " + category.Which + "::" + e.Message);
                throw;
            }
        }

        private static void SaveFile(string contents, Category category, int page)
        {
            var folderPath = Path.Combine(PATH, category.Which, DateTime.Now.ToString("yyyyMMdd"));
            Directory.CreateDirectory(folderPath);
            var fullPath = Path.Combine(folderPath, "reviews_page_" + (page + category.Offset) + ".txt");

            using (var sw = new StreamWriter(fullPath))
            {
                sw.WriteLine(contents);
            }
        }
    }
}
