using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Services.HealthCare;
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
        private List<GetStudentHealthModel> dataList = new List<GetStudentHealthModel>();
        #endregion

        #region Constructor
        public HealthCareViewModel(INavigationService navigationService, IHealthCareService healthCareService) : base(navigationService)
        {
            _navigationService = navigationService;
            _healthService = healthCareService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                HealthList = new ObservableCollection<HealthInformationModel>(GetHealthList());
                IsLoading = false;
                var studentId = AppConstants.User.StudentID;
                var data = await _healthService.GetStudentHealthInfo(studentId);
                dataList = data?.Data ?? new List<GetStudentHealthModel>();
                var info = data?.Data?.FirstOrDefault();
                Weight = $"{info?.Width} KG";
                Height = $"{info?.Height} CM";
                Month = $"{info?.MonthAge}";
                IsLoading = false;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Public methods
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
        #endregion
    }

    public class HealthInformationModel
    {
        public string BackgroundGradientStart { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }
}

