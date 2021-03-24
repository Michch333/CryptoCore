using CryptoCore.Models.ApiModels;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCore.Services.Reddit
{
    public class RedditClient
    {
        private readonly HttpClient _client;
        public RedditClient(HttpClient redditClient)
        {
            _client = redditClient;
        }
        public async Task<RedditResponse> GetRedditSearchInfo(string search = "DOGE")
        {
            var result = await _client.GetAsync($"/search.json?q={search}/?sort=new");

            if (result.IsSuccessStatusCode && result != null)
            {
                var stringContent = await result.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var obj = JsonSerializer.Deserialize<RedditResponse>(stringContent, options);

                return obj;
            }
            else
            {
                throw new HttpRequestException("Bad doge :(");
            }
        }
    }
}
