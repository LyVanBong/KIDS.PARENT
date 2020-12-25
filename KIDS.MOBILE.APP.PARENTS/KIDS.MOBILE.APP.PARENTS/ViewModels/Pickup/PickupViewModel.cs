using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
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

        private int _OnTime;
        public int OnTime
        {
            get => _OnTime;
            set => SetProperty(ref _OnTime, value);
        }

        private int _Late;
        public int Late
        {
            get => _Late;
            set => SetProperty(ref _Late, value);
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

        private ILeaveRequestService _leaveRequestService;

        public PickupViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
            SelectedDate = DateTime.Now;
            Month = DateTime.Now.Month;
        }
        private void OnSwipeLeftClicked(object o)
        {
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await GetAttendanceInformation();
            await GetListPickUpMessage();
            //await GetAttendanceForMonth(SelectedDate);
        }

        private async Task GetAttendanceInformation()
        {
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            TimePeriod = $"{Resource._00105} {startDate.ToString("yyyy-MM-dd")} {Resource._00106} {endDate.ToString("yyyy-MM-dd")}";
            var data = await _leaveRequestService.GetAttendanceInformation(AppConstants.User.ClassID, AppConstants.User.StudentID, startDate.ToString(), endDate.ToString());
            if(data?.Data?.Any() == true)
            {
                var item = data.Data.First();
                OnTime = item.CoMat ?? 0;
                Late = item.DiMuon ?? 0;
            }
        }

        private async Task GetListPickUpMessage()
        {

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
                    Color color = dataItem.CoMat == true ? Color.Green : Color.Red;
                    events.Add(
                        dataItem.Date.Value,
                        new DayEventCollection<object>{EventIndicatorColor = color, EventIndicatorSelectedColor = color });
                }
                Events = events;
            }
        }
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
