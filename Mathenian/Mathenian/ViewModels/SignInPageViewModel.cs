using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class SignInPageViewModel : BindableBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
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
                var parameters = new NavigationParameters
                {
                    { "Account", account },
                    { "IsSignIn", true }
                };

                await _navigationService.NavigateAsync("NavigationPage/MainPage", parameters);
            }
        }

        private string Hash(string password)
        {
            return password; // Make safer xd
        }
    }
}
