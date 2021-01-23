using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.LeaveRequest
{
    public class CreateLeaveRequestViewModel : BaseViewModel
    {
        #region Properties
        private ILeaveRequestService _leaveRequestService;
        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set => SetProperty(ref messageContent, value);
        }
        private DateTime selectedfromDate;
        public DateTime SelectedFromDate
        {
            get => selectedfromDate;
            set => SetProperty(ref selectedfromDate, value);
        }
        private DateTime selectedToDate;
        public DateTime SelectedToDate
        {
            get => selectedToDate;
            set => SetProperty(ref selectedToDate, value);
        }
        public DelegateCommand SendCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        private bool _isUpdate;
        public bool IsUpdate
        {
            get => _isUpdate;
            set => SetProperty(ref _isUpdate, value);
        }
        private MessageModel CurrentMessage { get; set; }
        #endregion

        #region Contructor
        public CreateLeaveRequestViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
            SendCommand = new DelegateCommand(OnSendClick);
            DeleteCommand = new DelegateCommand(OnDeleteClick);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                SelectedFromDate = DateTime.Now;
                SelectedToDate = DateTime.Now;
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
                if (parameters.ContainsKey("isUpdate"))
                {
                    IsUpdate = (bool?)parameters["isUpdate"] ?? false;
                }

                if (parameters.ContainsKey("message"))
                {
                    CurrentMessage = parameters["message"] as MessageModel;
                }

                if (IsUpdate)
                {
                    MessageContent = CurrentMessage?.Comment;
                    if (!string.IsNullOrEmpty(CurrentMessage.TimePeriod))
                    {
                        var split = CurrentMessage.TimePeriod.Split(' ');
                        string startDate = null;
                        string endDate = null;
                        foreach(var item in split)
                        {
                            if (item.Any(char.IsDigit))
                            {
                                if (!string.IsNullOrEmpty(startDate))
                                {
                                    startDate = item;
                                }
                                else
                                {
                                    endDate = item;
                                }
                            }
                        }
                        SelectedFromDate = !string.IsNullOrEmpty(startDate) ? DateTime.Parse(startDate) : SelectedFromDate;
                        SelectedToDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : SelectedToDate;
                    }
                    
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
                if (!IsUpdate)
                {
                    var model = new CreateLeaveRequestModel
                    {
                        ClassID = AppConstants.User.ClassID,
                        StudentID = AppConstants.User.StudentID,
                        FromDate = SelectedFromDate,
                        ToDate = SelectedFromDate,
                        Date = DateTime.Now,
                        Content = MessageContent
                    };
                    var result = await _leaveRequestService.CreateLeaveRequest(model);
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
                    var model = new CreateLeaveRequestModel
                    {
                        FromDate = SelectedFromDate,
                        ToDate = SelectedToDate,
                        Date = DateTime.Now,
                        Content = MessageContent,
                        ID = CurrentMessage.Id
                    };
                    var result = await _leaveRequestService.UpdateLeaveRequest(model);
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

        private async void OnDeleteClick()
        {
            var model = new CreateLeaveRequestModel
            {
                ID = CurrentMessage.Id
            };
            var result = await _leaveRequestService.DeleteLeaveRequest(model);
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
        #endregion
    }
}
