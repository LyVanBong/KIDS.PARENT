using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using Prism.Commands;
using Prism.Navigation;
using KIDS.MOBILE.APP.PARENTS.Helpers;
using Microsoft.AppCenter.Crashes;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using Xamarin.Essentials;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using Prism.Services;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.PushNotification;
using Xamarin.Forms;
using System.Collections.Generic;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private IPageDialogService _pageDialogService;
        private ILoginService _loginService;
        
        private bool _isSaveAccount;
        private bool _isCheckLogin;
        private bool _isLoading;
        private IPushNotificationService _pushNotificationService;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public ICommand LoginCommand { get; private set; }
        public ICommand SaveAccountCommand { get; private set; }
        public ICommand ForgotPasswordCommand { get; private set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSaveAccount
        {
            get => _isSaveAccount;
            set => SetProperty(ref _isSaveAccount, value);
        }

        private IDatabaseService _databaseService;
        public LoginViewModel(INavigationService navigationService, ILoginService loginService, IDatabaseService databaseService, IPageDialogService pageDialogService, IPushNotificationService pushNotificationService) : base(navigationService)
        {
            _pushNotificationService = pushNotificationService;
            _pageDialogService = pageDialogService;
            _loginService = loginService;
            _databaseService = databaseService;
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(Login);
            ForgotPasswordCommand = new DelegateCommand(OnForgotPasswordClicked);
#if DEBUG
            UserName = "0984103587";
            Password = "123456";
#endif
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
                if (IsLoading)
                    return;
                IsLoading = true;
                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
                {
                    var pass = _isCheckLogin ? Password : HashFunctionHelper.GetHashCode(Password, 1);
                    var data = await _loginService.LoginAppByUserPwd(UserName, pass);
                    if (data != null)
                    {

                        if (data.Code == 0)
                        {
                            await CheckSaveAccount(data.Data);
                            var idDevice = Xamarin.Essentials.Preferences.Get(AppConstants.PlayerId, null);
                            Preferences.Set("UserName", UserName);
                            if (idDevice != null)
                            {
                                var userSend = data.Data;
                                if (userSend != null)
                                {
                                    var send = await _pushNotificationService.UpdateDeviceDd(userSend.DonVi, userSend.ClassID, userSend.NguoiSuDung, idDevice, DateTime.Now.ToString("F"));
                                    Debug.WriteLine($"Phan hoi tu thong bao day: {send}");
                                }
                            }

                            var banners = new List<string>();
                            if (!string.IsNullOrEmpty(data.Data?.Banner)) banners.Add(data.Data?.Banner);
                            if (!string.IsNullOrEmpty(data.Data?.Banner2)) banners.Add(data.Data?.Banner2);
                            if (!string.IsNullOrEmpty(data.Data?.Banner3)) banners.Add(data.Data?.Banner3);
                            var param = new NavigationParameters();
                            param.Add("banner", banners);
                            await _navigationService.NavigateAsync("/MainPage?selected=HomePage", param);
                        }
                        else if (data.Code == -1)
                        {
                            await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00007, "OK");
                        }
                        else
                            await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00017, "OK");
                    }
                    else
                        await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00017, "OK");
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
                IsLoading = false;
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
                if (IsSaveAccount && user != null)
                {
                    Preferences.Set(AppConstants.SaveAccount, true);
                    AppConstants.User = user;
                }
                else
                {
                    if (Preferences.ContainsKey(AppConstants.SaveAccount))
                        Preferences.Remove(AppConstants.SaveAccount);
                }
                await _databaseService.UpdateAccount(user);
            }
            catch (Exception e)
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

            if (Preferences.ContainsKey(AppConstants.SaveAccount))
            {
                if (Preferences.Get(AppConstants.SaveAccount, false))
                {
                    _isCheckLogin = true;
                    var user = await _databaseService.GetAccount();
                    UserName = user.NickName;
                    Password = user.Password;
                    Login();
                }
                else
                {
                    UserName = Preferences.Get("UserName", string.Empty);
                }
            }
            else
            {
                UserName = Preferences.Get("UserName", string.Empty);
            }
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private async void OnForgotPasswordClicked()
        {
            await _pageDialogService.DisplayAlertAsync("Quên mật khẩu ?", "Để lấy lại mật khẩu, vui lòng liên hệ với nhà trường hoặc admin HappyKids", "OK");
        }
    }
}