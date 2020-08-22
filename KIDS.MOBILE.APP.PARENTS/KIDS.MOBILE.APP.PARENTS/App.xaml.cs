﻿using KIDS.MOBILE.APP.PARENTS.ViewModels.Account;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Home;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Login;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Main;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Notification;
using KIDS.MOBILE.APP.PARENTS.Views.Account;
using KIDS.MOBILE.APP.PARENTS.Views.Home;
using KIDS.MOBILE.APP.PARENTS.Views.Login;
using KIDS.MOBILE.APP.PARENTS.Views.Main;
using KIDS.MOBILE.APP.PARENTS.Views.Notification;
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
            #region Registry Page - ViewModel

            containerRegistry.RegisterForNavigation<MainPage,MainViewModel>();
            containerRegistry.RegisterForNavigation<NotificationPage,NotificationViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage,AccountViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomePage,HomeViewModel>();

            #endregion
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(nameof(LoginPage));
        }
        protected override void OnStart()
        {
            AppCenter.Start("android=69882c41-844f-4e47-ad19-80aa4647b134;" +
                            "ios=787d2acd-76d9-4df0-8be7-461d028c77f2;}",
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
