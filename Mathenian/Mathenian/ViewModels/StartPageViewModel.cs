using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Mathenian.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        public DelegateCommand SignUpCommand { get; private set; }
        public DelegateCommand SignInCommand { get; private set; }
        private readonly INavigationService _navigationService;

        public StartPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SignUpCommand = new DelegateCommand(ExecuteSignUpCommand);
            SignInCommand = new DelegateCommand(ExecuteSignInCommand);
        }


        async void ExecuteSignUpCommand()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }

        async void ExecuteSignInCommand()
        {
            await _navigationService.NavigateAsync("SignInPage");
        }
    }
}
