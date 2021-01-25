using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.MedicineAdvise;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.MedicineAdvise
{
    public class CreateMedicineAdviseViewModel : BaseViewModel
    {
        #region Properties
        private IMedicineAdviseService _messageService;
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
        private DateTime selectedfromDate;
        public DateTime SelectedFromDate
        {
            get => selectedfromDate;
            set => SetProperty(ref selectedfromDate, value);
        }
        private DateTime selectedToDate;
        public DateTime SelectedToDate
        {
            get => selectedToDate;
            set => SetProperty(ref selectedToDate, value);
        }
        private ImageSource chooseImage1;
        public ImageSource ChooseImage1
        {
            get => chooseImage1;
            set => SetProperty(ref chooseImage1, value);
        }
        private ObservableCollection<MedicineModel> _MedicineList;
        public ObservableCollection<MedicineModel> MedicineList
        {
            get => _MedicineList;
            set => SetProperty(ref _MedicineList, value);
        }
        private List<MedicineModel> medicineList;
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand GalleryCommand { get; set; }
        private bool isUpdate;
        private MessageModel CurrentMessage { get; set; }
        private MedicineTicketModel medicineDetail;
        #endregion

        #region Contructor
        public CreateMedicineAdviseViewModel(INavigationService navigationService, IMedicineAdviseService messageService) : base(navigationService)
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
                ChooseImage1 = ImageSource.FromFile("add_image.png");
                SelectedDate = DateTime.Now;
                medicineList = new List<MedicineModel>();
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
                medicineDetail = new MedicineTicketModel();
                isUpdate = (bool?)parameters["isUpdate"] ?? false;
                CurrentMessage = parameters["Message"] as MessageModel;
                if (isUpdate)
                {
                    //MessageContent = medicineDetail.Content;
                    //SelectedDate = medicineDetail.Date;
                    //if (medicineDetail.MedicineList?.Any() == true)
                    //{
                    //    foreach(var item in medicineDetail.MedicineList)
                    //    {
                    //        medicineList.Add(new MedicineModel
                    //        {
                    //            Id = item.Id ?? Guid.Empty,
                    //            Image = $"{AppConstants.UriBaseWebForm}{item.Picture}",
                    //            MessageContent = item.Note
                    //        });
                    //    }
                    //}
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
            if (!isUpdate)
            {
                var model = new PrescriptionModel
                {
                    ClassID = AppConstants.User.ClassID,
                    Content = MessageContent,
                    StudentID = AppConstants.User.StudentID,
                    Date = DateTime.Now,
                    FromDate = SelectedFromDate,
                    ToDate = SelectedToDate

                };
                var result = await _messageService.CreateMessage(model);
                if (result.Data == 1)
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
                var model = new PrescriptionModel
                {
                    ID = CurrentMessage.Id,
                    ClassID = AppConstants.User.ClassID,
                    Content = MessageContent,
                    StudentID = AppConstants.User.StudentID,
                    Date = DateTime.Now,
                    FromDate = SelectedFromDate,
                    ToDate = SelectedToDate
                };
                var result = await _messageService.UpdateMessage(model);
                if (result.Data == 1)
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

            //ChooseImage1 = ImageSource.FromStream(() => selectedImageFile.GetStream());
            medicineList.Add(new MedicineModel
            {
                Id = Guid.Empty,
                Image = ImageSource.FromStream(() => selectedImageFile.GetStream()),
                MessageContent = string.Empty
            });
            MedicineList = new ObservableCollection<MedicineModel>(medicineList);
        }
        #endregion
    }

    public class MedicineModel
    {
        public Guid Id { get; set; }
        public ImageSource Image { get; set; }
        public string MessageContent { get; set; }
    }
}

