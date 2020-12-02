using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        public LeaveRequestViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            MessagesList = new ObservableCollection<MessageModel>(GetMessagesList());
            InformationList = new ObservableCollection<AbsentInformationModel>(GetInformationList());
        }

        private List<AbsentInformationModel> GetInformationList()
        {
            return new List<AbsentInformationModel>()
            {
                new AbsentInformationModel
                {
                    Title = "Tổng",
                    Number = "23",
                    BackgroundGradientStart = "#f59083",
                    BackgroundGradientEnd = "#fae188"
                },
                new AbsentInformationModel
                {
                    Title = "Nghỉ",
                    Number = "5",
                    BackgroundGradientStart = "#ff7272",
                    BackgroundGradientEnd = "#f650c5"
                },
                new AbsentInformationModel
                {
                    Title = "Có phép",
                    Number = "3",
                    BackgroundGradientStart = "#5e7cea",
                    BackgroundGradientEnd = "#1dcce3"
                },
                new AbsentInformationModel
                {
                    Title = "Không phép",
                    Number = "2",
                    BackgroundGradientStart = "#255ea6",
                    BackgroundGradientEnd = "#b350d1"
                }
            };
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
