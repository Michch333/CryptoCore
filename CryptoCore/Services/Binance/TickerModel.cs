using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class TickerModel
    {
        public string Symbol { get; set; }
        public double PriceChange { get; set; }
        public float PriceChangePercent { get; set; }
        public float WeightedAvgPrice { get; set; }
        public float PrevClosePrice { get; set; }
        public float LastPrice { get; set; }
        public float LastQty { get; set; }
        public float BidPrice { get; set; }
        public float BidQty { get; set; }
        public float AskPrice { get; set; }
        public float AskQty { get; set; }
        public float OpenPrice { get; set; }
        public float HighPrice { get; set; }
        public float LowPrice { get; set; }
        public float Volume { get; set; }
        public float QuoteVolume { get; set; }
        public float OpenTime { get; set; }
        public long CloseTime { get; set; }
        public long FirstId { get; set; }
        public int LastId { get; set; }
        public int Count { get; set; }
    }
}
