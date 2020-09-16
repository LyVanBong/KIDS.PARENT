using System.Collections.ObjectModel;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Notification
{
    public class NotificationViewModel : BaseViewModel
    {
        private ObservableCollection<NotificationModel> _dataNotification;
        private INotificationService _notificationService;
        public ObservableCollection<NotificationModel> DataNotification
        {
            get => _dataNotification;
            set => SetProperty(ref _dataNotification, value);
        }

        public NotificationViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var user = AppConstants.User;
        }
    }
}