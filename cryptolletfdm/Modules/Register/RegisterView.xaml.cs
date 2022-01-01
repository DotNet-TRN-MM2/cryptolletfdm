using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Register
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<RegisterViewModel>();
        }
    }
}
