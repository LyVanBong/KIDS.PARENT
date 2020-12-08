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
using KIDS.MOBILE.APP.PARENTS.Services.News;
using KIDS.MOBILE.APP.PARENTS.Services.Tuition;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Message;
using KIDS.MOBILE.APP.PARENTS.Services.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Views.Activity;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Activity;

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
            containerRegistry.Register<INewService, NewService>();
            containerRegistry.Register<ITuitionService, TuitionService>();
            containerRegistry.Register<IMessageService, MessageService>();
            containerRegistry.Register<ILeaveRequestService, LeaveRequestService>();

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
