using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class TickerModel
    {
        public string Symbol { get; set; }
        public string PriceChange { get; set; }
        public string PriceChangePercent { get; set; }
        public string WeightedAvgPrice { get; set; }
        public string PrevClosePrice { get; set; }
        public string LastPrice { get; set; }
        public string LastQty { get; set; }
        public string BidPrice { get; set; }
        public string BidQty { get; set; }
        public string AskPrice { get; set; }
        public string AskQty { get; set; }
        public string OpenPrice { get; set; }
        public string HighPrice { get; set; }
        public string LowPrice { get; set; }
        public string Volume { get; set; }
        public string QuoteVolume { get; set; }
        public long OpenTime { get; set; }
        public long CloseTime { get; set; }
        public int FirstId { get; set; }
        public int LastId { get; set; }
        public int Count { get; set; }
    }
}
