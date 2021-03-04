using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class BinanceClient
    {
        private readonly HttpClient _client;
        public BinanceClient(HttpClient Client)
        {
            _client = Client;
        }
        public async Task<List<CurrentPriceResponse>> GetCurrentPrice()
        {
            var result = await _client.GetAsync($"/api/v3/ticker/price");

            if (result.IsSuccessStatusCode)
            {
                var stringContent = await result.Content.ReadAsStringAsync();



                var obj = JsonSerializer.Deserialize<List<CurrentPriceResponse>>(stringContent);

                return obj;
            }
            else
            {
                throw new HttpRequestException("Bad doge :(");
            }
        }
        public async Task<TwentyFourHourTickerReponse> GetTwentyFourHourTickerInfo()
        {
            var result = await _client.GetAsync($"/api/v3/ticker/24hr");

            if (result.IsSuccessStatusCode)
            {
                var stringContent = await result.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var obj = JsonSerializer.Deserialize<TwentyFourHourTickerReponse>(stringContent, options);

                return obj;
            }
            else
            {
                throw new HttpRequestException("Bad doge :(");
            }
        }
    }
}
