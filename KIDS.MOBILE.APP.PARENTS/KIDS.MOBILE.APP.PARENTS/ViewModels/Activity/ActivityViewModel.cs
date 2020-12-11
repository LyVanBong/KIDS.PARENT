using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set => SetProperty(ref _activityList, value);
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
        private string studentId;
        private string classId;
        private string gradeId;
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
                await GetActivityList(SelectedDate.ToString());
                await GetMenuList(SelectedDate.ToString());
                await GetSleepActivity();
                await GetPooActivity();
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
        #endregion

        #region Private methods
        private async Task GetSleepActivity()
        {
            var sleepActivity = await _activityService.GetTodaySleep(studentId, gradeId, SelectedDate.ToString());
            if(sleepActivity?.Data?.Any() == true)
            {
                var sleepItem = sleepActivity.Data?.First();
                SleepFrom = sleepItem.SleepFrom;
                SleepTo = sleepItem.SleepTo;
            }
        }
        private async Task GetPooActivity()
        {
            var pooActivity = await _activityService.GetTodayPoo(studentId, SelectedDate.ToString());
            if (pooActivity?.Data?.Any() == true)
            {
                var pooItem = pooActivity.Data?.First();
                PooNumber = pooItem.Hygiene ?? 0;
            }
        }
        private async Task GetActivityList(string date)
        {
            var listMorningActivity = await _activityService.GetMorningActivity(studentId, classId, date);
            var listAfternoonActivity = await _activityService.GetAfternoonActivity(studentId, classId, date);
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
                    listBinding.Add(new ExerciseClass
                    {
                        Id = item.ID,
                        Instructor = item.Contents,
                        ClassTime = item.ThoiGian,
                        IsLast = false,
                        Title = string.Empty
                    });
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
            Device.BeginInvokeOnMainThread(() =>
            {
                ActivityList = new ObservableCollection<ExerciseClass>(listBinding);
            });
        }

        private async Task GetMenuList(string date)
        {
            var listMenu = await _activityService.GetTodayMenu(studentId, gradeId, date);
            var menuList = new List<MenuToDay>();
            if(listMenu?.Data?.Any() == true)
            {
                foreach(var item in listMenu.Data)
                {
                    menuList.Add(new MenuToDay
                    {
                        Id = item.ID,
                        Time = item.BuaAn,
                        Content = item.MonAn
                    });
                }
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

    public class MenuToDay
    {
        public Guid? Id { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
    }
}

