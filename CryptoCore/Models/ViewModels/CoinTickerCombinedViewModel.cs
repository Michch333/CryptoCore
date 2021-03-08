using CryptoCore.Services.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.ViewModels
{
    public class CoinTickerCombinedViewModel
    {
        public CoinTickerCombinedViewModel()
        {
            CombinedInfo = new List<CoinTickerCombinedModel>();
        }
        public List<CoinTickerCombinedModel> CombinedInfo { get; set; }
    }
}
