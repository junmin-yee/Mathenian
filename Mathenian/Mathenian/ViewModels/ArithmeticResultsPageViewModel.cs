using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
	public class ArithmeticResultsPageViewModel : BindableBase, INavigationAware
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

        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        public ArithmeticResultsPageViewModel(INavigationService navigationService)
        {
            Title = "Arithmetic Results Page";
            _navigationService = navigationService;
        }

        async void ExecuteNavigateCommand()
        {
            await _navigationService.GoBackToRootAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Results = parameters.GetValue<int>("NumCorrect").ToString() + " correct out of 10 questions";
        }
    }
}
