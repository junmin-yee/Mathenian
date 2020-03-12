using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
    public class IntroductionPageViewModel : BindableBase, INavigationAware
    {
        private string _introImageSource;
        public string IntroImageSource
        {
            get => _introImageSource;
            set { SetProperty(ref _introImageSource, value); }
        }

        private Theme _theme;
        public Theme Theme { get => _theme; set => _theme = value; }

        private Topic _topic;
        private Mastery _mastery;
        private Account _userAccount;
        private Score _score;

        public DelegateCommand LessonCommand { get; private set; }
        public DelegateCommand TestCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public IntroductionPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _theme = App.Theme;

            LessonCommand = new DelegateCommand(ExecuteLessonCommand);
            TestCommand = new DelegateCommand(ExecuteTestCommand);
        }

        async void ExecuteLessonCommand()
        {
            _score = await App.Database.GetScoreByAttributes(0, (int)_topic, (int)_mastery);
            if (_score == null)
            {
                _score = new Score()
                {
                    ID = 0,
                    Type = 0,
                    Topic = (int)_topic,
                    Mastery = (int)_mastery,
                    TotalCorrect = 0,
                    TotalAttempt = 0
                };
            }
            var parameters = new NavigationParameters
            {
                { "Topic", _topic },
                { "Mastery", _mastery },
                { "Account", _userAccount },
                { "Score", _score }
            };

            await _navigationService.NavigateAsync("LessonPage", parameters);
        }

        async void ExecuteTestCommand()
        {
            _score = await App.Database.GetScoreByAttributes(1, (int)_topic, (int)_mastery);
            if (_score == null)
            {
                _score = new Score()
                {
                    ID = 0,
                    Type = 1,
                    Topic = (int)_topic,
                    Mastery = (int)_mastery,
                    TotalCorrect = 0,
                    TotalAttempt = 0
                };
            }
            var parameters = new NavigationParameters
            {
                { "Topic", _topic },
                { "Mastery", _mastery },
                { "Account", _userAccount },
                { "Score", _score }
            };

            await _navigationService.NavigateAsync("TestPage", parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _topic = parameters.GetValue<Topic>("Topic");
            _mastery = parameters.GetValue<Mastery>("Mastery");
            _userAccount = parameters.GetValue<Account>("Account");
            IntroImageSource = string.Format("{0}.png", _topic.ToString().ToLower());
        }
    }
}
