using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Database;
using KIDS.MOBILE.APP.PARENTS.Services.User;
using KIDS.MOBILE.APP.PARENTS.Views.Account;
using KIDS.MOBILE.APP.PARENTS.Views.User;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Account
{
    public class AccountViewModel : BaseViewModel, IActiveAware
    {
        private IUserService _userService;
        private IDatabaseService _databaseService;
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;
        private StudentModel _student;
        private bool _isActive;
        private bool _isLoading;
        private bool _isGoToProfile;
        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsGoToProfile
        {
            get => _isGoToProfile;
            set => SetProperty(ref _isGoToProfile, value);
        }
        public StudentModel Student
        {
            get => _student;
            set => SetProperty(ref _student, value);
        }
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, IsActiveChange);
        }
        public event EventHandler IsActiveChanged;
        public ICommand LogoutCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public AccountViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDatabaseService databaseService, IUserService userService)
        {
            _userService = userService;
            _databaseService = databaseService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            LogoutCommand = new Command(async () => await logout());
            ProfileCommand = new Command(async () => await Profile());
            ChangePasswordCommand = new Command(async () => await ChangePassword());
        }
        private async Task ChangePassword()
        {
            await _navigationService.NavigateAsync(nameof(ChangePasswordPage));
        }
        private async Task logout()
        {
            var answer = await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00012, Resource._00011, Resource._00010);
            if (answer)
            {
                await _databaseService.DeleteAccount();
                AppConstants.User = null;
                if (Preferences.ContainsKey(AppConstants.SaveAccount))
                    Preferences.Remove(AppConstants.SaveAccount);
                await _navigationService.NavigateAsync("/LoginPage");
            }
        }
        private async Task Profile()
        {
            if (_isGoToProfile)
                return;
            _isGoToProfile = true;
            await _navigationService.NavigateAsync(nameof(UserProfilePage));
            _isGoToProfile = false;
        }
        private void IsActiveChange()
        {
            if (IsActive)
            {
                InitializationAccount();
            }
            else
            {
            }
        }
        async void InitializationAccount()
        {
            IsLoading = true;
            try
            {
                User = AppConstants.User;
                var student = await _userService.GetStudent(User.StudentID);
                if (student != null)
                {
                    if (student.Code > 0)
                    {
                        var data = new ObservableCollection<StudentModel>(student.Data);
                        Student = data.ElementAt(0);
                    }
                    else
                    {
                        Student = new StudentModel();
                    }
                }
                else
                    Student = new StudentModel();
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
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
    }
}