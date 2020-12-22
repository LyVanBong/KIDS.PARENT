using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
using Prism.Navigation;
using Xamarin.Forms;

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

        private ILeaveRequestService _leaveRequestService;

        public PickupViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await GetAttendanceInformation();
            await GetListPickUpMessage();
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

        public async Task<List<GetAttendanceForMonthModel>> GetAttendanceForMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var data = await _leaveRequestService.GetAttendanceForMonth(AppConstants.User.ClassID.ToString(),
                AppConstants.User.StudentID.ToString(),
                startDate,
                startDate.AddMonths(1).AddDays(-1));
            return data?.Data;
        }
    }
}
