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
        private IMedicineAdviseService _prescriptionService;
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

        private bool _IsDeleteVisible;
        public bool IsDeleteVisible
        {
            get => _IsDeleteVisible;
            set => SetProperty(ref _IsDeleteVisible, value);
        }

        private List<MedicineModel> medicineList;
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand GalleryCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        private bool isUpdate;
        private MessageModel CurrentMessage { get; set; }
        private MedicineTicketModel medicineDetail;
        private List<MedicineModel> listMedicine;
        private string defaultPath = null;
        private Guid ID = Guid.Empty;
        #endregion

        #region Contructor
        public CreateMedicineAdviseViewModel(INavigationService navigationService, IMedicineAdviseService prescriptionService) : base(navigationService)
            {
            _navigationService = navigationService;
            _prescriptionService = prescriptionService;
            SendCommand = new DelegateCommand(OnSendClick);
            GalleryCommand = new DelegateCommand(OnGalleryClick);
            MedicineList = new ObservableCollection<MedicineModel>();
            DeleteCommand = new DelegateCommand(OnDelete);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                IsDeleteVisible = false;
                ChooseImage1 = ImageSource.FromFile("add_image.png");
                SelectedDate = DateTime.Now;
                SelectedFromDate = DateTime.Now;
                SelectedToDate = DateTime.Now;
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

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                isUpdate = (bool?)parameters["isUpdate"] ?? false;

                if(parameters.ContainsKey("Message")) CurrentMessage = parameters["Message"] as MessageModel;
                if (isUpdate)
                {
                    if (parameters.ContainsKey("Id"))
                    {
                        Guid id = (Guid?)parameters["Id"] ?? Guid.Empty;
                        ID = id;
                        IsDeleteVisible = true;
                        medicineDetail = await _prescriptionService.GetMedicineAdviseDetail(id);
                        MessageContent = medicineDetail?.Content;
                        SelectedDate = medicineDetail?.Date != null ? DateTime.Parse(medicineDetail.Date) : DateTime.Now;
                        SelectedFromDate = medicineDetail?.FromDate != null ? DateTime.Parse(medicineDetail.FromDate) : DateTime.Now;
                        SelectedToDate = medicineDetail?.ToDate != null ? DateTime.Parse(medicineDetail.ToDate) : DateTime.Now;
                        if (medicineDetail.MedicineList?.Any() == true)
                        {
                            foreach (var item in medicineDetail.MedicineList)
                            {
                                medicineList.Add(new MedicineModel
                                {
                                    Id = item.Id ?? Guid.Empty,
                                    Image = new Uri($"{AppConstants.UriBaseWebForm}/{item.Picture}"),
                                    MessageContent = item.Note,
                                    Unit = item.Unit,
                                    Name = item.Name,
                                    Action = 1,
                                    FilePath = item.Picture
                                });
                            }
                            listMedicine = medicineList;
                            MedicineList = new ObservableCollection<MedicineModel>(medicineList);
                        }
                    }
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
                var detailList = new List<MedicineDetailTicketModel>();
                Dictionary<string, string> fileList = null;
                if (MedicineList?.Any() == true)
                {
                    fileList = new Dictionary<string, string>();
                    foreach (var item in MedicineList)
                    {
                        detailList.Add(new MedicineDetailTicketModel
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Name,
                            Unit = item.Unit,
                            Note = item.MessageContent,
                            Action = 1
                        });
                        fileList.Add(item.FilePath, item.FilePath);
                    }
                }
                var model = new MedicineTicketModel
                {
                    ClassID = Guid.Parse(AppConstants.User.ClassID),
                    Content = MessageContent,
                    StudentID = Guid.Parse(AppConstants.User.StudentID),
                    Date = DateTime.Now.ToString("yyyy/MM/dd"),
                    FromDate = SelectedFromDate.ToString("yyyy/MM/dd"),
                    ToDate = SelectedToDate.ToString("yyyy/MM/dd"),
                    MedicineList = detailList,
                    Id = Guid.NewGuid()
                };
                               
                var result = await _prescriptionService.CreatePrescription(model, fileList);
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
                medicineDetail.FromDate = SelectedFromDate.ToString("yyyy/MM/dd");
                medicineDetail.ToDate = SelectedToDate.ToString("yyyy/MM/dd");
                medicineDetail.Content = MessageContent;
                medicineDetail.MedicineList = medicineDetail.MedicineList ?? new List<MedicineDetailTicketModel>();
                var fileList = new Dictionary<string, string>();
                if (MedicineList?.Any() == true)
                {   
                    foreach (var item in MedicineList)
                    {
                        medicineDetail.MedicineList.Add(new MedicineDetailTicketModel
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Name,
                            Unit = item.Unit,
                            Note = item.MessageContent,
                            Action = item.Action
                        });
                        if (item.Action == 0)
                        {
                            fileList.Add(item.FilePath, item.FilePath);
                        }
                    }
                }
                var result = await _prescriptionService.UpdatePrescription(medicineDetail, fileList);
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
            
            MedicineList.Add(new MedicineModel
            {
                Id = Guid.NewGuid(),
                Image = ImageSource.FromStream(() => selectedImageFile.GetStream()),
                Name = string.Empty,
                Unit = string.Empty,
                MessageContent = string.Empty,
                Action = 0,
                FilePath = selectedImageFile.Path
            });
            defaultPath = selectedImageFile.Path;
            //MedicineList = new ObservableCollection<MedicineModel>(medicineList);
        }

        public void OnDeleteClick(MedicineModel data)
        {
            var item = MedicineList.FirstOrDefault(x => x.Id == data.Id);
            if(item != null)
            {
                item.Action = 2;
            }
            MedicineList.Remove(data);
        }

        private async void OnDelete()
        {
            try
            {
                IsLoading = true;
                var result = await _prescriptionService.DeletePrescription(new PrescriptionModel { ID = this.ID });
                if(result?.Data > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Thành công!", "Xoá thành công", "OK");
                    await _navigationService.GoBackAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Lỗi!", "Xoá không thành công. Vui lòng thử laị hoặc liên hệ với admin.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi!", "Xoá không thành công. Vui lòng thử laị hoặc liên hệ với admin.", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }

    public class MedicineModel
    {
        public Guid Id { get; set; }
        public ImageSource Image { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string MessageContent { get; set; }
        public int Action { get; set; }
        public string FilePath { get; set; }
    }
}

