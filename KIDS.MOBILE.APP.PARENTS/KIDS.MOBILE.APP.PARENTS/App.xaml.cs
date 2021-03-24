using System.Collections.Generic;
using System.Diagnostics;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.Login;
using KIDS.MOBILE.APP.PARENTS.Services.Notification;
using KIDS.MOBILE.APP.PARENTS.Services.PushNotification;
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
using KIDS.MOBILE.APP.PARENTS.Services.News;
using KIDS.MOBILE.APP.PARENTS.Services.Tuition;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Message;
using KIDS.MOBILE.APP.PARENTS.Services.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Views.Activity;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Activity;
using KIDS.MOBILE.APP.PARENTS.Services.Activity;
using KIDS.MOBILE.APP.PARENTS.Services.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Views.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.ViewModels.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Views.Pickup;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Pickup;
using KIDS.MOBILE.APP.PARENTS.Views.Album;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Album;
using KIDS.MOBILE.APP.PARENTS.Services.Album;
using KIDS.MOBILE.APP.PARENTS.Services.HealthCare;

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

            containerRegistry.Register<IPushNotificationService, PushNotificationService>();
            containerRegistry.Register<INotificationService, NotificationService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IDatabaseService, DatabaseService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IRequestProvider, RequestProvider>();
            containerRegistry.Register<INewService, NewService>();
            containerRegistry.Register<ITuitionService, TuitionService>();
            containerRegistry.Register<IMessageService, MessageService>();
            containerRegistry.Register<ILeaveRequestService, LeaveRequestService>();
            containerRegistry.Register<IActivityService, ActivityService>();
            containerRegistry.Register<IMedicineAdviseService, MedicineAdviseService>();
            containerRegistry.Register<IAlbumService, AlbumService>();
            containerRegistry.Register<IImageService>();
            containerRegistry.Register<IHealthCareService, HealthCareService>();
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
            containerRegistry.RegisterForNavigation<CreateMessagePage, CreateMessageViewModel>();
            containerRegistry.RegisterForNavigation<CreateLeaveRequestPage, CreateLeaveRequestViewModel>();
            containerRegistry.RegisterForNavigation<ActivityPage, ActivityViewModel>();
            containerRegistry.RegisterForNavigation<MedicineAdvisePage, MedicineAdviseViewModel>();
            containerRegistry.RegisterForNavigation<CreteMedicineAdvisePage, CreateMedicineAdviseViewModel>();
            containerRegistry.RegisterForNavigation<PickupPage, PickupViewModel>();
            containerRegistry.RegisterForNavigation<AlbumDetailpage, AlbumDetailViewModel>();
            containerRegistry.RegisterForNavigation<MessageDetailPage, MessageDetailViewModel>();
            containerRegistry.RegisterForNavigation<AlbumPage, AlbumViewModel>();
            containerRegistry.RegisterForNavigation<ImageDetailPage, ImageDetailViewModel>();
            containerRegistry.RegisterForNavigation<SleepActivityPage, SleepActivityViewModel>();
            containerRegistry.RegisterForNavigation<TuitionFeeDetailPage, TuitionFeeDetailViewModel>();
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
            PARENTS.Resources.Resource.Culture = new System.Globalization.CultureInfo("vi");
            NavigationService.NavigateAsync(nameof(LoginPage));
            InitializedOneSignal();
        }

        private void InitializedOneSignal()
        {
            //Remove this method to stop OneSignalApp Debugging  
            OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);

            OneSignal.Current.StartInit(OneSignalApp.OneSignalAppId)
                .Settings(new Dictionary<string, bool>()
                {
                    {IOSSettings.kOSSettingsKeyAutoPrompt, false},
                    {IOSSettings.kOSSettingsKeyInAppLaunchURL, false}
                })
                .InFocusDisplaying(OSInFocusDisplayOption.Notification).HandleNotificationReceived(HandleNotificationReceived).HandleNotificationOpened(HandleNotificationOpened)
                .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();

            // lay id cua thiet bi
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }

        private void IdsAvailable(string userID, string pushToken)
        {
            Xamarin.Essentials.Preferences.Set(AppConstants.PlayerId, userID);
        }

        /// <summary>
        /// khi ung dung dang chay ma co thong bao day ve se vao day
        /// Called when your app is in focus and a notificaiton is recieved.
        /// The name of the method can be anything as long as the signature matches.
        /// Method must be static or this object should be marked as DontDestroyOnLoad
        /// </summary>
        /// <param name="notification"></param>
        private static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;

            Debug.WriteLine("GameControllerExample:HandleNotificationReceived: " + message);
            Debug.WriteLine("displayType: " + notification.displayType);
            var mess = "Notification received with text: " + message;
        }

        /// <summary>
        /// Khi nguoi dung click vao mot thong bao nao day se vao xu kien nay
        /// Called when a notification is opened.
        /// The name of the method can be anything as long as the signature matches.
        /// Method must be static or this object should be marked as DontDestroyOnLoad
        /// </summary>
        /// <param name="result"></param>
        private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;
            string message = payload.body;
            string actionID = result.action.actionID;

            Debug.WriteLine("GameControllerExample:HandleNotificationOpened: " + message);
            var extraMessage = "Notification opened with text: " + message;

            if (additionalData != null)
            {
                if (additionalData.ContainsKey("discount"))
                {
                    extraMessage = (string)additionalData["discount"];
                    // Take user to your store.
                }
            }
            if (actionID != null)
            {
                // actionSelected equals the id on the button the user pressed.
                // actionSelected will equal "__DEFAULT__" when the notification itself was tapped when buttons were present.
                extraMessage = "Pressed ButtonId: " + actionID;
            }
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
