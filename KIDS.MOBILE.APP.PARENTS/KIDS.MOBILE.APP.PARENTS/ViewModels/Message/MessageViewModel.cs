using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        #region Properties
        private IMessageService _messageService;
        private ObservableCollection<MessageModel> _messageList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }
        #endregion

        #region Contructor
        public MessageViewModel(INavigationService navigationService, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                //MessageList = new ObservableCollection<MessageModel>(GetMessagesList());
                IsLoading = true;
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
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private async Task GetMessagesList()
        {

            //return new List<MessageModel> {
            //    new MessageModel
            //    {
            //        ReceivedUser ="Toroto",
            //        DateTime = DateTime.Now.ToLongDateString(),
            //        Image="",
            //        Comment = "Mở tài khoản ngay, " +
            //        "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
            //        "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
            //        "ngay khi mở tà khoản Standard Chartered EliteFly"
            //    },
            //    new MessageModel
            //    {
            //        ReceivedUser ="Toroto",
            //        DateTime = DateTime.Now.ToLongDateString(),
            //        Image="",
            //        Comment = "Mở tài khoản ngay, " +
            //        "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
            //        "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
            //        "ngay khi mở tà khoản Standard Chartered EliteFly"
            //    },
            //    new MessageModel
            //    {
            //        ReceivedUser ="Toroto",
            //        DateTime = DateTime.Now.ToLongDateString(),
            //        Image="",
            //        Comment = "Mở tài khoản ngay, " +
            //        "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
            //        "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
            //        "ngay khi mở tà khoản Standard Chartered EliteFly"
            //    },
            //    new MessageModel
            //    {
            //        ReceivedUser ="Toroto",
            //        DateTime = DateTime.Now.ToLongDateString(),
            //        Image="",
            //        Comment = "Mở tài khoản ngay, " +
            //        "tích lũy lên đến 360.000 dặm thưởng, tận hưởng chuyến bay 0 đồng." +
            //        "💥 Tận hưởng chuyến bay 0 đồng Vietnam Airline với cơ hội  tích lũy lên đến 360.000 dặm thưởng " +
            //        "ngay khi mở tà khoản Standard Chartered EliteFly"
            //    }
            //};
            var studentId = AppConstants.User.StudentID;
            var data = await _messageService.GetAllSentMessage(studentId);
            if(data?.Data?.Any() == true)
            {
                var messageList = new List<MessageModel>();
                foreach(var item in data.Data)
                {
                    messageList.Add(new MessageModel
                    {
                        Id = item.CommunicationID,
                        ReceivedUser = item.TeacherID.ToString(),
                        DateTime = item.DateCreate != null ? item.DateCreate.Value.ToShortDateString() : string.Empty,
                        ImageUrl = $"{AppConstants.UriBaseWebForm}{item.Picture}",
                        Comment = item.Content
                    });
                }
                MessageList = new ObservableCollection<MessageModel>(messageList);
            }
        }
        #endregion
    }

    public class MessageModel
    {
        public Guid Id { get; set; }
        public string ReceivedUser { get; set; }
        public string ReceivedUserText { get =>  $"{Resource._00090}{ReceivedUser}"; }
        public string DateTime { get; set; }
        public string ImageUrl { get; set; }
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
