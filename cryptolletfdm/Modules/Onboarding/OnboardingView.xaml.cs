using System;
using System.Collections.Generic;
using Autofac;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Onboarding
{
    public partial class OnboardingView : ContentPage
    {
        public OnboardingView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<OnboardingViewModel>();
        }
    }
}
