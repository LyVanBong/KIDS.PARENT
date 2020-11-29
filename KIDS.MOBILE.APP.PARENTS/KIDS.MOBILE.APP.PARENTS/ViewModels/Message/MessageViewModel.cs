using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        #region Properties
        
        private ObservableCollection<MessageModel> _messageList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }
        #endregion

        #region Contructor
        public MessageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            MessageList = new ObservableCollection<MessageModel>(GetMessagesList());
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private List<MessageModel> GetMessagesList()
        {
            
            return new List<MessageModel> {
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    Image="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    Image="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    Image="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                },
                new MessageModel
                {
                    ReceivedUser ="Toroto",
                    DateTime = DateTime.Now.ToLongDateString(),
                    Image="",
                    Comment = "Mở tài khoản ngay, " +
                    "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
                    "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
                    "ngay khi mở tà khoản Standard Chartered EliteFly"
                }
            };
        }
        #endregion
    }

    public class MessageModel
    {
        public string ReceivedUser { get; set; }
        public string ReceivedUserText { get => "Gui toi " + ReceivedUser; }
        public string DateTime { get; set; }
        public ImageSource Image { get; set; }
        public string Comment { get; set; }
        public string ShortComment {
            get {
                const int length = 100;
                if(Comment.Length > length)
                {
                    return Comment.Substring(0, length) + "...";
                }
                return Comment;
            } }
        public string CommentText { get => "Viet binh luan"; }
    }
}
