using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mathenian.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private string _title;
        public string Title { get => _title; set => _title = value; }

        private UserCompletion _userCompletion;
        public UserCompletion UserCompletion { get => _userCompletion; set => _userCompletion = value; }

        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
        }

        async void ExecuteNavigateCommand()
        {
            await _navigationService.NavigateAsync("ArithmeticLessonPage");
        }
    }
}
