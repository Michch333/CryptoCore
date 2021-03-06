﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class CombinedAllAndWatched
    {

        public string CoinSymbol { get; set; }
        public float Price { get; set; }
        public double PriceChange { get; set; }
        public float PriceChangePercent { get; set; }
        public double UserHigh { get; set; }
        public double UserLow { get; set; }
        public int Count { get; set; }
    }
}
