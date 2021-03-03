using CryptoCore.Models.ApiModels;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCore.Services.Reddit
{
    public class RedditClient
    {
        private readonly HttpClient _redditClient;
        public RedditClient(HttpClient redditClient)
        {
            _client = redditClient;
        }
        public async Task<RedditResponse> GetRedditSearchInfo(string search = "DOGE")
        {
            var result = await _redditClient.GetAsync($"/search.json?q={search}");

            if (result.IsSuccessStatusCode)
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
