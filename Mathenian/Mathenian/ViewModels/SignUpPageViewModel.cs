using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class SignUpPageViewModel : BindableBase
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

        public SignUpPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SubmitCommand = new DelegateCommand(ExecuteSubmitCommand);
        }

        async void ExecuteSubmitCommand()
        {
            UserCompletion userCompletion = new UserCompletion();
            Account account = new Account
            {
                Username = Username,
                Password = Hash(Password),
                Completion = userCompletion.GenerateForDatabase(),
                TotalQuestionsCompleted = 0,
                TotalQuestionsAttempted = 0,
                DailyStreak = 0,
                LastLoggedIn = DateTime.Now,
                IsDarkMode = 0
            };

            await App.Database.SaveItemAsync(account);

            account = await App.Database.GetAccountByCredentialAsync(Username, Hash(Password));

            var parameters = new NavigationParameters
            {
                { "Account", account },
                { "IsResult", false }
            };

            await _navigationService.NavigateAsync("/MainPage", parameters);
        }

        private string Hash(string password)
        {
            return password; // Make safer xd
        }
    }
}
