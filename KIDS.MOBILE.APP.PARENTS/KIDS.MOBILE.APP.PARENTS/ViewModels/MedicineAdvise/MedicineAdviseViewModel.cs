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
        public DelegateCommand<object> SelectedCommand { get; }
        private bool hasAnyMessages;
        public bool HasAnyMessages
        {
            get => hasAnyMessages;
            set => SetProperty(ref hasAnyMessages, value);
        }
        #endregion

        #region Contructor
        public MedicineAdviseViewModel(INavigationService navigationService, IMedicineAdviseService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            AddCommand = new DelegateCommand(OnAddClick);
            SelectedCommand = new DelegateCommand<object>(OnSelectedClicked);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                IsLoading = true;
                HasAnyMessages = false;
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

        public async void OnAppearing()
        {
            try
            {
                IsLoading = true;
                await GetMessagesList();
            }
            catch (Exception ex)
            {

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
            var data = await _messageService.GetAllMedicineAdvise(studentId);
            if (data?.Data?.Any() == true)
            {
                var messageList = new List<MessageModel>();
                foreach (var item in data.Data)
                {
                    messageList.Add(new MessageModel
                    {
                        Id = item.ID ?? Guid.Empty,
                        ReceivedUser = item.NguoiGui,
                        TimePeriod = item.Date != null ? item.Date.Value.ToString("dd-MM-yyyy") : string.Empty,
                        ImageUrl = $"{AppConstants.UriBaseWebForm}{item.Picture}",
                        Comment = item.Content
                    });
                }
                messageList = messageList.OrderByDescending(x => x.DateTime).ToList();
                MessageList = new ObservableCollection<MessageModel>(messageList);
                HasAnyMessages = true;
            }
        }

        private async void OnAddClick()
        {
            var param = new NavigationParameters();
            param.Add("isUpdate", false);
            await _navigationService.NavigateAsync(nameof(CreteMedicineAdvisePage), param);
        }

        private async void OnSelectedClicked(object dataItem)
        {
            var data = (Syncfusion.ListView.XForms.ItemTappedEventArgs)dataItem;
            var param = new NavigationParameters();
            param.Add("Id", ((MessageModel)data.ItemData).Id);
            param.Add("isUpdate", true);

            await _navigationService.NavigateAsync(nameof(CreteMedicineAdvisePage), param);
        }
        #endregion
    }
}