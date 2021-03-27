using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using KIDS.MOBILE.APP.PARENTS.ViewModels.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Views.Pickup;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Interfaces;
using Xamarin.Plugin.Calendar.Models;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Pickup
{
    public class PickupViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<MessageModel> _messagesList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessagesList
        {
            get => _messagesList;
            set => SetProperty(ref _messagesList, value);
        }

        private string _TimePeriod;
        public string TimePeriod
        {
            get => _TimePeriod;
            set => SetProperty(ref _TimePeriod, value);
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get => _SelectedDate;
            set => SetProperty(ref _SelectedDate, value);
        }

        private EventCollection _Events = new EventCollection();
        public EventCollection Events
        {
            get => _Events;
            set => SetProperty(ref _Events, value);
        }

        private int _Month;
        public int Month
        {
            get => _Month;
            set
            {
                _Month = value;
                RaisePropertyChanged(nameof(Month));
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GetAttendanceForMonth(new DateTime(SelectedDate.Year, Month, 1));
                });
            }
        }

        private StudentModel _Student;
        public StudentModel Student
        {
            get => _Student;
            set => SetProperty(ref _Student, value);
        }

        private ObservableCollection<AbsentInformationModel> _informationList = new ObservableCollection<AbsentInformationModel>();
        public ObservableCollection<AbsentInformationModel> InformationList
        {
            get => _informationList;
            set => SetProperty(ref _informationList, value);
        }

        private IUserService _userService;
        private ILeaveRequestService _leaveRequestService;
        private GetAttendanceResponseModel dataCount;
        #endregion

        #region Constructor
        public PickupViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService, IUserService userService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
            _userService = userService;
            SelectedDate = DateTime.Now;
            Month = DateTime.Now.Month;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                await GetInformationList();
                await GetStudentInformation();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
            //await GetAttendanceForMonth(SelectedDate);
        }
        #endregion

        #region Methods
        private async Task GetStudentInformation()
        {
            var student = await _userService.GetStudent(AppConstants.User.StudentID);
            if (student?.Code > 0)
            {
                Student = student.Data.FirstOrDefault();
            }
        }

        public async Task GetAttendanceForMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var data = await _leaveRequestService.GetAttendanceForMonth(AppConstants.User.ClassID.ToString(),
                AppConstants.User.StudentID.ToString(),
                startDate,
                startDate.AddMonths(1).AddDays(-1));
            if (data?.Data?.Any() == true)
            {
                var events = new EventCollection();
                foreach (var dataItem in data.Data)
                {
                    Color color = GetColor(dataItem);
                    events.Add(
                        dataItem.Date.Value,
                        new DayEventCollection<object>{EventIndicatorColor = color, EventIndicatorSelectedColor = color });
                }
                Events = events;
            }
        }

        private async Task GetInformationList()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var data = await _leaveRequestService.GetAttendanceInformation(AppConstants.User.ClassID, AppConstants.User.StudentID, startDate.ToString("yyyy-MM-dd"),startDate.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"));
            if (data?.Data?.Any() == true)
            {
                var information = data.Data.First();
                var info = new List<AbsentInformationModel>
                {
                    new AbsentInformationModel
                    {
                        Title = Resource._00100,
                        Number = information.NghiCoPhep.ToString(),
                        BackgroundGradientStart = "#039612",
                        BackgroundGradientEnd = "#039612"
                    },
                    new AbsentInformationModel
                    {
                        Title = Resource._00101,
                        Number = information.NghiKhongPhep.ToString(),
                        BackgroundGradientStart = "#ff0a0a",
                        BackgroundGradientEnd = "#ff0a0a"
                    },
                    new AbsentInformationModel
                    {
                        Title = "Đi muộn",
                        Number = information.DiMuon.ToString(),
                        BackgroundGradientStart = "#d000f5",
                        BackgroundGradientEnd = "#d000f5"
                    },
                    new AbsentInformationModel
                    {
                        Title = "Số ngày đi học",
                        Number = information.STT.ToString(),
                        BackgroundGradientStart = "#ff6d00",
                        BackgroundGradientEnd = "#ff6d00"
                    },
                };
                InformationList = new ObservableCollection<AbsentInformationModel>(info);
            }
        }

        private Color GetColor(GetAttendanceForMonthModel data)
        {
            if (data?.NghiKhongPhep == true)
            {
                return Color.FromHex("#ff0a0a");
            }
            else if (data?.NghiCoPhep == true)
            {
                return Color.FromHex("#039612");
            }
            else if (data?.DiMuon == true)
            {
                return Color.FromHex("#d000f5");
            }
            return Color.FromHex("#ff6d00");
        }
        #endregion
    }

    public class DayEventCollection<T> : List<T>, IPersonalizableDayEvent
    {
        /// <summary>
        /// Empty contructor extends from base()
        /// </summary>
        public DayEventCollection() : base()
        {

        }

        /// <summary>
        /// Color contructor extends from base()
        /// </summary>
        /// <param name="eventIndicatorColor"></param>
        /// <param name="eventIndicatorSelectedColor"></param>
        public DayEventCollection(Color? eventIndicatorColor, Color? eventIndicatorSelectedColor) : base()
        {
            EventIndicatorColor = eventIndicatorColor;
            EventIndicatorSelectedColor = eventIndicatorSelectedColor;
        }

        /// <summary>
        /// IEnumerable contructor extends from base(IEnumerable collection)
        /// </summary>
        /// <param name="collection"></param>
        public DayEventCollection(IEnumerable<T> collection) : base(collection)
        {

        }

        /// <summary>
        /// Capacity contructor extends from base(int capacity)
        /// </summary>
        /// <param name="capacity"></param>
        public DayEventCollection(int capacity) : base(capacity)
        {

        }

        #region PersonalizableProperties
        public Color? EventIndicatorColor { get; set; }
        public Color? EventIndicatorSelectedColor { get; set; }
        public Color? EventIndicatorTextColor { get; set; }
        public Color? EventIndicatorSelectedTextColor { get; set; }

        #endregion

    }
}
