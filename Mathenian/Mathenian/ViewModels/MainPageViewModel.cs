using Mathenian.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Mathenian.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title { get => _title; set => _title = value; }

        private UserCompletion _userCompletion;
        public UserCompletion UserCompletion { get => _userCompletion; set => _userCompletion = value; }

        private Color[] _buttonColors = new Color[] { Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue,
                                        Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue };
        public Color[] ButtonColors { get => _buttonColors; set => _buttonColors = value; }

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
            Topic topic = parameters.GetValue<Topic>("Topic");
            UserCompletion.Update(topic, parameters.GetValue<int>("PercentIncrease"));

            switch (UserCompletion.Lessons[topic].Mastery)
            {
                case Mastery.Silver:
                    ButtonColors[(int)topic] = Color.Brown;
                    break;
                case Mastery.Gold:
                    ButtonColors[(int)topic] = Color.Silver;
                    break;
                case Mastery.Platinum:
                    ButtonColors[(int)topic] = Color.SlateGray;
                    break;
            }
            RaisePropertyChanged("ButtonColors");
        }
    }
}
