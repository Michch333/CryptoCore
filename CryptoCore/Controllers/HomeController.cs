
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

using CryptoCore.Models.ViewModels;
using System.Linq;

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
            var newResponse = RemoveUsdt(response);
            foreach (var coin in newResponse)
            {
                var tempObject = new CoinModel();
                tempObject.Symbol = coin.symbol;
                tempObject.Price = float.Parse(coin.price);
                coinList.Add(tempObject);
            }
            return coinList;
        }
        public async Task<List<RedditModel>> SearchReddit(string search)
        {
            var redditList = new List<RedditModel>();
            var redditResponse = await _redditClient.GetRedditSearchInfo(search);
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            foreach (var post in redditResponse.data.children)
            {
                var tempObject = new RedditModel();
                //var timeInUtc = DateTime.Parse(post.data.created_utc.ToString());
                //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeInUtc, cstZone);
                tempObject.Title = post.data.title;
                tempObject.SubReddit = post.data.subreddit;
                tempObject.AuthorName = post.data.author;
                tempObject.PermaLink = post.data.permalink;
                //tempObject.CreatedDateTime = timeInUtc;
                redditList.Add(tempObject);

            }
            return redditList;

        }

        public async Task<List<TickerModel>> ConvertTickerToFloatAndRemoveUsdt()
        {

            var coinList = new List<TickerModel>();
            var response = await _binanceClient.GetTwentyFourHourTickerInfo();
            var newResponse = RemoveUsdt(response);
            foreach (var coin in newResponse)
            {
                var tempObject = new TickerModel();
                tempObject.Symbol = coin.symbol;
                tempObject.PriceChangePercent = float.Parse(coin.priceChangePercent);
                tempObject.PriceChange = double.Parse(coin.priceChange);
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
                        tempObject.Price = coin.Price;
                        tempObject.PriceChange = ticker.PriceChange;
                        tempObject.PriceChangePercent = ticker.PriceChangePercent;
                        tempObject.Count = ticker.Count;
                        combinedInfo.Add(tempObject);
                    }

                }

            }
            return combinedInfo;

        }

        public List<CurrentPriceResponse> RemoveUsdt(List<CurrentPriceResponse> response)
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
        public List<TickerResponse> RemoveUsdt(List<TickerResponse> response)
        {
            var pickles = new List<TickerResponse>();
            foreach (var coin in response)
            {
                if (coin.symbol.EndsWith("USDT"))
                {
                    var tempObject = new TickerResponse();
                    tempObject.symbol = coin.symbol;
                    tempObject.priceChangePercent = coin.priceChangePercent;
                    tempObject.priceChange = coin.priceChange;
                    tempObject.count = coin.count;
                    pickles.Add(tempObject);
                }

            }
            return pickles;
        }
        public void AddCoinInfoToDatabase(CoinTickerCombinedModel coinInfo)
        {
            var tempCoin = new CoinDAL();
            tempCoin.Symbol = coinInfo.CoinSymbol;
            tempCoin.Price = coinInfo.Price;
            tempCoin.Count = coinInfo.Count;

            _db.Coins.Add(tempCoin);
            _db.SaveChanges();
        }
        public void AddCoinInfoToDatabase(CoinTickerCombinedModel coinInfo, DateTime time)
        {
            var tempCoin = new CoinDAL();
            tempCoin.Symbol = coinInfo.CoinSymbol;
            tempCoin.Price = coinInfo.Price;
            tempCoin.Count = coinInfo.Count;
            tempCoin.EntryTime = time;

            _db.Coins.Add(tempCoin);
            _db.SaveChanges();
        }
        public async void AddCoinInfoToDatabase(string symbol)
        {
            var results = await SearchBySymbol(symbol);
            foreach (var coin in results)
            {
                var newDAL = new CoinDAL();
                newDAL.Price = coin.Price;
                newDAL.Symbol = coin.CoinSymbol;
                newDAL.Count = 69; // TODO - Cant access count because its something already
                _db.Coins.Add(newDAL);
            }
            _db.SaveChanges();
        }
        public async void AddCoinInfoToDatabase()
        {
            var results = await GetAllCoinInfo();
            foreach (var coin in results)
            {
                var newDAL = new CoinDAL();
                newDAL.Price = coin.Price;
                newDAL.Symbol = coin.CoinSymbol;
                newDAL.Count = coin.Count;
                _db.Coins.Add(newDAL);
            }
            _db.SaveChanges();
        }
        public void AddCoinPreferenceToDatabase(string symbol, int high, int low)
        {
            var matchedRecord = _db.AllWalletInfo.Where(e => e.UserID == 1 && e.Symbol == symbol).FirstOrDefault(); // TODO - Hard Coded UserID
            if (matchedRecord != null)
            {
                matchedRecord.UserHigh = high;
                matchedRecord.UserLow = low;
                _db.Update(matchedRecord);
            }
            else
            {
                var newEntry = new WalletDAL();
                newEntry.Symbol = symbol;
                newEntry.UserHigh = high;
                newEntry.UserLow = low;
                newEntry.TimeAddedToWallet = DateTime.Now;
                newEntry.UserID = 1;
                _db.AllWalletInfo.Add(newEntry);
            }
            _db.SaveChanges();
        }
        public void RemoveCoinPreference(string symbol) // TODO - Hard Coded user ID 
        {
            var matchedRecord = _db.AllWalletInfo.Where(e => e.UserID == 1 && e.Symbol == symbol).FirstOrDefault();
            if (matchedRecord != null)
            {
                _db.AllWalletInfo.Remove(matchedRecord);
            }
            _db.SaveChanges();
        }
        public List<CoinDAL> GetCoinInfoFromDatabase(string symbol)
        {
            var listOfCoinRecords = _db.Coins.Where(s => s.Symbol == symbol).ToList<CoinDAL>();
            return listOfCoinRecords;
        }
        public List<CoinDAL> GetCoinInfoFromDatabase()
        {
            var listOfCoinRecords = _db.Coins.Where(s => s.CoinId > 0).ToList<CoinDAL>();
            return listOfCoinRecords;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CoinTickerCombinedViewModel();
            model.AllInfo = await GetAllCoinInfo();
            AddCoinPreferenceToDatabase("DOGE",7,5);
            return View(model);

        }

        public async Task<IActionResult> UserWallet()
        {
            var model = new UserWalletViewModel();
            model.AllInfo = await GetAllCoinInfo();
            var listOfCoins = _db.AllWalletInfo.Where(e => e.UserID == 1).ToList(); // TODO - Hard Coding User id
            foreach (var followedCoin in listOfCoins)
            {
                var response = await SearchReddit(followedCoin.Symbol);
                foreach (var item in response)
                {
                    model.RedditPosts.Add(item);
                }
                var coinInfo = await SearchBySymbolExact(followedCoin.Symbol);
                model.WatchedCoinInfo.Add(coinInfo);
            }
            return View(model);
        }

        public async Task<IActionResult> DisplaySearchInfo(string symbol = "DOGE")
        {
            var model = new CoinTickerCombinedViewModel();
            model.SearchInfo = await SearchBySymbol(symbol);
            model.AllInfo = await GetAllCoinInfo();

            return View(model);
        }
        public async Task<List<CoinTickerCombinedModel>> SearchBySymbol(string symbol = "DOGE")
        {
            var upperSymbol = symbol.ToUpper();
            var searchedCoin = new List<CoinTickerCombinedModel>();
            var curatedList = await GetAllCoinInfo();
            foreach (var coin in curatedList)
            {

                if (coin.CoinSymbol.Contains(upperSymbol))
                {
                    var tempObject = new CoinTickerCombinedModel();
                    tempObject.CoinSymbol = coin.CoinSymbol;
                    tempObject.Price = coin.Price;
                    tempObject.PriceChange = coin.PriceChange;
                    tempObject.PriceChangePercent = coin.PriceChangePercent;
                    tempObject.Count = coin.Count;
                    searchedCoin.Add(tempObject);
                }
            }
            return searchedCoin;
        }
        public async Task<CoinTickerCombinedModel> SearchBySymbolExact(string symbol = "DOGE")
        {
            var upperSymbol = symbol.ToUpper();
            var searchedCoin = new CoinTickerCombinedModel();
            var curatedList = await GetAllCoinInfo();
            foreach (var coin in curatedList)
            {

                if (coin.CoinSymbol.Contains(upperSymbol)) 
                {
                    searchedCoin.CoinSymbol = coin.CoinSymbol;
                    searchedCoin.Price = coin.Price;
                    searchedCoin.PriceChange = coin.PriceChange;
                    searchedCoin.PriceChangePercent = coin.PriceChangePercent;
                    searchedCoin.Count = coin.Count;
                }
            }
            return searchedCoin;
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
