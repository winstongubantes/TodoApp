using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.ViewModels
{
    public class TodoItemDetailPageViewModel : ViewModelBase
    {

        #region Private Fields

        private readonly ITodoService _todoService;
        private readonly IMediaService _mediaService;
        private readonly IPageDialogService _dialogService;

        private bool _isEdit;
        private TodoItem _todoItem;
        private string _buttonText = "Add";
        private string _description;
        private DateTime _taskDateTime = DateTime.Now;
        private string _imageUrl;

        #endregion Private Fields

        #region Public Constructors

        public TodoItemDetailPageViewModel(
            INavigationService navigationService, 
            ITodoService todoService, 
            IMediaService mediaService, 
            IPageDialogService dialogService)
            : base(navigationService)
        {
            _todoService = todoService;
            _mediaService = mediaService;
            _dialogService = dialogService;
            UpdateCommand = new DelegateCommand(OnUpdateCommand);
            TakePictureCommand = new DelegateCommand(OnTakePictureCommand);

            Title = "Add Todo";
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand UpdateCommand { get; }

        public ICommand TakePictureCommand { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime TaskDateTime
        {
            get => _taskDateTime;
            set => SetProperty(ref _taskDateTime, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        #endregion Public Properties

        #region Public Methods

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            _isEdit = parameters.ContainsKey(nameof(TodoItem));

            if (_isEdit)
            {
                _todoItem = parameters.GetValue<TodoItem>(nameof(TodoItem));
                Description = _todoItem.Description;
                TaskDateTime = _todoItem.TaskDateTime;
                ImageUrl = _todoItem.ImageUrl;
                ButtonText = "Update";
                Title = "Edit Todo";
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async void OnTakePictureCommand()
        {
            var pictureCapturedPath = await _mediaService.CapturePhotoAsync();
            ImageUrl = pictureCapturedPath;
        }

        private async void OnUpdateCommand()
        {
            var inputValidation = ValidateInputs();

            if (!inputValidation.IsValid)
            {
                await _dialogService.DisplayAlertAsync("Invalid", inputValidation.Message, "Ok");
                return;
            }

            if (_isEdit)
            {
                _todoItem.Description = Description;
                _todoItem.TaskDateTime = TaskDateTime;
                _todoItem.ImageUrl = ImageUrl;

                await _todoService.UpdateTodoItem(_todoItem);
            }
            else
            {
                _todoItem = new TodoItem
                {
                    Description = Description,
                    TaskDateTime = TaskDateTime,
                    ImageUrl = ImageUrl
                };

                await _todoService.AddTodoItem(_todoItem);
            }

            await NavigationService.GoBackAsync();
        }

        private (bool IsValid, string Message) ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Description)) return (false, "Description must not be empty");

            return (true, string.Empty);
        }

        #endregion Private Methods

    }
}
