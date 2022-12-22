using CosmeticsData.Models;
using CosmeticsData.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;


#nullable disable
namespace CosmeticsData
{
    class Program
    {
        public class Settings
        {
            public string Language { get; set; }
        }

        public static void Main()
        {
            Console.Title = "Cosmetics Data - New Cosmetics Generator";
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            if (!File.Exists("settings.json"))
            {
                Console.WriteLine("Oh, looks like you don't have a settings profile! Send me the language you want for extract the data (ex: en, es, fr, ..)");
                string lang = Console.ReadLine();
                Settings settings = new Settings { Language = lang };
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText("settings.json", json);
            }

            var setting = File.ReadAllText("settings.json");
            var x = JsonConvert.DeserializeObject<Settings>(setting);

            var watch = Stopwatch.StartNew();
            getCosmetics(x.Language); // return 0 istead an exception - return 1 if successfull
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Logger.InitilizeSuccess($"\n\nTask executed in {elapsedMs}ms");
            Console.ReadKey();
        }

        public static int getCosmetics(string lang)
        {
            // request data
            Logger.InitilizeInfo("Requesting data..");
            var cosmetics = request.Uwu(lang);
            var final = JsonConvert.DeserializeObject<Root>(cosmetics);
            if (final.status != 200)
            {
                Logger.InitilizeError($"Bad request: {final.status}");
                return 0;
            }
            else
            {
                Logger.InitilizeSuccess($"Request with status code [{final.status}(OK)]");
                Logger.InitilizeInfo($"Current build: {final.data.build}");

                foreach (var x in final.data.items)
                {
                    Logger.InitilizeProccess($"Downloading image for \"{x.name}\"");
                    using (var _client = new RestClient(x.images.icon))
                    {
                        var _req = new RestRequest();
                        byte[] response = _client.DownloadData(_req);
                        File.WriteAllBytes($"Data/{x.id}.png", response);
                    }
                }
                return 1;
            }
        }
    }
}
