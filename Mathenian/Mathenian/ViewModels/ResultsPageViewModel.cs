using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private int _numCorrect;
        private Topic _topic;
        private Account _userAccount;

        public DelegateCommand NavigateCommand { get; private set; }
        
        private readonly INavigationService _navigationService;

        public ResultsPageViewModel(INavigationService navigationService)
        {
            Title = "Results Page";
            _navigationService = navigationService;

            NavigateCommand = new DelegateCommand(ExecuteNavigateCommand);
        }

        async void ExecuteNavigateCommand()
        {
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
            int numQuestions = parameters.GetValue<int>("NumQuestions");

            _userAccount = parameters.GetValue<Account>("Account");
            _userAccount.TotalQuestionsAttempted += numQuestions;
            _userAccount.TotalQuestionsCompleted += _numCorrect;

            Title = _topic.ToString() + " Results Page";
            Results = string.Format("{0} correct out of {1} questions", _numCorrect, numQuestions);
        }
    }
}
