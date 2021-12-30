﻿using System;
using System.Collections.Generic;
using cryptolletfdm.Common.Database;
using Newtonsoft.Json;

namespace cryptolletfdm.Common.Models
{

    public class Coin : BaseDatabaseItem
    {
        public Coin(string name, string symbol, string imageUrl, string color)
        {
            Name = name;
            Symbol = symbol;
            ImageUrl = imageUrl;
            HexColor = color;
        }

        public Coin() { }

        public int CoinId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public decimal Amount { get; set; }
        public decimal DollarValue { get; set; }
        public string ImageUrl { get; set; }
        public string HexColor { get; set; }

        public static List<Coin> GetAvailableAssets()
        {
            return new List<Coin>
            {
                new Coin("Bitcoin", "BTC", "BTC.png", "#850CDB"),
                new Coin("Bitcoin Cash", "BCH", "BCH.png", "#EF4E5B"),
                new Coin("Dash", "DASH", "DASH.png", "#8A709C"),
                new Coin("Eos", "EOS", "EOS.png", "#7E8E81"),
                new Coin("Ethereum", "ETH", "ETH.png", "#BDACAC"),
                new Coin("Litecoin", "LTC", "LTC.png", "#9063B0"),
                new Coin("Monero", "XMR", "XMR.png", "#DA8B90"),
                new Coin("Ripple", "XRP", "XRP.png", "#856F92"),
                new Coin("Stellar", "XLM", "XLM.png", "#50A665")
            };
        }
    }
}
