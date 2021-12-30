﻿using System;
using System.Threading.Tasks;
using cryptolletfdm.Common.Base;
using Xamarin.Forms;

namespace cryptolletfdm.Common.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        Task PopAsync();
        Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        Task GoBackAsync();
    }

    public class ShellRoutingService : INavigationService
    {

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        public Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("//", parameters);
        }

        public Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("", parameters);
        }

        private Task GoToAsync<TViewModel>(string routePrefix, string parameters) where TViewModel : BaseViewModel
        {
            var route = routePrefix + typeof(TViewModel).Name;
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                route += $"?{parameters}";
            }
            return Shell.Current.GoToAsync(route);
        }
    }
}
