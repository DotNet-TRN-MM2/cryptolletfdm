using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptolletfdm.Application;
using cryptolletfdm.Common.Models;

namespace cryptolletfdm.Common.Controllers
{
    public interface IWalletController
    {
        Task<List<Transaction>> GetTransactions(bool forceReload = false);
        Task<List<Coin>> GetCoins(bool forceReload = false);
    }

    public class WalletController : IWalletController
    {
        private List<Coin> _defaultAssets = new List<Coin>
        {
                new Coin
                {
                    Name = "Bitcoin",
                    Amount = 0M,
                    Symbol = "BTC",
                    DollarValue = 0
                },
                new Coin
                {
                    Name = "Ethereum",
                    Amount = 0,
                    Symbol = "ETH",
                    DollarValue = 0
                },
                new Coin
                {
                    Name = "Litecoin",
                    Amount = 0,
                    Symbol = "LTC",
                    DollarValue = 0
                },
        }

        public Task<List<Transaction>> GetTransactions(bool forceReload = false)
        {
            return Task.FromResult(new List<Transaction>
            {
                new Transaction
                {
                    Amount = 1,
                    DollarValue=9500,
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    Symbol = "BTC",
                    TransactionDate = DateTime.Now
                },
                new Transaction
                {
                    Amount = 2,
                    DollarValue=600,
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    Symbol = "ETH",
                    TransactionDate = DateTime.Now
                },
                new Transaction
                {
                    Amount = 3,
                    DollarValue=150,
                    Status = Constants.TRANSACTION_DEPOSITED,
                    StatusImageSource = Constants.TRANSACTION_DEPOSITED_IMAGE,
                    Symbol = "LTC",
                    TransactionDate = DateTime.Now
                },

            });

        }

    }
}

