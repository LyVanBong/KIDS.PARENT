using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.Views.LeaveRequest;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.LeaveRequest
{
    public class LeaveRequestViewModel : BaseViewModel
    {
        
        private ObservableCollection<MessageModel> _messagesList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessagesList
        {
            get => _messagesList;
            set => SetProperty(ref _messagesList, value);
        }

        private ObservableCollection<AbsentInformationModel> _informationList = new ObservableCollection<AbsentInformationModel>();
        public ObservableCollection<AbsentInformationModel> InformationList
        {
            get => _informationList;
            set => SetProperty(ref _informationList, value);
        }
        private ILeaveRequestService _leaveRequestService;
        private string fromDate;
        public string FromDate 
        {
            get => fromDate;
            set => SetProperty(ref fromDate, value);
        }
        private string toDate;
        public string ToDate 
        {
            get => toDate;
            set => SetProperty(ref toDate, value);
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<object> SelectedCommand { get; }

        public LeaveRequestViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
            AddCommand = new DelegateCommand(OnAddClick);
            SelectedCommand = new DelegateCommand<object>(OnSelectedClick);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.Initialize(parameters);
                var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                FromDate = date.Date.ToString("yyyy-MM-dd");
                ToDate = date.AddMonths(1).AddDays(-1).Date.ToString("yyyy-MM-dd");
                await GetInformationList();
                await GetMessagesList();
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

        private async Task GetInformationList()
        {
            var data = await _leaveRequestService.GetAttendanceInformation(AppConstants.User.ClassID, AppConstants.User.StudentID, FromDate, ToDate);
            if(data?.Data?.Any() == true)
            {
                var information = data.Data.First();
                var info = new List<AbsentInformationModel>
                {
                    new AbsentInformationModel
                    {
                        Title = Resource._00100,
                        Number = information.NghiCoPhep.ToString(),
                        BackgroundGradientStart = "#5e7cea",
                        BackgroundGradientEnd = "#1dcce3"
                    },
                    new AbsentInformationModel
                    {
                        Title = Resource._00101,
                        Number = information.NghiKhongPhep.ToString(),
                        BackgroundGradientStart = "#255ea6",
                        BackgroundGradientEnd = "#b350d1"
                    },
                    new AbsentInformationModel
                    {
                        Title = "Đi muộn",
                        Number = information.DiMuon.ToString(),
                        BackgroundGradientStart = "#ff7272",
                        BackgroundGradientEnd = "#f650c5"
                    }
                };
                InformationList = new ObservableCollection<AbsentInformationModel>(info);
            }
        }

        private async void OnSelectedClick(object item)
        {
            var data = (Syncfusion.ListView.XForms.ItemTappedEventArgs)item;
            var param = new NavigationParameters();
            param.Add("message", data.ItemData);
            param.Add("isUpdate", true);
            await _navigationService.NavigateAsync(nameof(CreateLeaveRequestPage), param);
        }

        private async Task GetMessagesList()
        {
            var listRequest = new List<MessageModel>();
            var listResult = await _leaveRequestService.GetListLeaveRequest(AppConstants.User.StudentID);
            if (listResult?.Data?.Any() == true)
            {
                foreach(var item in listResult.Data)
                {
                    listRequest.Add(new MessageModel
                    {
                        ReceivedUser = item.NguoiGui,
                        DateTime = item.Date?.ToString("yyyy-MM-dd") ?? string.Empty,
                        ImageUrl = $"{AppConstants.UriBaseWebForm}{item.Picture}",
                        Comment = item.Content,
                        TimePeriod = $"{Resource._00105} {item.FromDate?.ToString("yyyy-MM-dd")} {Resource._00106} {item.ToDate?.ToString("yyyy-MM-dd")}",
                        Approved = item.Status == true ? Resource._00103 : Resource._00104
                    });
                }
            }
            MessagesList = new ObservableCollection<MessageModel>(listRequest);
        }

        private async void OnAddClick()
        {
            var param = new NavigationParameters();
            param.Add("isUpdate", false);
            await _navigationService.NavigateAsync(nameof(CreateLeaveRequestPage), param);
        }
    }

    public class AbsentInformationModel
    {
        public string Title { get; set; }
        public string Number { get; set; }
        public string BackgroundGradientStart { get; set; }
        public string BackgroundGradientEnd { get; set; }
    }
}
