using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Services.Binance
{
    public class CurrentPriceResponse
    {

        public string symbol { get; set; }
        public string price { get; set; }
    }


}
