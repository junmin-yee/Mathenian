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
        public string Title { get => _title; set => _title = value; }

        private string _results;
        public string Results
        {
            get => _results;
            set
            {
                _results = value;
                RaisePropertyChanged("Results");
            }
        }

        private int _numCorrect;
        private Topic _topic;

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
            var parameters = new NavigationParameters();
            parameters.Add("Topic", _topic);
            parameters.Add("PercentIncrease", _numCorrect * 2);

            await _navigationService.GoBackToRootAsync(parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _numCorrect = parameters.GetValue<int>("NumCorrect");
            _topic = parameters.GetValue<Topic>("Topic");
            Results = string.Format("{0} correct out of {1} questions", _numCorrect.ToString(), parameters.GetValue<int>("NumQuestions"));
        }
    }
}
