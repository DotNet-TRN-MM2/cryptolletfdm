﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using cryptolletfdm.Common.Base;
using cryptolletfdm.Common.Controllers;
using cryptolletfdm.Common.Models;
using cryptolletfdm.Common.Navigation;
using cryptolletfdm.Modules.AddTransaction;
using cryptolletfdm.Modules.Wallet;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Transactions
{
    public class TransactionsViewModel: BaseViewModel
    {
        private INavigationService _navigationService;
        private IWalletController _walletController;
        private string _filter = string.Empty;

        public TransactionsViewModel(INavigationService navigationService,
                                     IWalletController walletController)
        {
            _navigationService = navigationService;
            _walletController = walletController;
            Transactions = new ObservableCollection<Transaction>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            _filter = parameter.ToString();
            await GetTransactions();
        }

        private async Task GetTransactions()
        {
            IsRefreshing = true;
            var transactions = await _walletController.GetTransactions();
            if (!string.IsNullOrEmpty(_filter))
            {
                transactions = transactions.Where(x => x.Status == _filter).ToList();
            }
            Transactions = new ObservableCollection<Transaction>(transactions);
            IsRefreshing = false;
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { SetProperty(ref _isRefreshing, value); }
        }

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set { SetProperty(ref _transactions, value); }
        }

        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set { SetProperty(ref _selectedTransaction, value); }
        }

        public ICommand TransactionSelectedCommand { get => new Command(async () => await TransactionSelected()); }

        private async Task TransactionSelected()
        {
            if (SelectedTransaction == null)
            {
                return;
            }
            await _navigationService.PushAsync<AddTransactionViewModel>($"id={SelectedTransaction.Id}");
            SelectedTransaction = null;
        }

        public ICommand RefreshTransactionsCommand { get => new Command(async () => await RefreshTransactions()); }

        private async Task RefreshTransactions()
        {
            await GetTransactions();
        }

        public ICommand TradeCommand { get => new Command(async () => await PerformNavigation()); }

        private async Task PerformNavigation()
        {
            await _navigationService.PushAsync<AddTransactionViewModel>();
        }
    }
}

