using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Message;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Message
{
    public class CreateMessageViewModel : BaseViewModel
    {
        #region Properties
        private IMessageService _messageService;
        private ImageSource chooseImage;
        public ImageSource ChooseImage
        {
            get => chooseImage;
            set => SetProperty(ref chooseImage, value);
        }
        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set => SetProperty(ref messageContent, value);
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand GalleryCommand { get; set; }
        private bool isUpdate;
        private MessageModel CurrentMessage { get; set; }
        #endregion

        #region Contructor
        public CreateMessageViewModel(INavigationService navigationService, IMessageService messageService) : base(navigationService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            SendCommand = new DelegateCommand(OnSendClick);
            GalleryCommand = new DelegateCommand(OnGalleryClick);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                ChooseImage = ImageSource.FromFile("add_image.png");
                SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                isUpdate = (bool?)parameters["isUpdate"] ?? false;
                CurrentMessage = parameters["Message"] as MessageModel;
                if (isUpdate)
                {
                    MessageContent = CurrentMessage.Comment;
                    SelectedDate = CurrentMessage.DateTime != null ? DateTime.Parse(CurrentMessage.DateTime) : DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private async void OnSendClick()
        {
            if (string.IsNullOrEmpty(MessageContent)) 
            { 
                await App.Current.MainPage.DisplayAlert(Resource._00063, Resource._00096, Resource._00011); 
            }
            else
            {
                if (!isUpdate)
                {
                    var model = new CreateMessageModel
                    {
                        ClassID = AppConstants.User.ClassID,
                        TeacherID = string.Empty, //TODO
                        Parent = null,
                        Content = MessageContent,
                        DateCreate = SelectedDate,
                        StudentID = AppConstants.User.StudentID,
                        Type = 2
                    };
                    var result = await _messageService.CreateMessage(model);
                    if (result?.Data == 1)
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00097, string.Empty, Resource._00011);
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00063, string.Empty, Resource._00011);
                    }
                }
                else
                {
                    var model = new CreateMessageModel
                    {
                        CommunicationID = CurrentMessage.Id.ToString(),
                        Content = MessageContent,
                        DateCreate = SelectedDate,
                        StudentID = AppConstants.User.StudentID,
                        Type = 2
                    };
                    var result = await _messageService.UpdateMessage(model);
                    if (result?.Data == 1)
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00097, string.Empty, Resource._00011);
                        await _navigationService.GoBackAsync();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Resource._00063, string.Empty, Resource._00011);
                    }
                }
            }
        }

        private async void OnGalleryClick()
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

            ChooseImage = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }
        #endregion
    }
}
