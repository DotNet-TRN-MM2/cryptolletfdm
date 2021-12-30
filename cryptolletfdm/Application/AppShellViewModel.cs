using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cryptollet.Common.Base;
using Xamarin.Forms;

namespace cryptolletfdm
{
    public class AppShellViewModel : BaseViewModel
    {
        public ICommand SignOutCommand { get => new Command(async () => await SignOut()); }

        private async Task SignOut()
        {
            await Shell.Current.DisplayAlert("todo", "you have been logged out.", "OK");
        }

        public ICommand AssetCommand { get => new Command(async () => await Asset()); }

        private async Task Asset()
        {

        }

        private object _isRefreshing;
        public object isRefreshing
        {
            get => _isRefreshing;
            set { SetProperty(ref _isRefreshing, value); }
        }
    }
}
