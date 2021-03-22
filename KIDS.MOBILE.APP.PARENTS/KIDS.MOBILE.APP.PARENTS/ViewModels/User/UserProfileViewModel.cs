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
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.User
{
    public class UserProfileViewModel : BaseViewModel
    {
        private ParentModel _user;
        private IUserService _userService;
        private IPageDialogService _pageDialogService;
        private IDatabaseService _databaseService;
        public ICommand UpdateProfileCommand { get; private set; }
        public DelegateCommand ChangeImageCommand { get; }
        public ParentModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        private ImageSource _ProfilePicture;
        public ImageSource ProfilePicture
        {
            get => _ProfilePicture;
            set => SetProperty(ref _ProfilePicture, value);
        }

        public UserProfileViewModel(INavigationService navigationService, IUserService userService, IPageDialogService pageDialogService, IDatabaseService databaseService) : base(navigationService)
        {
            _databaseService = databaseService;
            _pageDialogService = pageDialogService;
            _userService = userService;
            UpdateProfileCommand = new Command(UpdateProfile);
            ChangeImageCommand = new DelegateCommand(async () => await ChangeProfilePicture());
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
                ProfilePicture = ImageSource.FromUri(new Uri(User.TmpPicture));
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

        private async Task ChangeProfilePicture()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Not supported", "Your device does not currently support this functionality", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                return;
            }
            ProfilePicture = ImageSource.FromStream(() => selectedImageFile.GetStream());
            var tempUrl = User.Picture;
            var file = new Dictionary<string, string>();
            file.Add(selectedImageFile.Path, selectedImageFile.Path);
            var result = await _userService.UpdateInfoUser(User,file);
            if (result == null || result.Data < 0)
            {
                await _pageDialogService.DisplayAlertAsync(Resource._00002, Resource._00063, "OK");
                User.Picture = tempUrl;
                ProfilePicture = ImageSource.FromUri(new Uri(User.TmpPicture));
            }
        }
    }
}
