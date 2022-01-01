using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Login
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<LoginViewModel>();
        }
    }
}
