using System;
using System.Collections.Generic;
using Autofac;
using cryptolletfdmfdm.Modules.Assets;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Assets
{
    public partial class AssetsView : ContentPage
    {
        public AssetsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AssetsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AssetsViewModel).InitializeAsync(null);
        }
    }
}