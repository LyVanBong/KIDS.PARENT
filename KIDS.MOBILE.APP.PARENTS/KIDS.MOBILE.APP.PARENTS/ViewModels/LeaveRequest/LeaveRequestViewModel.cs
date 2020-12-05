using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services;
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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public LeaveRequestViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.Initialize(parameters);
                MessagesList = new ObservableCollection<MessageModel>(GetMessagesList());
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
            var data = await _leaveRequestService.GetAttendanceInformation(AppConstants.User.ClassID, AppConstants.User.StudentID, DateFrom.ToShortDateString(), DateTo.ToShortDateString());
            if(data?.Data?.Any() == true)
            {
                var information = data.Data.First();
                var info = new List<AbsentInformationModel>
                {
                        new AbsentInformationModel
                        {
                            Title = Resource._00098,
                            Number = information.STT.ToString(),
                            BackgroundGradientStart = "#f59083",
                            BackgroundGradientEnd = "#fae188"
                        },
                        new AbsentInformationModel
                        {
                            Title = Resource._00099,
                            Number = information.CoMat.ToString(),
                            BackgroundGradientStart = "#ff7272",
                            BackgroundGradientEnd = "#f650c5"
                        },
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
                        }
                };
                InformationList = new ObservableCollection<AbsentInformationModel>(info);
            }
        }

        private List<MessageModel> GetMessagesList()
        {

            return new List<MessageModel> {
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    ImageUrl="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    ImageUrl="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    ImageUrl="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    ImageUrl="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                }
            };
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
