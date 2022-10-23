using RestSharp;

#nullable disable
namespace CosmeticsData.Models
{
    public class request
    {
        const string _url = "https://fortnite-api.com/v2/cosmetics/br/new";

        public static string Uwu(string language)
        {
            using (var _client = new RestClient(_url))
            {
                var _req = new RestRequest();
                _req.AddParameter("language", language);
                var _Execute = _client.Execute(_req);
                var _content = _Execute.Content;
                return _content;
            }
        }
    }
}
