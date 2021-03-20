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
                RaisePropertyChanged(nameof(ActivityHeightRequest));
                RaisePropertyChanged(nameof(ActivityList));
            }
        }
        private ObservableCollection<MenuToDay> menuList = new ObservableCollection<MenuToDay>();
        public ObservableCollection<MenuToDay> MenuList
        {
            get => menuList;
            set => SetProperty(ref menuList, value);
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
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
        public decimal ActivityHeightRequest
        {
            get
            {
                var count = ActivityList?.Count ?? 0;
                return 80 * (count + 1);
            }
        }
        private string _EatingComment;
        public string EatingComment
        {
            get => _EatingComment;
            set 
            {
                _EatingComment = value;
                RaisePropertyChanged(nameof(EatingComment));
                RaisePropertyChanged(nameof(IsEatingCommentVisible));
            }
        }
        public bool IsEatingCommentVisible { get => !string.IsNullOrEmpty(EatingComment); }
        private string _StudyingComment;
        public string StudyingComment
        {
            get => _StudyingComment;
            set
            {
                _StudyingComment = value;
                RaisePropertyChanged(nameof(_StudyingComment));
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
        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                SelectedDate = DateTime.Now;
                studentId = AppConstants.User.StudentID;
                classId = AppConstants.User.ClassID;
                gradeId = AppConstants.User.GradeID;
                ActivityList = new ObservableCollection<ExerciseClass>(new List<ExerciseClass>());
                await GetDailyActivity(SelectedDate);
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
            //await GetMenuList();
            //await GetSleepActivity();
            //await GetPooActivity();
        }
        #endregion

        #region Private methods
        private async Task GetSleepActivity()
        {
            var sleepActivity = await _activityService.GetTodaySleep(studentId, gradeId, SelectedDate.ToString("yyyy/MM/dd hh:mm"));
            if(sleepActivity?.Data?.Any() == true)
            {
                var sleepItem = sleepActivity.Data?.First();
                SleepFrom = sleepItem.SleepFrom;
                SleepTo = sleepItem.SleepTo;
            }
        }
        private async Task GetPooActivity()
        {
            var pooActivity = await _activityService.GetTodayPoo(studentId, SelectedDate.ToString("yyyy/MM/dd hh:mm"));
            if (pooActivity?.Data?.Any() == true)
            {
                var pooItem = pooActivity.Data?.First();
                PooNumber = pooItem.Hygiene ?? 0;
            }
        }
        private async Task GetActivityList()
        {
            var selectedDate = SelectedDate.ToString("yyyy/MM/dd hh:mm");
            var listMorningActivity = await _activityService.GetMorningActivity(studentId, classId, selectedDate);
            var listAfternoonActivity = await _activityService.GetAfternoonActivity(studentId, classId, selectedDate);

            StudyingComment = listAfternoonActivity?.Data?.FirstOrDefault().NhanXet;
            HasAnyActivity = listMorningActivity?.Data?.Any() == true || listAfternoonActivity?.Data?.Any() == true;

            var listActivities = new List<DailyActivityResponseModel>();
            var listBinding = new List<ExerciseClass>();
            if (listMorningActivity?.Data?.Any() == true) listActivities.AddRange(listMorningActivity.Data);
            if (listAfternoonActivity?.Data?.Any() == true) listActivities.AddRange(listAfternoonActivity.Data);
            foreach(var item in listActivities)
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
            ActivityList = new ObservableCollection<ExerciseClass>(listBinding);
        }

        private async Task GetMenuList()
        {
            var date = SelectedDate.ToString("yyyy/MM/dd");
            var listMenu = await _activityService.GetTodayMenu(studentId, gradeId, date);
            EatingComment = listMenu?.Data?.FirstOrDefault()?.MealComment;
            HasAnyActivity = HasAnyActivity ? HasAnyActivity : listMenu?.Data?.Any() == true;
            var menuList = new List<MenuToDay>();
            if(listMenu?.Data?.Any() == true)
            {
                foreach(var item in listMenu.Data)
                {
                    menuList.Add(new MenuToDay
                    {
                        Id = item.ID,
                        Time = item.BuaAn,
                        Content = item.MonAn,
                        Comment = item.MealComment
                    });
                }
                EatingComment = listMenu.Data.First().MealComment;
            }
            MenuList = new ObservableCollection<MenuToDay>(menuList);
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

