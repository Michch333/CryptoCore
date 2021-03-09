
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoCore.Models;
using CryptoCore.Services.Binance;

using System.Threading.Tasks;
using CryptoCore.Data;
using CryptoCore.Models.DALModels;
using System.Collections.Generic;
using CryptoCore.Services.Reddit;
using System;

namespace CryptoCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BinanceClient _binanceClient;
        private readonly RedditClient _redditClient;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, BinanceClient binanceClient, RedditClient redditClient, ApplicationDbContext dbContext)
        {
            _binanceClient = binanceClient;
            _redditClient = redditClient;
            _logger = logger;
            _db = dbContext;
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
        //public async Task<List<RedditModel>> GetRedditData()
        //{
        //    var redditList = new List<RedditModel>();
        //    var redditResponse = await _redditClient.GetRedditSearchInfo();
        //    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        //    foreach (var post in redditResponse.data.children)
        //    {
        //        var timeInUtc = DateTime.Parse(post.data.created_utc.ToString());
        //        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeInUtc, cstZone);
        //    }


        //}

        public async Task<List<TickerModel>> ConvertTickerToFloatAndRemoveUsdt()
        {

            var coinList = new List<TickerModel>();
            var response = await _binanceClient.GetTwentyFourHourTickerInfo();
            var newResponse = removeUsdt(response);
            foreach (var coin in newResponse)
            {
                var tempObject = new TickerModel();
                tempObject.Symbol = coin.symbol;
                tempObject.PriceChangePercent = float.Parse(coin.priceChangePercent);
                tempObject.PriceChange = float.Parse(coin.priceChange);
                tempObject.Count = coin.count;
                coinList.Add(tempObject);
            }
            return coinList;
        }
        public async Task<List<CoinTickerCombinedModel>> GetAllCoinInfo()
        {

            var combinedInfo = new List<CoinTickerCombinedModel>();
            var priceList = await ConvertPricesToFloatAndRemoveUsdt();
            var tickerList = await ConvertTickerToFloatAndRemoveUsdt();
            foreach (var coin in priceList)
               
            {
                foreach (var ticker in tickerList)
                {
                    if (coin.Symbol == ticker.Symbol)
                    {
                        var tempObject = new CoinTickerCombinedModel();
                        tempObject.CoinSymbol = coin.Symbol;
                        tempObject.TickerSymbol = ticker.Symbol;
                        tempObject.Price =coin.Price;
                        tempObject.PriceChange =ticker.PriceChange;
                        tempObject.PriceChangePercent =ticker.PriceChangePercent;
                        tempObject.Count = ticker.Count;
                        combinedInfo.Add(tempObject);
                    }
                    
                }

            }
            return combinedInfo;

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
        public List<TickerResponse> removeUsdt(List<TickerResponse> response)
        {
            var pickles = new List<TickerResponse>();
            foreach (var coin in response)
            {
                if (coin.symbol.EndsWith("USDT"))
                {
                    var tempObject = new TickerResponse();
                    tempObject.symbol = coin.symbol;
                    tempObject.priceChangePercent = coin.priceChangePercent;
                    tempObject.count = coin.count;
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
        public async Task<IActionResult> SearchBySymbol(string symbol ="DOGE")
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
        public async Task<IActionResult> AddCoinToDB(string coinSymbol, float coinPrice)
        {
            var emptyCoin = new CoinDAL();
            emptyCoin.Symbol = coinSymbol.Replace("USDT", "");
            emptyCoin.Price = coinPrice;
            emptyCoin.HighAlertThreshold = coinPrice * 1.10f;
            emptyCoin.LowAlertThreshold = coinPrice * 0.90f;

            _db.Add(emptyCoin);
            _db.SaveChanges();
            var model = new CurrentPriceViewModel();
            model.Coins = await ConvertPricesToFloatAndRemoveUsdt();
            return View("Index", model);
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
