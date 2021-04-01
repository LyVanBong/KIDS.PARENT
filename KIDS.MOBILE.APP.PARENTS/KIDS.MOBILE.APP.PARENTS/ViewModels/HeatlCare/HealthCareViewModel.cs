using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Services.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Services.Popup;
using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare
{
    public class HealthCareViewModel : BaseViewModel
    {
        #region Properties
        
        private ObservableCollection<HealthInformationModel> _healthList;
        public ObservableCollection<HealthInformationModel> HealthList
        {
            get => _healthList;
            set => SetProperty(ref _healthList, value);
        }

        private string _Weight;
        public string Weight
        {
            get => _Weight;
            set => SetProperty(ref _Weight, value);
        }

        private string _Month;
        public string Month
        {
            get => _Month;
            set => SetProperty(ref _Month, value);
        }

        private string _Height;
        public string Height
        {
            get => _Height;
            set => SetProperty(ref _Height, value);
        }

        private IHealthCareService _healthService;
        private IInputAlertDialogService _popup;
        private List<GetStudentHealthModel> dataList = new List<GetStudentHealthModel>();
        public DelegateCommand<string> HeathChartCommand { get; }
        public DelegateCommand AddCommand { get; }
        #endregion

        #region Constructor
        public HealthCareViewModel(INavigationService navigationService,
            IHealthCareService healthCareService,
            IInputAlertDialogService popup) : base(navigationService)
        {
            _navigationService = navigationService;
            _healthService = healthCareService;
            _popup = popup;
            HeathChartCommand = new DelegateCommand<string>(OnHealthChartClicked);
            AddCommand = new DelegateCommand(OnAddClicked);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                HealthList = new ObservableCollection<HealthInformationModel>(GetHealthList());
                await GetHealthInformation();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Public methods
        public void OnDisappering()
        {
            _popup.ClosePopup();
        }
        #endregion

        #region Private methods
        private List<HealthInformationModel> GetHealthList()
        {
            return new List<HealthInformationModel>
            {
                new HealthInformationModel
                {
                    BackgroundGradientStart="#cf86ff",
                    Name = "Chiều cao",
                    Information = "130 cm"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#8691ff",
                    Name = "Cân nặng",
                    Information = "30 kg"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#ff9686",
                    Name = "Bệnh về mắt",
                    Information = "Không"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#cf86ff",
                    Name = "Dị ứng",
                    Information = "Không"
                },
                new HealthInformationModel
                {
                    BackgroundGradientStart="#8691ff",
                    Name = "Bệnh về tim",
                    Information = "Không"
                },
            };
        }

        private async void OnHealthChartClicked(string type)
        {
            var param = new NavigationParameters();
            dataList = dataList?.OrderByDescending(x => x.Date).Take(5).ToList();
            param.Add("Type", type);
            param.Add("Information", dataList);
            await _navigationService.NavigateAsync(nameof(HealthChartPage), param);
        }

        private async Task GetHealthInformation()
        {
            try
            {
                IsLoading = false;
                var studentId = AppConstants.User.StudentID;
                var data = await _healthService.GetStudentHealthInfo(studentId);
                dataList = data?.Data ?? new List<GetStudentHealthModel>();
                var info = data?.Data?.FirstOrDefault();
                Weight = $"{info?.Width} KG";
                Height = $"{info?.Height} CM";
                Month = $"{info?.MonthAge}";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnAddClicked()
        {
            try
            {
                var result = await _popup.OpenMultipleDataInputAlertDialog(string.Empty, string.Empty, string.Empty);
                if(result != null)
                {
                    if(string.IsNullOrEmpty(result.Value1) || string.IsNullOrEmpty(result.Value2) || string.IsNullOrEmpty(result.Value3))
                    {
                        await Application.Current.MainPage.DisplayAlert("Lỗi", "Hãy nhập đủ thông tin cho bé", "OK");
                    }
                    else
                    {
                        var model = new CreateHealthInformationModel
                        {
                            ID = Guid.NewGuid(),
                            StudentID = AppConstants.User.StudentID,
                            ClassID = AppConstants.User.ClassID,
                            Date = DateTime.Now,
                            MonthAge = double.Parse(result.Value1),
                            Width = double.Parse(result.Value2),
                            Height = double.Parse(result.Value3)
                        };
                        var response = await _healthService.CreateHealthInformation(model);
                        if(response?.Data == 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Thành công", "Nhập thông tin cho bé thành công", "OK");
                            await GetHealthInformation();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Lỗi", "Lỗi phát sinh. Vui lòng thử lại hoặc liên hệ với admin.", "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Hãy nhập lại thông tin cho bé ( chỉ nhập số )", "OK");
            }
        }
        #endregion
    }

    public class HealthInformationModel
    {
        public string BackgroundGradientStart { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }
}

