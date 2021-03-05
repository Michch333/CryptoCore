
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoCore.Models;
using CryptoCore.Services.Binance;

using System.Threading.Tasks;
using CryptoCore.Data;
using CryptoCore.Models.DALModels;

namespace CryptoCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BinanceClient _binanceClient;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, BinanceClient binanceClient, ApplicationDbContext dbContext)
        {
            _binanceClient = binanceClient;
            _logger = logger;
            _db = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CurrentPriceViewModel();
            var response = await _binanceClient.GetCurrentPrice();
            foreach (var coin in response)
            {
                var tempObject = new CoinModel();
                tempObject.Symbol = coin.symbol;
                tempObject.Price = coin.price;
                model.Coins.Add(tempObject);

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
