using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class ProfilePageViewModel : BindableBase, INavigationAware
    {
        private string _username;
        public string Username { get => _username; set { SetProperty(ref _username, value); } }

        private string _totalCorrectQuestions;
        public string TotalCorrectQuestions { get => _totalCorrectQuestions; set { SetProperty(ref _totalCorrectQuestions, value); } }

        private string _accuracy;
        public string Accuracy { get => _accuracy; set { SetProperty(ref _accuracy, value); } }

        private string _dailyStreak;
        public string DailyStreak { get => _dailyStreak; set { SetProperty(ref _dailyStreak, value); } }

        private bool _isDarkMode;
        public bool IsDarkMode 
        { 
            get => _isDarkMode;
            set { SetProperty(ref _isDarkMode, value); }
        }

        private Account _userAccount;

        public DelegateCommand NavigateCommand { get; private set; }
        private readonly INavigationService _navigationService;

        public ProfilePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand(ExecuteNavigateCommand);
            _username = "";
            _totalCorrectQuestions = "";
            _accuracy = "";
            _dailyStreak = "";
            _isDarkMode = false;
        }

        async void ExecuteNavigateCommand()
        {
            var parameters = new NavigationParameters
            {
                { "Account", _userAccount }
            };

            await _navigationService.GoBackAsync(parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _userAccount = parameters.GetValue<Account>("Account");
            Username = _userAccount.Username;
            TotalCorrectQuestions = string.Format("Total # of Correct Questions: {0}", _userAccount.TotalQuestionsCompleted);
            double accuracy = (_userAccount.TotalQuestionsAttempted == 0) ? 1 : (double)_userAccount.TotalQuestionsCompleted / _userAccount.TotalQuestionsAttempted;
            Accuracy = string.Format("Percent Correct: {0:0%}", accuracy);
            DailyStreak = string.Format("Daily Streak: {0}", _userAccount.DailyStreak);
            IsDarkMode = _userAccount.IsDarkMode != 0;
        }
    }
}
