using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Views.MedicineAdvise;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.MedicineAdvise
{
    public class MedicineAdviseViewModel : BaseViewModel
    {
        #region Properties
        private IMedicineAdviseService _messageService;
        private ObservableCollection<MessageModel> _messageList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessageList
        {
            get => _messageList;
            set => SetProperty(ref _messageList, value);
        }
        public DelegateCommand AddCommand { get; }
        #endregion

        #region Contructor
        public MedicineAdviseViewModel(INavigationService navigationService, IMedicineAdviseService messageService) : base(navigationService)
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
            if (data?.Data?.Any() == true)
            {
                var messageList = new List<MessageModel>();
                foreach (var item in data.Data)
                {
                    messageList.Add(new MessageModel
                    {
                        Id = item.ID ?? Guid.Empty,
                        ReceivedUser = item.NguoiGui,
                        DateTime = item.Date != null ? item.Date.Value.ToShortDateString() : string.Empty,
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
            await _navigationService.NavigateAsync(nameof(CreteMedicineAdvisePage), param);
        }
        #endregion
    }
}