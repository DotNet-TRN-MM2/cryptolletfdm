using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using cryptolletfdm.Common.Base;
using cryptolletfdm.Common.Localization;
using cryptolletfdm.Common.Database;
using cryptolletfdm.Common.Dialog;
using cryptolletfdm.Common.Models;
using cryptolletfdm.Common.Navigation;
using cryptolletfdm.Common.Security;
using cryptolletfdm.Common.Settings;
using cryptolletfdm.Common.Validation;
using cryptolletfdm.Modules.Register;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IRepository<User> _userRepository;
        private IDialogMessage _dialogMessage;
        private IUserPreferences _userPreferences;

        public LoginViewModel(INavigationService navigationService,
            IRepository<User> repository,
            IDialogMessage message,
            IUserPreferences userPreferences)
        {
            _navigationService = navigationService;
            _userRepository = repository;
            _dialogMessage = message;
            _userPreferences = userPreferences;
            AddValidations();
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get => _password;
            set { SetProperty(ref _password, value); }
        }

        public ICommand RegisterCommand { get => new Command(async () => await GoToRegister()); }
        public ICommand LoginCommand { get => new Command(async () => await LoginUser(), () => IsNotBusy); }

        private async Task LoginUser()
        {
            if (!EntriesCorrectlyPopulated())
            {
                return;
            }
            IsBusy = true;
            var user = (await _userRepository.GetAllAsync())
                .FirstOrDefault(x => x.Email == Email.Value);
            if (user == null)
            {
                await DisplayCredentialsError();
                IsBusy = false;
                return;
            }
            if (!SecurePasswordHasher.Verify(Password.Value, user.HashedPassword))
            {
                await DisplayCredentialsError();
                IsBusy = false;
                return;
            }

            _userPreferences.Set(Constants.IS_USER_LOGGED_IN, true);
            _userPreferences.Set(Constants.USER_ID, Email.Value);
            _navigationService.GoToMainFlow();
            IsBusy = false;
        }

        private async Task DisplayCredentialsError()
        {
            await _dialogMessage.DisplayAlert(Resources.Login_Error, Resources.Login_WrongCredentials, "Ok");
            Password.Value = "";
        }

        private async Task GoToRegister()
        {
            await _navigationService.InsertAsRoot<RegisterViewModel>();
        }

        private void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is empty." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not in correct format." });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is empty." });
        }

        private bool EntriesCorrectlyPopulated()
        {
            _email.Validate();
            _password.Validate();

            return _email.IsValid && _password.IsValid;
        }
    }
}

