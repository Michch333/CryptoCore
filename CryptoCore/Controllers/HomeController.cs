
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoCore.Models;
using CryptoCore.Services.Binance;

using System.Threading.Tasks;
using System.Collections.Generic;

namespace CryptoCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BinanceClient _binanceClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BinanceClient binanceClient)
        {
            _binanceClient = binanceClient;
            _logger = logger;
        }

        public async Task<List<CoinModel>> ConvertPricesToFloatAndRemoveUsdt()
        {

            var coinList = new List<CoinModel>();
            var response = await _binanceClient.GetCurrentPrice();
            var newResponse = removeUsdt(response);
            foreach (var coin in newResponse)
            {
                var tempObject = new CoinModel();
                tempObject.Symbol = coin.symbol;
                tempObject.Price = float.Parse(coin.price);
                coinList.Add(tempObject);
            }
            return coinList;
        }

        public List<CurrentPriceResponse> removeUsdt(List<CurrentPriceResponse> response)
        {
            var pickles = new List<CurrentPriceResponse>();
            foreach (var coin in response)
            {
                if (coin.symbol.EndsWith("USDT"))
                {
                    var tempObject = new CurrentPriceResponse();
                    tempObject.symbol = coin.symbol;
                    tempObject.price = coin.price;
                    pickles.Add(tempObject);
                }
                
            }
            return pickles;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CurrentPriceViewModel();
            model.Coins = await ConvertPricesToFloatAndRemoveUsdt();


            return View(model);
        }
        public async Task<IActionResult> SearchBySymbol(string symbol ="BTC")
        { 
            var model = new CurrentPriceViewModel();
            var curatedList = await ConvertPricesToFloatAndRemoveUsdt();
            foreach (var coin in curatedList)
            {

                if (coin.Symbol.Contains(symbol)) 
                {
                    var tempObject = new CoinModel();
                    tempObject.Symbol = coin.Symbol;
                    tempObject.Price = coin.Price;
                    model.Coins.Add(tempObject);
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
