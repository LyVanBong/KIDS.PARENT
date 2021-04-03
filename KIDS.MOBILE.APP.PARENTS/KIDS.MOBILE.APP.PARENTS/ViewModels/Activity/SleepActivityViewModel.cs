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
        }
        #endregion

        #region Private methods
        private async Task GetMenuList()
        {
            var selectedDate = SelectedDate.ToString("yyyy/MM/dd");
            var listMenu = await _activityService.GetTodayMenu(studentId, gradeId, selectedDate);
            HasAnyActivity = listMenu?.Data?.Any() == true;
            EatingComment = listMenu?.Data?.FirstOrDefault()?.MealComment;
            var menuList = new List<MenuToDay>();
            if(listMenu?.Data?.Any() == true)
            {
                foreach(var item in listMenu.Data)
                {
                    var menu = new MenuToDay
                    {
                        Id = item.ID,
                        Time = item.BuaAn,
                        Content = item.TenMonAn,
                        Comment = item.MealComment
                    };
                    if(listMenu.Data.IndexOf(item) == 0)
                    {
                        menu.Image = ImageSource.FromFile("breakfast.png");
                        
                    }
                    else if(listMenu.Data.IndexOf(item) == listMenu.Data.Count -1 )
                    {
                        menu.Image = ImageSource.FromFile("dinner.png");
                    }
                    else
                    {
                        menu.Image = ImageSource.FromFile("lunch.jpeg");
                    }
                    var colorIndex = listMenu.Data.IndexOf(item) % 5;
                    switch (colorIndex)
                    {
                        case 0:
                            menu.TextColor = Color.FromHex("#ff7700");
                            break;
                        case 1:
                            menu.TextColor = Color.FromHex("#002aff");
                            break;
                        case 2:
                            menu.TextColor = Color.FromHex("#ff00c3");
                            break;
                        case 3:
                            menu.TextColor = Color.FromHex("#9d00ff");
                            break;
                        default:
                            menu.TextColor = Color.FromHex("#ff0000");
                            break;
                    }
                    menuList.Add(menu);
                }
                EatingComment = listMenu.Data.First().MealComment;
            }
            MenuList = new ObservableCollection<MenuToDay>(menuList);
        }
        #endregion
    }
}

