﻿using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Transactions
{
    public partial class WithdrawnTransactionsView : ContentPage
    {
        public WithdrawnTransactionsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<TransactionsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as TransactionsViewModel).InitializeAsync(Constants.TRANSACTION_WITHDRAWN);
        }
    }
}
