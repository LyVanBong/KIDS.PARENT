using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.LeaveRequest
{
    public class CreateLeaveRequestViewModel : BaseViewModel
    {
        #region Properties
        private IMessageService _messageService;
        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set => SetProperty(ref messageContent, value);
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }
        public DelegateCommand SendCommand { get; set; }
        private bool isUpdate;
        private MessageModel CurrentMessage { get; set; }
        #endregion

        #region Contructor
        public CreateLeaveRequestViewModel(INavigationService navigationService, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            SendCommand = new DelegateCommand(OnSendClick);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                isUpdate = (bool?)parameters["isUpdate"] ?? false;
                CurrentMessage = parameters["Message"] as MessageModel;
                if (isUpdate)
                {
                    MessageContent = CurrentMessage.Comment;
                    SelectedDate = CurrentMessage.DateTime != null ? DateTime.Parse(CurrentMessage.DateTime) : DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private async void OnSendClick()
        {
            if (string.IsNullOrEmpty(MessageContent))
            {
                await App.Current.MainPage.DisplayAlert(Resource._00063, Resource._00096, Resource._00011);
            }
            else
            {
                if (!isUpdate)
                {
                    var model = new CreateMessageModel
                    {
                        ClassID = AppConstants.User.ClassID,
                        TeacherID = string.Empty, //TODO
                        Parent = AppConstants.User.ParentID,
                        Content = MessageContent,
                        DateCreate = SelectedDate,
                        StudentID = AppConstants.User.StudentID,
                        Type = 2
                    };
                    var result = await _messageService.CreateMessage(model);
                    if (result.Data == 1)
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00097, string.Empty, Resource._00011);
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00063, string.Empty, Resource._00011);
                    }
                }
                else
                {
                    var model = new CreateMessageModel
                    {
                        CommunicationID = CurrentMessage.Id.ToString(),
                        Content = MessageContent,
                        DateCreate = SelectedDate,
                        StudentID = AppConstants.User.StudentID,
                        Type = 2
                    };
                    var result = await _messageService.UpdateMessage(model);
                    if (result.Data == 1)
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00097, string.Empty, Resource._00011);
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00063, string.Empty, Resource._00011);
                    }
                }
            }
        }
        #endregion
    }
}
