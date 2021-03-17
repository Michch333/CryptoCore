using CryptoCore.Models.DALModels;
using CryptoCore.Services.Binance;
using CryptoCore.Services.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.ViewModels
{
    public class ExpandedCoinViewModel
    {
        public ExpandedCoinViewModel()
        {
            RedditPosts = new List<RedditModel>();
            CoinDAL = new List<CoinDAL>();
            AllInfo = new List<CoinTickerCombinedModel>();
        }

        public string CoinSymbol { get; set; }
        public float Price { get; set; }
        public double PriceChange { get; set; }
        public float PriceChangePercent { get; set; }
        public double UserHigh { get; set; }
        public double UserLow { get; set; }
        public int Count { get; set; }
        public List<RedditModel> RedditPosts { get; set; }
        public List<CoinDAL> CoinDAL { get; set; }
        public List<CoinTickerCombinedModel> AllInfo { get; set; }
    }
}

