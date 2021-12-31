using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using cryptolletfdm.Common.Base;
using cryptolletfdm.Common.Controllers;
using cryptolletfdm.Common.Models;
using cryptolletfdm.Common.Navigation;
using cryptolletfdm.Modules.AddTransaction;
using Xamarin.Forms;

namespace cryptolletfdmfdm.Modules.Assets
{
    public class AssetsViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IWalletController _walletController;

        public AssetsViewModel(INavigationService navigationService,
                               IWalletController walletController)
        {
            _navigationService = navigationService;
            _walletController = walletController;
            Assets = new ObservableCollection<Coin>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            var assets = await _walletController.GetCoins();
            Assets = new ObservableCollection<Coin>(assets);
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set { SetProperty(ref _assets, value); }
        }

        public ICommand AddTransactionCommand { get => new Command(async () => await AddTransaction()); }

        private async Task AddTransaction()
        {
            await _navigationService.PushAsync<AddTransactionViewModel>();
        }
    }
}