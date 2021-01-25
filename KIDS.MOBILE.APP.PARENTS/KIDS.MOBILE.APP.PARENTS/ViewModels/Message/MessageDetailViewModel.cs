using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using Prism.Commands;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Message
{
    public class MessageDetailViewModel : BaseViewModel
    {
        #region Properties
        private IMessageService _messageService;
        private ObservableCollection<MessageModel> _messageList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }
        private MessageModel _MessageModel;
        public MessageModel MessageModel
        {
            get => _MessageModel;
            set => SetProperty(ref _MessageModel, value);
        }
        private ObservableCollection<MessageModel> _CommentList;
        public ObservableCollection<MessageModel> CommentList
        {
            get => _CommentList;
            set => SetProperty(ref _CommentList, value);
        }
        private string _Content;
        public string Content
        {
            get => _Content;
            set => SetProperty(ref _Content, value);
        }
        public DelegateCommand AddCommand { get; }
        #endregion

        #region Contructor
        public MessageDetailViewModel(INavigationService navigationService, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            AddCommand = new DelegateCommand(OnAddCommentClick);
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                MessageModel = (MessageModel)parameters["message"];
                await GetCommentList();
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
        private async Task GetCommentList()
        {
            var data = await _messageService.GetCommentOnMessage(MessageModel.Id.ToString());
            if(data?.Data?.Any() == true)
            {
                var commentList = new List<MessageModel>();
                foreach(var item in data.Data)
                {
                    commentList.Add(new MessageModel
                    {
                        Comment = item.Content,
                        ReceivedUser = item.NguoiGui,
                        DateTime = item.DateCreate?.ToString("yyyy-MM-dd hh:mm")
                    });
                }
                CommentList = new ObservableCollection<MessageModel>(commentList);
            }
        }

        private async void OnAddCommentClick()
        {
            var model = new CreateMessageModel
            {
                ClassID = AppConstants.User.ClassID,
                TeacherID = string.Empty, //TODO
                Parent = MessageModel.Id.ToString(),
                Content = Content,
                DateCreate = DateTime.Now,
                StudentID = AppConstants.User.StudentID,
                Type = 2
            };
            var result = await _messageService.CreateMessage(model);
            if (result?.Data == 1)
            {
                Content = string.Empty;
                await GetCommentList();
            }
        }
        #endregion
    }
}
