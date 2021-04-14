using Prism;
using Prism.Ioc;
using TodoApp.Services;
using TodoApp.ViewModels;
using TodoApp.Views;
using Xamarin.Forms;

namespace TodoApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthService, AuthService>();
            containerRegistry.RegisterSingleton<ITodoService, TodoService>();
            containerRegistry.RegisterSingleton<IMediaService, MediaService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<TodoItemDetailPage, TodoItemDetailPageViewModel>();
        }
    }
}
