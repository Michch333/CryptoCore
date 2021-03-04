using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCore.Models
{
    public partial class Crypto
    {
        public string Symbol { get; set; }
        public float Price  { get; set; }
        public Currency Name => ConvertSymbolToCurrency(Symbol);
        public Currency ConvertSymbolToCurrency(string symbol)
        {
            switch (symbol.ToUpper())
            {
                case "BTC":
                    return Currency.Bitcoin;
                case "ETH":
                    return Currency.Ethereum;
                case "ADA":
                    return Currency.Cardano;
                case "BNB":
                    return Currency.BinanceCoin;
                case "USDT":
                    return Currency.Tether;
                case "DOT":
                    return Currency.Polkadot;
                case "XRP":
                    return Currency.XRP;
                case "LTC":
                    return Currency.Litecoin;
                case "LINK":
                    return Currency.Chainlink;
                case "BCH":
                    return Currency.BitcoinCash;
                case "XLM":
                    return Currency.Stellar;
                case "USDC":
                    return Currency.USDCoin;
                case "UNI":
                    return Currency.Uniswap;
                case "XEM":
                    return Currency.NEM;
                case "DOGE":
                    return Currency.DogeCoin;
                case "WBTC":
                    return Currency.WrappedBitcoin;
                case "AAVE":
                    return Currency.Aave;
                case "ATOM":
                    return Currency.Cosmos;
                case "THETA":
                    return Currency.Theta;
                case "XMR":
                    return Currency.Monero;
                case "CRO":
                    return Currency.CryptoDotCom;
                case "SOL":
                    return Currency.Solana;
                case "EOS":
                    return Currency.EOS;
                case "BSV":
                    return Currency.BitcoinSV;
                case "TRX":
                    return Currency.TRON;
                case "MIOTA":
                    return Currency.Iota;
                case "VET":
                    return Currency.VeChain;
                case "FTT":
                    return Currency.FTXToken;
                case "XTZ":
                    return Currency.Tezos;
                case "LUNA":
                    return Currency.Terra;
                case "HT":
                    return Currency.HuobiToken;
                case "BUSD":
                    return Currency.BinanceUSD;
                case "NEO":
                    return Currency.Neo;
                case "SNX":
                    return Currency.Synthetix;
                case "ALGO":
                    return Currency.Algorand;
                case "DAI":
                    return Currency.Dai;
                case "GRT":
                    return Currency.TheGraph;
                case "EGLD":
                    return Currency.Elrond;
                case "FIL":
                    return Currency.Filecoin;
                case "COMP":
                    return Currency.Compound;
                case "DASH":
                    return Currency.Dash;
                case "SUSHI":
                    return Currency.SushiSwap;
                case "MKR":
                    return Currency.Maker;
                case "AVAX":
                    return Currency.Avalanche;
                case "KSM":
                    return Currency.Kusama;
                case "LEO":
                    return Currency.UnusSedLeo;
                case "DCR":
                    return Currency.Decred;
                case "CAKE":
                    return Currency.PancakeSwap;
                case "VGX":
                    return Currency.VoyagerToken;
                case "ZEC":
                    return Currency.Zcash;
                default:
                    return Currency.Bitcoin;
            }

        }
    }
}
