using System;
using System.Collections.ObjectModel;
using KIDS.MOBILE.APP.PARENTS.Services;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Pickup
{
    public class PickupViewModel : BaseViewModel
    {
        private ObservableCollection<MessageModel> _messagesList = new ObservableCollection<MessageModel>();
        public ObservableCollection<MessageModel> MessagesList
        {
            get => _messagesList;
            set => SetProperty(ref _messagesList, value);
        }
        private ILeaveRequestService _leaveRequestService;

        public PickupViewModel(INavigationService navigationService, ILeaveRequestService leaveRequestService) : base(navigationService)
        {
            _navigationService = navigationService;
            _leaveRequestService = leaveRequestService;
        }
    }
}
