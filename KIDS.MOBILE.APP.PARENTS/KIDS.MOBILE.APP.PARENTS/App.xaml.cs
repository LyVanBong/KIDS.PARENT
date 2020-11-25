﻿using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Account;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Home;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Login;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Main;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Notification;
using KIDS.MOBILE.APP.PARENTS.ViewModels.User;
using KIDS.MOBILE.APP.PARENTS.Views.Account;
using KIDS.MOBILE.APP.PARENTS.Views.Home;
using KIDS.MOBILE.APP.PARENTS.Views.Login;
using KIDS.MOBILE.APP.PARENTS.Views.Main;
using KIDS.MOBILE.APP.PARENTS.Views.Notification;
using KIDS.MOBILE.APP.PARENTS.Views.User;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Unity;

namespace KIDS.MOBILE.APP.PARENTS
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #region Registry Service

            containerRegistry.Register<INotificationService, NotificationService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IDatabaseService, DatabaseService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IRequestProvider, RequestProvider>();

            #endregion
            #region Registry Page - ViewModel

            containerRegistry.RegisterForNavigation<StudentProfilePage,StudentProfileViewModel>();
            containerRegistry.RegisterForNavigation<UserProfilePage, UserProfileViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<NotificationPage, NotificationViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage, AccountViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();

            #endregion
            #region Registry Dialog
            #endregion
        }

        protected override void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppSettings.SyncfusionLicense);
            InitializeComponent();
            NavigationService.NavigateAsync(nameof(LoginPage));
        }
        protected override void OnStart()
        {
            AppCenter.Start(AppCenterConstants.AppSecretAndroid +
                            AppCenterConstants.AppSecretiOS,
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
