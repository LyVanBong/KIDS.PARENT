using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Activity;
using KIDS.MOBILE.APP.PARENTS.Services.Activity;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Activity
{
    public class ActivityViewModel : BaseViewModel
    {
        #region Properties
        private IActivityService _activityService;

        private ObservableCollection<ExerciseClass> _activityList = new ObservableCollection<ExerciseClass>();
        public ObservableCollection<ExerciseClass> ActivityList
        {
            get => _activityList;
            set
            {
                _activityList = value;
                RaisePropertyChanged(nameof(ActivityList));
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        public bool IsActivityVisible
        {
            get => ActivityList.Any();
        }

        private bool _IsDailyCommentVisible;
        public bool IsDailyCommentVisible
        {
            get => _IsDailyCommentVisible;
            set => SetProperty(ref _IsDailyCommentVisible, value);
        }

        private string _StudyingComment;
        public string StudyingComment
        {
            get => _StudyingComment;
            set
            {
                _StudyingComment = value;
                RaisePropertyChanged(nameof(StudyingComment));
                RaisePropertyChanged(nameof(IsStudyingCommentVisible));
            }
        }

        public bool IsStudyingCommentVisible { get => !string.IsNullOrEmpty(StudyingComment); }
        private string studentId;
        private string classId;
        private string gradeId;

        private bool hasAnyActivity;
        public bool HasAnyActivity
        {
            get => hasAnyActivity;
            set => SetProperty(ref hasAnyActivity, value);
        }
        //public DelegateCommand AddCommand { get; }
        #endregion

        #region Contructor
        public ActivityViewModel(INavigationService navigationService,IActivityService activityService) : base(navigationService)
        {
            _navigationService = navigationService;
            _activityService = activityService;
            //AddCommand = new DelegateCommand(OnAddClick);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                SelectedDate = DateTime.Now;
                studentId = AppConstants.User.StudentID;
                classId = AppConstants.User.ClassID;
                gradeId = AppConstants.User.GradeID;
                //ActivityList = new ObservableCollection<ExerciseClass>(new List<ExerciseClass>());
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

        #region Public methods
        public async Task GetDailyActivity(DateTime date)
        {
            SelectedDate = date;
            await GetActivityList();
        }
        #endregion

        #region Private methods
        private async Task GetActivityList()
        {
            try
            {
                ActivityList?.Clear();
                var selectedDate = SelectedDate.ToString("yyyy/MM/dd");
                var listMorningActivity = await _activityService.GetMorningActivity(studentId, classId, selectedDate);
                var listAfternoonActivity = await _activityService.GetAfternoonActivity(studentId, classId, selectedDate);

                HasAnyActivity = listMorningActivity?.Data?.Any() == true || listAfternoonActivity?.Data?.Any() == true;

                var listActivities = new List<DailyActivityResponseModel>();
                var listBinding = new List<ExerciseClass>();
                if (listMorningActivity?.Data?.Any() == true) listActivities.AddRange(listMorningActivity.Data);
                if (listAfternoonActivity?.Data?.Any() == true) listActivities.AddRange(listAfternoonActivity.Data);
                foreach (var item in listActivities)
                {
                    listBinding.Add(new ExerciseClass
                    {
                        Id = item.ID,
                        Instructor = item.Contents,
                        ClassTime = item.ThoiGian,
                        IsLast = false,
                        Title = string.Empty
                    });
                }
                if (listBinding.Any()) listBinding.Last().IsLast = true;
                StudyingComment = listAfternoonActivity?.Data?.FirstOrDefault().NhanXet;
                ActivityList = new ObservableCollection<ExerciseClass>(listBinding);
            }
            catch (Exception ex)
            {

            }
        }

        
        #endregion
    }

    public class ExerciseClass
    {
        public Guid? Id { get; set; }
        public string ClassTime { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }

        public bool IsLast { get; set; } = false;
    }

    public class MenuToDay : INotifyPropertyChanged
    {
        public Guid? Id { get; set; }

        private string _Time;
        public string Time
        {
            get => _Time;
            set {
                _Time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }
        public string Content { get; set; }
        public string Comment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

