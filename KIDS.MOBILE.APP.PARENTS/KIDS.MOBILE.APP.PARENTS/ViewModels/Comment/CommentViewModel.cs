using System;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services;
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
        #endregion

        #region Constructor
        public CommentViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.Initialize(parameters);
                var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                await GetAttendanceForMonth(date);
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
        #endregion

        #region Methods
        public async Task GetAttendanceForMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var data = await _leaveRequestService.GetAttendanceForMonth(AppConstants.User.ClassID.ToString(),
                AppConstants.User.StudentID.ToString(),
                startDate,
                startDate.AddMonths(1).AddDays(-1));
            if (data?.Data?.Any() == true)
            {
                var comment = data.Data.First();
                StudyingComment = $"{comment.StudyCommentAM}{comment.StudyCommentPM}";
                EatingComment = $"{comment.MealComment0}{comment.MealComment1}{comment.MealComment2}{comment.MealComment3}{comment.MealComment4}{comment.MealComment5}";
                SleepingComment = $"{comment.SleepComment}";
                PooComment = $"{comment.HygieneComment}";
                ImageSource = !string.IsNullOrEmpty(comment.PhieuBeNgoan) ? new Uri(comment.PhieuBeNgoan) : null;
            }
        }
        #endregion
    }
}
