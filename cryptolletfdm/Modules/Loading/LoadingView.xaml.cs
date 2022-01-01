using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Loading
{
    public partial class LoadingView : ContentPage
    {
        public LoadingView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<LoadingViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as LoadingViewModel).InitializeAsync(null);
        }
    }
}
