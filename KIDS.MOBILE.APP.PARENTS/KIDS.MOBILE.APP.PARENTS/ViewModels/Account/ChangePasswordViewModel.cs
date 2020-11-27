using KIDS.MOBILE.APP.PARENTS.Helpers;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Account
{
    class ChangePasswordViewModel : BaseViewModel
    {
        private IPageDialogService _pageDialogService;
        private ILoginService _loginService;
        private IUserService _userService;
        private IDatabaseService _databaseService;
        private INavigationService _navigationService;
        private UserModel User;
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RetypeNewPass { get; set; }
        public ICommand UpdatePassCommand { get; private set; }
        public ICommand GoBackCommand { get; set; }
        public ChangePasswordViewModel(ILoginService loginService, IUserService userService, IPageDialogService pageDialogService, IDatabaseService databaseService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _pageDialogService = pageDialogService;
            _userService = userService;
            _loginService = loginService;
            UpdatePassCommand = new Command(UpdateUser);
            GoBackCommand = new Command(GoBack);
        }
        private async void UpdateUser()
        {
            try
            {
                IsLoading = true;
                User = await _databaseService.GetAccount();
                if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(RetypeNewPass))
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00018, "OK");
                    return;
                }
                if (NewPassword.Length < 8)
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00022, "OK");

                    return;
                }
                if (string.Compare(NewPassword, RetypeNewPass, true) != 0)
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00019, "OK");
                    return;
                }
                var pass = HashFunctionHelper.GetHashCode(OldPassword, 1);
                if (User.Password == pass)
                {
                    var checkData = await _loginService.LoginAppByUserPwd(User.NickName, pass);
                    if (checkData != null)
                        if (checkData.Code != 0)
                        {
                            await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00021, "OK");
                            return;
                        }
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00020, "OK");
                    return;
                }
                pass = HashFunctionHelper.GetHashCode(NewPassword, 1);
                var data = await _userService.UpdateUser(User.NickName, pass);
                if (data.Code != 30)
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00021, "OK");
                    return;
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00023, "OK");
                    OldPassword = "";
                    NewPassword = "";
                    RetypeNewPass = "";
                    return;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
        private void GoBack()
        {
            _navigationService.GoBackAsync();
        }
    }
}
