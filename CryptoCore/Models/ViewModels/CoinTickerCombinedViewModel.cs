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
            SearchInfo = new List<CoinTickerCombinedModel>();
            AllInfo = new List<CoinTickerCombinedModel>();
        }
        public List<CoinTickerCombinedModel> SearchInfo { get; set; }

        public List<CoinTickerCombinedModel> AllInfo{ get; set; }


    }
}
