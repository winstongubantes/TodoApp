using Prism.Mvvm;
using Prism.Navigation;

namespace TodoApp.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        #region Private Fields

        private string _title;
        private readonly INavigationService _navigationService;

        #endregion Private Fields

        #region Public Constructors

        public ViewModelBase(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #endregion Public Properties

        #region Protected Properties

        protected INavigationService NavigationService => _navigationService;

        #endregion Protected Properties

        #region Public Methods

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        #endregion Public Methods
    }
}