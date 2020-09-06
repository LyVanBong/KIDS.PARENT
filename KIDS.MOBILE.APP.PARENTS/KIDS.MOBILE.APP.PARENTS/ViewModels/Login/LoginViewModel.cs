using System;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using KIDS.MOBILE.APP.PARENTS.Views.Home;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using KIDS.MOBILE.APP.PARENTS.Helpers;
using Microsoft.AppCenter.Crashes;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using Xamarin.Essentials;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using Prism.Services;
using KIDS.MOBILE.APP.PARENTS.Resources;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private IPageDialogService _pageDialogService;
        private ILoginService _loginService;
        private INavigationService _navigationService;
        private bool _isSaveAccount;
        private bool _isCheckLogin;
        public ICommand LoginCommand { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSaveAccount
        {
            get => _isSaveAccount;
            set => SetProperty(ref _isSaveAccount, value);
        }

        private IDatabaseService _databaseService;
        public LoginViewModel(INavigationService navigationService, ILoginService loginService, IDatabaseService databaseService,IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
            _loginService = loginService;
            _databaseService = databaseService;
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(Login);
        }
        private bool isOnline()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            return false;
        }
        private async void Login()
        {
            if (!isOnline())
            {
                await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00009, "OK");
                return;
            }
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    var pass = _isCheckLogin ? Password : HashFunctionHelper.GetHashCode(Password, 1);
                    var data = await _loginService.LoginAppByUserPwd(UserName, pass);
                    if (data != null)
                    {
                        if (data.Code == 0)
                        {
                            if (data.Data.IsTeacher.Equals("2"))
                            {
                                await CheckSaveAccount(data.Data);
                                await _navigationService.NavigateAsync("/MainPage?selected=HomePage");
                            }
                            else
                                await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00007, "OK");
                        }
                        else
                        {
                            await _pageDialogService.DisplayAlertAsync(Resource._00002,Resource._00007,"OK");
                        }
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00007, "OK");
                    }
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00008, "OK");
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {

            }
        }
        /// <summary>
        /// Lưu tài khoản đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task CheckSaveAccount(UserModel user)
        {
            try
            {
                if(IsSaveAccount && user != null)
                {
                    Preferences.Set(AppConstants.SaveAccount, true);
                    await _databaseService.UpdateAccount(user);
                }
                if(IsSaveAccount)
                {
                    if (Preferences.ContainsKey(AppConstants.SaveAccount))
                        Preferences.Remove(AppConstants.SaveAccount);
                    await _databaseService.DeleteAccount();
                }
            }
            catch(Exception e)
            {
                Crashes.TrackError(e);
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsSaveAccount = true;
            CheckLogin();
        }
        /// <summary>
        /// Login
        /// </summary>
        private async void CheckLogin()
        {
            if(Preferences.ContainsKey(AppConstants.SaveAccount))
                if (Preferences.Get(AppConstants.SaveAccount, false))
                {
                    _isCheckLogin = true;
                    var user = await _databaseService.GetAccount();
                    UserName = user.NickName;
                    Password = user.Password;
                    await Task.Delay(TimeSpan.FromMilliseconds(1500));
                    Login();
                }
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }
    }
}