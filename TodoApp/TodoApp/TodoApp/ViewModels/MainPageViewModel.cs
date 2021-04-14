using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Services;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Views;

namespace TodoApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        #region Private Fields

        private readonly ITodoService _todoService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IMediaService _mediaService;
        private ObservableCollection<TodoItem> _todoItems;

        #endregion Private Fields

        #region Public Constructors

        public MainPageViewModel(
            INavigationService navigationService, 
            ITodoService todoService, 
            IPageDialogService pageDialogService, 
            IMediaService mediaService)
            : base(navigationService)
        {
            _todoService = todoService;
            _pageDialogService = pageDialogService;
            _mediaService = mediaService;
            AddTodoItemCommand = new DelegateCommand(OnAddTodoItemCommand);
            EditTodoItemCommand = new DelegateCommand<TodoItem>(OnEditTodoItemCommand);
            DeleteTodoItemCommand = new DelegateCommand<TodoItem>(OnDeleteTodoItemCommand);
            SetTodoItemDoneCommand = new DelegateCommand<TodoItem>(OnSetTodoItemDoneCommand);

            Title = "Todo List";
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand AddTodoItemCommand { get; }

        public ICommand EditTodoItemCommand { get; }

        public ICommand DeleteTodoItemCommand { get; }

        public ICommand SetTodoItemDoneCommand { get; }

        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set => SetProperty(ref _todoItems, value);
        }

        #endregion Public Properties

        #region Public Methods

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await PopulateTodoItem();
        }

        public override async void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            await PopulateTodoItem();
        }

        #endregion Public Methods

        #region Private Methods

        private async void OnSetTodoItemDoneCommand(TodoItem obj)
        {
            if (string.IsNullOrWhiteSpace(obj.ImageUrl))
            {
                var photoUrl = await _mediaService.CapturePhotoAsync();
                if (string.IsNullOrWhiteSpace(photoUrl)) return;

                obj.ImageUrl = photoUrl;
            }

            obj.IsDone = true;
            await _todoService.UpdateTodoItem(obj);

            await PopulateTodoItem();
        }

        private async Task PopulateTodoItem()
        {
            var todoItems = await _todoService.GetTodoItems();
            TodoItems = new ObservableCollection<TodoItem>(todoItems);
        }

        private async void OnDeleteTodoItemCommand(TodoItem item)
        {
            var isDelete = await _pageDialogService.DisplayAlertAsync("Delete Todo",
                "Are you sure you want to delete this item?", "Yes", "No");

            if (isDelete)
            {
                await _todoService.DeleteTodoItem(item);
                await PopulateTodoItem();
            }
        }

        private void OnEditTodoItemCommand(TodoItem item)
        {
            var param = new NavigationParameters
            {
                {nameof(TodoItem), item}
            };

            NavigationService.NavigateAsync(nameof(TodoItemDetailPage), param);
        }

        private void OnAddTodoItemCommand()
        {
            NavigationService.NavigateAsync(nameof(TodoItemDetailPage));
        }

        #endregion Private Methods

    }
}
