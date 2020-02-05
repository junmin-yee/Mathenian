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
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title { get => _title; set => _title = value; }

        private UserCompletion _userCompletion;
        public UserCompletion UserCompletion { get => _userCompletion; set => _userCompletion = value; }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;

            _userCompletion = new UserCompletion();

            NavigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand);
        }

        async void ExecuteNavigateCommand(string parameter)
        {
            Topic topic;
            Enum.TryParse(parameter, out topic);

            var parameters = new NavigationParameters();
            parameters.Add("Topic", topic);
            parameters.Add("Mastery", UserCompletion.Lessons[topic].Mastery);

            await _navigationService.NavigateAsync("LessonPage", parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            UserCompletion.Update(parameters.GetValue<Topic>("Topic"), parameters.GetValue<int>("PercentIncrease"));
        }
    }
}
