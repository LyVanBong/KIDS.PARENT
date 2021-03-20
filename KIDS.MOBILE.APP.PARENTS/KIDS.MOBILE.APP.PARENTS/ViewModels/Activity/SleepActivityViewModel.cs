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
    public class SleepActivityViewModel : BaseViewModel
    {
        #region Properties
        private IActivityService _activityService;
        private ObservableCollection<MenuToDay> menuList = new ObservableCollection<MenuToDay>();
        public ObservableCollection<MenuToDay> MenuList
        {
            get => menuList;
            set {
                menuList = value;
                RaisePropertyChanged(nameof(MenuList));
            }
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
        private bool _IsDailyCommentVisible;
        public bool IsDailyCommentVisible
        {
            get => _IsDailyCommentVisible;
            set => SetProperty(ref _IsDailyCommentVisible, value);
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
        
        private string studentId;
        private string classId;
        private string gradeId;

        private bool hasAnyActivity;
        public bool HasAnyActivity
        {
            get => hasAnyActivity;
            set => SetProperty(ref hasAnyActivity, value);
        }
        #endregion

        #region Contructor
        public SleepActivityViewModel(INavigationService navigationService,IActivityService activityService) : base(navigationService)
        {
            _navigationService = navigationService;
            _activityService = activityService;
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
            await GetMenuList();
            await GetSleepActivity();
            await GetPooActivity();
        }
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

        private async Task GetMenuList()
        {
            var date = SelectedDate.ToString();
            var listMenu = await _activityService.GetTodayMenu(studentId, gradeId, date);
            HasAnyActivity = HasAnyActivity ? HasAnyActivity : listMenu?.Data?.Any() == true;
            EatingComment = listMenu?.Data?.FirstOrDefault()?.MealComment;
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
}

