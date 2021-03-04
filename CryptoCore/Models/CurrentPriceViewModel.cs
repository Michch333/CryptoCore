using CryptoCore.Services.Binance;
using System.Collections.Generic;


namespace CryptoCore.Models
{
    public class CurrentPriceViewModel
    {
        public CurrentPriceViewModel()
        {
            Coins = new List<CoinModel>();
        }
        public List<CoinModel> Coins { get; set; }
    }
}