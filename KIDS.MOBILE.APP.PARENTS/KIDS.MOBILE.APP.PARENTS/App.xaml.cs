using System.Collections.Generic;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using KIDS.MOBILE.APP.PARENTS.ViewModels;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Account;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Home;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Login;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Main;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Notification;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Tuition;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Survey;
using KIDS.MOBILE.APP.PARENTS.ViewModels.User;
using KIDS.MOBILE.APP.PARENTS.Views;
using KIDS.MOBILE.APP.PARENTS.Views.Account;
using KIDS.MOBILE.APP.PARENTS.Views.Home;
using KIDS.MOBILE.APP.PARENTS.Views.Login;
using KIDS.MOBILE.APP.PARENTS.Views.Main;
using KIDS.MOBILE.APP.PARENTS.Views.Notification;
using KIDS.MOBILE.APP.PARENTS.Views.Tuition;
using KIDS.MOBILE.APP.PARENTS.Views.User;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using KIDS.MOBILE.APP.PARENTS.Views.Message;
using KIDS.MOBILE.APP.PARENTS.Views.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.ViewModels.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;
using KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare;

namespace KIDS.MOBILE.APP.PARENTS
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer)
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

            containerRegistry.RegisterForNavigation<StudentProfilePage, StudentProfileViewModel>();
            containerRegistry.RegisterForNavigation<UserProfilePage, UserProfileViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<NotificationPage, NotificationViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage, AccountViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<NewsPage, NewsViewModel>();
            containerRegistry.RegisterForNavigation<TuitionPage, TuitionViewModel>();
            containerRegistry.RegisterForNavigation<SurveyPage, SurveyViewModel>();
            containerRegistry.RegisterForNavigation<MessagePage, MessageViewModel>();
            containerRegistry.RegisterForNavigation<LeaveRequestPage, LeaveRequestViewModel>();
            containerRegistry.RegisterForNavigation<HealthCarePage, HealthCareViewModel>();

            #endregion
            #region Registry Dialog
            #endregion
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppSettings.SyncfusionLicense);
            AppCenter.Start(AppCenterConstants.AppSecretAndroid +
                            AppCenterConstants.AppSecretiOS,
                typeof(Analytics), typeof(Crashes));
            NavigationService.NavigateAsync(nameof(LoginPage));
            InitializedOneSignal();
        }

        private static void InitializedOneSignal()
        {
            //Remove this method to stop OneSignalApp Debugging  
            OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);

            OneSignal.Current.StartInit(OneSignalApp.OneSignalAppId)
                .Settings(new Dictionary<string, bool>()
                {
                    {IOSSettings.kOSSettingsKeyAutoPrompt, false},
                    {IOSSettings.kOSSettingsKeyInAppLaunchURL, false}
                })
                .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
