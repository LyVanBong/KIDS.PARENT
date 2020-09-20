using System;
using System.Collections.ObjectModel;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using Prism;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Notification
{
    public class NotificationViewModel : BaseViewModel, IActiveAware
    {
        private ObservableCollection<NotificationModel> _dataNotification;
        private INotificationService _notificationService;
        private bool _isActive;
        private bool _loadNotification;

        public bool LoadNotification
        {
            get => _loadNotification;
            set => SetProperty(ref _loadNotification, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, IsActiveChange);
        }

        public event EventHandler IsActiveChanged;
        public ObservableCollection<NotificationModel> DataNotification
        {
            get => _dataNotification;
            set => SetProperty(ref _dataNotification, value);
        }

        public NotificationViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private async void IsActiveChange()
        {
            if (IsActive)
            {
                LoadNotification = true;
                var notification = new { SchoolId = AppConstants.User.ClassID, StudentId = AppConstants.User.StudentID };
                var data = await _notificationService.GetNotification(notification.SchoolId, notification.StudentId);
                if (data.Code > 0)
                {
                    DataNotification = new ObservableCollection<NotificationModel>(data.Data);
                }

                LoadNotification = false;
            }
            else
            {

            }
        }
    }
}