using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Notification
{
    public class NotificationViewModel : BaseViewModel, IActiveAware
    {
        private ObservableCollection<NotificationModel> _dataNotification;
        private INotificationService _notificationService;
        private bool _isActive;
        private bool _loadNotification;
        private string _count;

        public string Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

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

        public NotificationViewModel(INavigationService navigationService, INotificationService notificationService) : base(navigationService)
        {
            _notificationService = notificationService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await CountNotification();
        }

        private async Task CountNotification()
        {
            try
            {
                var data = await _notificationService.CountNotification();
                if (data != null && data.Data > 0)
                {
                    if (data.Data > 9)
                    {
                        Count = "9+";
                    }

                    if (data.Data < 10 && data.Data > 0)
                    {
                        Count = data.Data + "";
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        private async void IsActiveChange()
        {
            if (IsActive)
            {
                LoadNotification = true;
                var notification = new { SchoolId = AppConstants.User.ClassID, StudentId = AppConstants.User.StudentID };
                var data = await _notificationService.GetNotification(notification.SchoolId, notification.StudentId);
                if (data != null && data.Code > 0)
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