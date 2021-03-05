using CryptoCore.Services.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models
{
    public class TickerViewModel
    {
        public TickerViewModel()
        {
            Coins = new List<TickerModel>();
        }


        public List<TickerModel> Coins { get; set; }
    }
}
