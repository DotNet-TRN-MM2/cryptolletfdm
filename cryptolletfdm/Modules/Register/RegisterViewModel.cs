﻿using System.Threading.Tasks;
using System.Windows.Input;
using cryptolletfdm.Common.Base;
using cryptolletfdm.Common.Database;
using cryptolletfdm.Common.Models;
using cryptolletfdm.Common.Navigation;
using cryptolletfdm.Common.Security;
using cryptolletfdm.Common.Settings;
using cryptolletfdm.Common.Validation;
using cryptolletfdm.Modules.Login;
using Xamarin.Forms;

namespace cryptolletfdm.Modules.Register
{
    public class RegisterViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IRepository<User> _userRepository;
        private IUserPreferences _userPreferences;

        public RegisterViewModel(INavigationService navigationService,
            IRepository<User> repository,
            IUserPreferences userPreferences)
        {
            _navigationService = navigationService;
            _userRepository = repository;
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

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        public ICommand LoginCommand { get => new Command(async () => await GoToLogin()); }
        public ICommand RegisterUserCommand { get => new Command(async () => await RegisterUser(), () => IsNotBusy); }

        private async Task RegisterUser()
        {
            if (!EntriesCorrectlyPopulated())
            {
                return;
            }
            IsBusy = true;
            var user = new User()
            {
                Email = Email.Value,
                FirstName = Name.Value,
                HashedPassword = SecurePasswordHasher.Hash(Password.Value)
            };
            await _userRepository.SaveAsync(user);

            _userPreferences.Set(Constants.IS_USER_LOGGED_IN, true);
            _userPreferences.Set(Constants.USER_ID, Email.Value);
            _navigationService.GoToMainFlow();
            IsBusy = false;
        }

        private async Task GoToLogin()
        {
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }

        private void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            _name = new ValidatableObject<string>();

            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email is empty." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not in a correct format." });

            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password is empty." });

            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Name is empty." });
        }

        private bool EntriesCorrectlyPopulated()
        {
            _email.Validate();
            _password.Validate();
            _name.Validate();

            return _email.IsValid && _password.IsValid && _name.IsValid;
        }
    }
}
