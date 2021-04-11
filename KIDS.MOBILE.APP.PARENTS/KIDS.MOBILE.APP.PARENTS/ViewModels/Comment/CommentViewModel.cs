using System;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.Services.Activity;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using KIDS.MOBILE.APP.PARENTS.Services.Popup;
using KIDS.MOBILE.APP.PARENTS.Views.Message;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Comment
{
    public class CommentViewModel : BaseViewModel
    {
        #region Properties
        private ILeaveRequestService _leaveRequestService;

        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get => _ImageSource;
            set => SetProperty(ref _ImageSource, value);
        }

        private string _StudyingComment;
        public string StudyingComment
        {
            get => _StudyingComment;
            set => SetProperty(ref _StudyingComment, value);
        }

        private string _SleepingComment;
        public string SleepingComment
        {
            get => _SleepingComment;
            set => SetProperty(ref _SleepingComment, value);
        }

        private string _EatingComment;
        public string EatingComment
        {
            get => _EatingComment;
            set => SetProperty(ref _EatingComment, value);
        }

        private string _PooComment;
        public string PooComment
        {
            get => _PooComment;
            set => SetProperty(ref _PooComment, value);
        }

        private ImageSource _WeeklyImageSource;
        public ImageSource WeeklyImageSource
        {
            get => _WeeklyImageSource;
            set => SetProperty(ref _WeeklyImageSource, value);
        }

        private string _WeeklyComment;
        public string WeeklyComment
        {
            get => _WeeklyComment;
            set => SetProperty(ref _WeeklyComment, value);
        }

        private string sleepFrom;
        public string SleepFrom
        {
            get => sleepFrom;
            set => SetProperty(ref sleepFrom, value);
        }

        private string sleepTo;
        public string SleepTo
        {
            get => sleepTo;
            set => SetProperty(ref sleepTo, value);
        }

        private int pooNumber;
        public int PooNumber
        {
            get => pooNumber;
            set => SetProperty(ref pooNumber, value);
        }

        private string dailyComment;
        public string DailyComment
        {
            get => dailyComment;
            set => SetProperty(ref dailyComment, value);
        }

        public DelegateCommand ThanksCommand { get; }

        private IActivityService _activityService;
        private IInputAlertDialogService _popup;
        private IMessageService _messageService;
        #endregion

        #region Constructor
        public CommentViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService, IActivityService activityService, IInputAlertDialogService popup, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
            _activityService = activityService;
            _messageService = messageService;
            _popup = popup;
            ThanksCommand = new DelegateCommand(OnThanksClicked);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.Initialize(parameters);
                var date = DateTime.Now;
                await GetAttendanceForMonth(date);
                await GetSleepActivity(date);
                await GetPooActivity(date);
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

        public void OnDisappering()
        {
            _popup.ClosePopup();
        }
        #endregion

        #region Methods
        public async Task GetAttendanceForMonth(DateTime date)
        {
            try
            {
                var data = await _activityService.GetMorningActivity(AppConstants.User.StudentID, AppConstants.User.ClassID, date.ToString("yyyy-MM-dd")); ;
                if (data?.Data?.Any() == true)
                {
                    var comment = data.Data.First();
                    StudyingComment = $"{comment.NhanXetHoc}";
                    EatingComment = $"{comment.NhanXetAn}";
                    SleepingComment = $"{comment.NhanXetNgu}";
                    PooComment = $"{comment.NhanXetWC}";
                    DailyComment = $"{comment.NhanXetNgay}";
                    ImageSource = !string.IsNullOrEmpty(comment.PhieuBeNgoanNgay) ? new Uri($"{AppConstants.UriBaseWebForm}{comment.PhieuBeNgoanNgay}") : null;
                    WeeklyComment = $"{comment.NhanXetTuan}";
                    //WeeklyImageSource = !string.IsNullOrEmpty(comment.WeekPhieuBeNgoan) ? new Uri(comment.WeekPhieuBeNgoan) : null;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetSleepActivity(DateTime date)
        {
            try
            {
                var sleepActivity = await _activityService.GetTodaySleep(AppConstants.User.StudentID, AppConstants.User.GradeID, date.ToString("yyyy/MM/dd"));
                if (sleepActivity?.Data?.Any() == true)
                {
                    var sleepItem = sleepActivity.Data?.First();
                    SleepFrom = sleepItem.SleepFrom;
                    SleepTo = sleepItem.SleepTo;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetPooActivity(DateTime date)
        {
            try
            {
                var pooActivity = await _activityService.GetTodayPoo(AppConstants.User.StudentID, date.ToString("yyyy/MM/dd"));
                if (pooActivity?.Data?.Any() == true)
                {
                    var pooItem = pooActivity.Data?.First();
                    PooNumber = pooItem.Hygiene ?? 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void OnThanksClicked()
        {
            try
            {
                var message = await _popup.OpenThanksMessagePopup(string.Empty);
                if(message != null)
                {
                    var model = new CreateMessageModel
                    {
                        ClassID = AppConstants.User.ClassID,
                        TeacherID = string.Empty, //TODO
                        Parent = AppConstants.User.ParentID.ToString(),
                        Content = message.Value1,
                        DateCreate = DateTime.Now,
                        StudentID = AppConstants.User.StudentID,
                        Type = 2
                    };
                    var result = await _messageService.CreateMessage(model);
                    if (result?.Data == 1)
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00097, string.Empty, Resource._00011);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00063, string.Empty, Resource._00011);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}
