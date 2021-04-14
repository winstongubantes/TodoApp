using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using TodoApp.Services;
using TodoApp.Views;
using Xamarin.Forms;

namespace TodoApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IAuthService _authService;
        private readonly IPageDialogService _dialogService;
        private string _userName;
        private string _password;

        #endregion Private Fields

        #region Public Constructors

        public LoginPageViewModel(
            INavigationService navigationService, 
            IAuthService authService, 
            IPageDialogService dialogService)
            : base(navigationService)
        {
            _authService = authService;
            _dialogService = dialogService;
            LoginCommand = new DelegateCommand(OnLoginCommand);

            Title = "Todo App";
        }

        #endregion Public Constructors

        #region Public Properties

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        #endregion Public Properties

        #region Private Methods

        private void OnLoginCommand()
        {
            var resultAuth = _authService.IsAuthenticated(UserName, Password);

            if (resultAuth.IsAuthenticated)
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
            else _dialogService.DisplayAlertAsync("Authentication!", resultAuth.Message, "Ok");
        }

        #endregion Private Methods
    }
}
