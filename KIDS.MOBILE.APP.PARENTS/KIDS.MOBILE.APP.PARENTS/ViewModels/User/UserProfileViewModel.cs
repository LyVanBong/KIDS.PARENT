using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Resources;
using Unity;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.User
{
    public class UserProfileViewModel : BaseViewModel
    {
        private bool _isLoading;
        private ParentModel _user;
        private IUserService _userService;
        private IPageDialogService _pageDialogService;
        private IDatabaseService _databaseService;
        public ICommand UpdateProfileCommand { get; private set; }
        public ParentModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public UserProfileViewModel(IUserService userService, IPageDialogService pageDialogService, IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _pageDialogService = pageDialogService;
            _userService = userService;
            UpdateProfileCommand = new Command(UpdateProfile);
        }

        private async void UpdateProfile()
        {
            if (IsLoading)
                return;
            IsLoading = true;
            var data = await _userService.UpdateInfoUser(User);
            if (data != null && data.Code > 0)
                await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00062, "OK");
            else
                await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00063, "OK");
            IsLoading = false;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await InitialUserProfile();
        }
        public async Task InitialUserProfile()
        {
            try
            {
                IsLoading = true;
                var user = await _databaseService.GetAccount();
                var data = await _userService.GetParentOfStudent(user.StudentID);
                if (data != null)
                {
                    User = data.Data.FirstOrDefault();
                }
                else
                    User = new ParentModel();
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
