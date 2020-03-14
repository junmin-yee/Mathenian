using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;

namespace Mathenian.ViewModels
{
    public class SignInPageViewModel : BindableBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand SubmitCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public SignInPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            SubmitCommand = new DelegateCommand(ExecuteSubmitCommand);
        }

        async void ExecuteSubmitCommand()
        {
            Account account = await App.Database.GetAccountByCredentialAsync(Username, Hash(Password));

            if (account == null)
                await _dialogService.DisplayAlertAsync("Warning", "Credentials do not match", "Ok");
            else
            {
                if (account.LastLoggedIn.AddDays(1).Date == DateTime.Now.Date)
                    account.DailyStreak += 1;
                else if (account.LastLoggedIn.Date != DateTime.Now.Date)
                    account.DailyStreak = 0;
                account.LastLoggedIn = DateTime.Now;
                App.Theme.UpdateTheme(account.IsDarkMode);

                var parameters = new NavigationParameters
                {
                    { "Account", account },
                    { "IsResult", false }
                };

                await _navigationService.NavigateAsync("/MainPage", parameters);
            }
        }

        private string Hash(string password)
        {
            return password; // Make safer xd
        }
    }
}
