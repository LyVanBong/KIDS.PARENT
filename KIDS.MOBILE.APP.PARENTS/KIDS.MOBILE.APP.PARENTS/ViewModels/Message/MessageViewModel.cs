using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using KIDS.MOBILE.APP.PARENTS.Views.Message;
using Prism.Commands;
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
        public DelegateCommand AddCommand { get;}
        #endregion

        #region Contructor
        public MessageViewModel(INavigationService navigationService, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            AddCommand = new DelegateCommand(OnAddClick);
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
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
                        ReceivedUser = item.NguoiGui.ToString(),
                        DateTime = item.DateCreate != null ? item.DateCreate.Value.ToShortDateString() : string.Empty,
                        ImageUrl = $"{AppConstants.UriBaseWebForm}{item.Picture}",
                        Comment = item.Content
                    });
                }
                MessageList = new ObservableCollection<MessageModel>(messageList);
            }
        }

        private async void OnAddClick()
        {
            var param = new NavigationParameters();
            param.Add("isUpdate", false);
            await _navigationService.NavigateAsync(nameof(CreateMessagePage),param);
        }
        #endregion
    }

    public class MessageModel
    {
        public Guid Id { get; set; }
        public string ReceivedUser { get; set; }
        public string ReceivedUserText { get =>  $"{Resource._00090}{ReceivedUser}"; }
        public string TimePeriod { get; set; }
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
        public string Approved { get; set; }
    }
}
