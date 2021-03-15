using CryptoCore.Models.DALModels;
using CryptoCore.Services.Binance;
using CryptoCore.Services.Reddit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models.ViewModels
{
    public class UserWalletViewModel
    {
        public UserWalletViewModel()
        {
            AllInfo = new List<CoinTickerCombinedModel>();
            WatchedCoinInfo = new List<CoinTickerCombinedModel>();
            RedditPosts = new List<RedditModel>();
            CombinedInfo = new List<CombinedAllAndWatched>();
        }
        public WalletDAL Wallet { get; set; }
        public List<CoinTickerCombinedModel> AllInfo { get; set; }
        public List<CoinTickerCombinedModel> WatchedCoinInfo { get; set; }
        public List<RedditModel> RedditPosts { get; set; }
        public List<CombinedAllAndWatched> CombinedInfo { get; set; }
    }
}
