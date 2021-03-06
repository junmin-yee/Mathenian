﻿using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Mathenian.ViewModels
{
    public class ResultsPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get => _title;
            set { SetProperty(ref _title, value); }
        }

        private string _results;
        public string Results
        {
            get => _results;
            set { SetProperty(ref _results, value); }
        }

        private Theme _theme;
        public Theme Theme { get => _theme; set => _theme = value; }

        private int _numQuestions;
        private int _numCorrect;
        private Topic _topic;
        private Account _userAccount;
        private Score _score;

        public DelegateCommand NavigateCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public ResultsPageViewModel(INavigationService navigationService)
        {
            Title = "Results Page";
            _navigationService = navigationService;
            _theme = App.Theme;

            NavigateCommand = new DelegateCommand(ExecuteNavigateCommand);
        }

        async void ExecuteNavigateCommand()
        {
            if (_score != null)
            {
                _score.TotalCorrect += _numCorrect;
                _score.TotalAttempt += _numQuestions;
                await App.Database.SaveScoreAsync(_score);
            }

            var parameters = new NavigationParameters
            {
                { "Topic", _topic },
                { "PercentIncrease", _numCorrect * 10 },
                { "IsResult", true },
                { "Account", _userAccount }
            };
            await _navigationService.NavigateAsync("/MainPage", parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _numCorrect = parameters.GetValue<int>("NumCorrect");
            _topic = parameters.GetValue<Topic>("Topic");
            _numQuestions = parameters.GetValue<int>("NumQuestions");

            _userAccount = parameters.GetValue<Account>("Account");
            _userAccount.TotalQuestionsAttempted += _numQuestions;
            _userAccount.TotalQuestionsCompleted += _numCorrect;

            _score = parameters.GetValue<Score>("Score");

            Title = _topic.ToString() + " Results Page";
            Results = string.Format("{0} correct out of {1} questions", _numCorrect, _numQuestions);
        }
    }
}
