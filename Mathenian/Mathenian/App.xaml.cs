using Prism;
using Prism.Ioc;
using Mathenian.ViewModels;
using Mathenian.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mathenian.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mathenian
{
    public partial class App
    {
        static MathenianDatabase database;
        static Theme theme;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/StartPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LessonPage, LessonPageViewModel>();
            containerRegistry.RegisterForNavigation<ResultsPage, ResultsPageViewModel>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<IntroductionPage, IntroductionPageViewModel>();
            containerRegistry.RegisterForNavigation<TestPage, TestPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
        }

        public static MathenianDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MathenianDatabase();
                }
                return database;
            }
        }

        public static Theme Theme
        {
            get
            {
                if (theme == null)
                {
                    theme = new Theme();
                }
                return theme;
            }
        }
    }
}
