
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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CryptoCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BinanceClient _binanceClient;
        private readonly RedditClient _redditClient;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, BinanceClient binanceClient, RedditClient redditClient, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                tempObject.Title = post.data.title;
                tempObject.SubReddit = post.data.subreddit;
                tempObject.AuthorName = post.data.author;
                tempObject.PermaLink = "https://www.reddit.com" + post.data.permalink;
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
        public async Task<List<CoinTickerCombinedModel>> GetAllCoinInfo(bool shouldSaveToDatabase = true)
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
                        if (shouldSaveToDatabase)
                        {
                            AddCoinInfoToDatabase(tempObject);
                        }
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
                    tempObject.symbol = coin.symbol.Replace("USDT","");
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
                    tempObject.symbol = coin.symbol.Replace("USDT", "");
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
            tempCoin.Symbol = coinInfo.CoinSymbol.Replace("USDT", "");
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
        //public async void AddCoinInfoToDatabase(string symbol)
        //{
        //    var results = await SearchBySymbol(symbol);
        //    foreach (var coin in results)
        //    {
        //        CoinDAL newDAL = new CoinDAL();
        //        newDAL.Price = coin.Price;
        //        newDAL.Symbol = coin.CoinSymbol;
        //        newDAL.Count =69;
        //        _db.Coins.Add(newDAL);
        //    }
        //    _db.SaveChanges();
        //}

        public async Task AddCoinInfoToDatabase(string symbol)
        {
            var results = await SearchBySymbol(symbol);
            foreach (var coin in results)
            {
                CoinDAL newDAL = new CoinDAL();
                newDAL.Price = coin.Price;
                newDAL.Symbol = coin.CoinSymbol;
                newDAL.Count = coin.Count;
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
        public void AddCoinPreferenceToDatabase(string symbol, float high, float low)
        {
            var userId = _userManager.GetUserId(User);
            var matchedRecord = _db.AllWalletInfo.Where(e => e.UserID.Equals(userId) && e.Symbol == symbol).FirstOrDefault();
            if (matchedRecord != null)
            {
                matchedRecord.UserHigh = high;
                matchedRecord.UserLow = low;
                _db.Update(matchedRecord);
            }
            else
            {
                var newEntry = new WalletDAL();
                newEntry.Symbol = symbol.Replace("USDT","");
                newEntry.UserHigh = high;
                newEntry.UserLow = low;
                newEntry.TimeAddedToWallet = DateTime.Now;
                newEntry.UserID = userId;
                _db.AllWalletInfo.Add(newEntry);
            }
            _db.SaveChanges();
        }
        public void RemoveCoinPreference(string symbol)
        {
            var userId = _userManager.GetUserId(User);
            var matchedRecord = _db.AllWalletInfo.Where(e => e.UserID.Equals(userId) && e.Symbol == symbol).FirstOrDefault();
            if (matchedRecord != null)
            {
                _db.AllWalletInfo.Remove(matchedRecord);
            }
            _db.SaveChanges();
        }
        public List<CoinDAL> GetCoinInfoFromDatabase(string symbol)
        {
            var listOfCoinRecords = _db.Coins.Where(s => s.Symbol == symbol).OrderBy(dt=>dt.EntryTime).ToList<CoinDAL>();
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
            return View(model);

        }
        public async Task<IActionResult> UserWallet()
        {
            UserWalletViewModel model = await BuildWalletViewModel();
            return View(model);
        }

        private async Task<UserWalletViewModel> BuildWalletViewModel()
        {
            var userId = _userManager.GetUserId(User);
            var model = new UserWalletViewModel();
            model.AllInfo = await GetAllCoinInfo();
            var listOfCoins = _db.AllWalletInfo.Where(e => e.UserID.Equals(userId)).ToList(); // TODO - Hard Coding User id
            foreach (var followedCoin in listOfCoins)
            {

                var coinInfo = await SearchBySymbolExact(followedCoin.Symbol);
                var tempObject = new CombinedAllAndWatched();
                tempObject.CoinSymbol = coinInfo.CoinSymbol;
                tempObject.UserHigh = followedCoin.UserHigh;
                tempObject.UserLow = followedCoin.UserLow;
                tempObject.Price = coinInfo.Price;
                tempObject.PriceChangePercent = coinInfo.PriceChangePercent;
                tempObject.Count = coinInfo.Count;
                model.CombinedInfo.Add(tempObject);
            }

            return model;
        }
        [AllowAnonymous]
        public async Task<IActionResult> DisplaySearchInfo(string symbol = "DOGE")
        {
            var model = new CoinTickerCombinedViewModel();
            model.SearchInfo = await SearchBySymbol(symbol);
            model.AllInfo = await GetAllCoinInfo();

            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> DisplayAllCoins()
        {
            var model = new CoinTickerCombinedViewModel();
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

                if (coin.CoinSymbol.Equals(upperSymbol))
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

        public async Task<IActionResult> AddCoin(string symbol, float high, float low)
        {
            AddCoinPreferenceToDatabase(symbol, high, low);
            var model = await BuildWalletViewModel();

            return View("UserWallet", model);
        }
        public async Task<IActionResult> RemoveCoin(string symbol)
        {
            RemoveCoinPreference(symbol);
            var model = await BuildWalletViewModel();

            return View("UserWallet", model);
        }

        public async Task<IActionResult> ExpandedCoin(string symbol = "DOGE")
        {
            await AddCoinInfoToDatabase(symbol);
            var userId = _userManager.GetUserId(User);
            var model = new ExpandedCoinViewModel();
            model.RedditPosts = await SearchReddit(symbol);
            model.AllInfo = await GetAllCoinInfo();
            model.DatabaseInfo = GetCoinInfoFromDatabase(symbol);
            var tempObject = await SearchBySymbolExact(symbol);
            var wallet = GetWalletFromDatabase(symbol);
            model.CoinSymbol = tempObject.CoinSymbol;
            model.Price = tempObject.Price;
            model.PriceChange = tempObject.PriceChange;
            model.PriceChangePercent = tempObject.PriceChangePercent;
            model.Count = tempObject.Count;
            model.Lables = new List<string>();
            model.Data = new List<float>();
            double datbaseDividedByFive = (model.DatabaseInfo.Count() / 5);
            var iter = (int)Math.Truncate(datbaseDividedByFive);
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            for (int i = model.DatabaseInfo.Count - 1; i >= iter - 1; i = i - iter)
            {
                model.Lables.Add(model.DatabaseInfo[i].EntryTime.ToLocalTime().ToString());
                model.Data.Add(model.DatabaseInfo[i].Price);
            }
            if (wallet != null)
            {
                model.IsinWallet = true;
                model.UserHigh = wallet.UserHigh;
                model.UserLow = wallet.UserLow;
            }
            else
            {
                model.IsinWallet = false;
                model.UserHigh = 0;
                model.UserLow = 0;

            }

            return View(model);
        }
        public WalletDAL GetWalletFromDatabase(string symbol)
        {
            var Wallet = _db.AllWalletInfo.Where(s => s.Symbol == symbol).FirstOrDefault();
            return Wallet;
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

        public IActionResult Login() { return View(); }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        } 
        public async Task<IActionResult> DatabaseUpdate(bool shouldUpdateDatabase) 
        {
            var model = new CoinTickerCombinedViewModel();
            model.AllInfo = await GetAllCoinInfo(shouldUpdateDatabase);

            return View(model);
        }

    }
}
