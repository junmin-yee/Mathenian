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

        private Account _userAccount;
        public Account UserAccount { get => _userAccount; set => _userAccount = value; }

        private UserCompletion _userCompletion;
        public UserCompletion UserCompletion { get => _userCompletion; set => _userCompletion = value; }

        private Color[] _buttonColors = new Color[] { Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue,
                                        Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue };
        public Color[] ButtonColors { get => _buttonColors; set => _buttonColors = value; }

        private Theme _theme;
        public Theme Theme { get => _theme; set => _theme = value; }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        public DelegateCommand SignOutCommand { get; private set; }
        public DelegateCommand ProfileCommand { get; private set; }

        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;

            _userCompletion = new UserCompletion();
            _userAccount = new Account();
            Theme = App.Theme;

            NavigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand);
            SignOutCommand = new DelegateCommand(ExecuteSignOutCommand);
            ProfileCommand = new DelegateCommand(ExecuteProfileCommand);
        }

        async void ExecuteNavigateCommand(string parameter)
        {
            Enum.TryParse(parameter, out Topic topic);

            var parameters = new NavigationParameters
            {
                { "Topic", topic },
                { "Mastery", UserCompletion.Lessons[topic].Mastery },
                { "Account", UserAccount }
            };

            await _navigationService.NavigateAsync("IntroductionPage", parameters);
        }

        async void ExecuteSignOutCommand()
        {
            UserAccount.Completion = UserCompletion.GenerateForDatabase();
            await App.Database.SaveAccountAsync(UserAccount);
            await _navigationService.NavigateAsync("/StartPage");
        }

        async void ExecuteProfileCommand()
        {
            var parameters = new NavigationParameters
            {
                { "Account", UserAccount }
            };

            await _navigationService.NavigateAsync("ProfilePage", parameters);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            UserAccount = parameters.GetValue<Account>("Account");
            UserCompletion.UpdateFromDatabase(UserAccount.Completion);

            if (parameters.GetValue<bool>("IsResult"))
            {
                Topic topic = parameters.GetValue<Topic>("Topic");
                UserCompletion.Update(topic, parameters.GetValue<int>("PercentIncrease"));
                UserAccount.Completion = UserCompletion.GenerateForDatabase();
            }

            foreach (Topic topic in Enum.GetValues(typeof(Topic)))
            {
                ButtonColors[(int)topic] = UpdateButtonColor(topic);
            }
            RaisePropertyChanged("ButtonColors");
        }

        private Color UpdateButtonColor(Topic topic)
        {
            Color color = Color.LightBlue;
            if (UserCompletion.Lessons[topic].IsMastered())
                color = Color.SlateGray;
            else
            {
                switch (UserCompletion.Lessons[topic].Mastery)
                {
                    case Mastery.Silver:
                        color = Color.Brown;
                        break;
                    case Mastery.Gold:
                        color =  Color.Silver;
                        break;
                    case Mastery.Platinum:
                        color = Color.Gold;
                        break;
                }
            }
            return color;
        }
    }
}
