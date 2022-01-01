using System;
using System.Threading.Tasks;
using System.Windows.Input;
using cryptolletfdm.Common.Base;
using cryptolletfdm.Common.Navigation;
using cryptolletfdm.Modules.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace cryptolletfdm
{
    public class AppShellViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public AppShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SignOutCommand { get => new Command(async () => await SignOut()); }

        private async Task SignOut()
        {
            Preferences.Remove(Constants.IS_USER_LOGGED_IN);
            _navigationService.GoToLoginFlow();
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
