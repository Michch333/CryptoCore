using CryptoCore.Services.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class CoinTickerCombinedModel
    {

        public string CoinSymbol { get; set; }
        public float Price { get; set; }
        public string TickerSymbol { get; set; }
        public double PriceChange { get; set; }
        public float PriceChangePercent { get; set; }
        public int Count { get; set; }
        public DateTime EntryDate { get; set; }

    }

}
