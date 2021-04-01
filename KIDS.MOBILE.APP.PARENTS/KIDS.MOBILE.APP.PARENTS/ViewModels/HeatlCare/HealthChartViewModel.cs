using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Services.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Services.Popup;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare
{
    public class HealthChartViewModel : BaseViewModel
    {
        #region Properties
        
        private ObservableCollection<ChartModel> _healthList;
        public ObservableCollection<ChartModel> HealthList
        {
            get => _healthList;
            set {
                _healthList = value;
                RaisePropertyChanged(nameof(HealthList));
            }
        }

        private ObservableCollection<Color> _ColorList;
        public ObservableCollection<Color> ColorList
        {
            get => _ColorList;
            set
            {
                _ColorList = value;
                RaisePropertyChanged(nameof(ColorList));
            }
        }

        private string _ChartTitle;
        public string ChartTitle
        {
            get => _ChartTitle;
            set => SetProperty(ref _ChartTitle, value);
        }

        private string _NormalIndexText;
        public string NormalIndexText
        {
            get => _NormalIndexText;
            set => SetProperty(ref _NormalIndexText, value);
        }

        private string _UnderIndexText;
        public string UnderIndexText
        {
            get => _UnderIndexText;
            set => SetProperty(ref _UnderIndexText, value);
        }

        private string _OverIndexText;
        public string OverIndexText
        {
            get => _OverIndexText;
            set => SetProperty(ref _OverIndexText, value);
        }

        private Color _WeightColor;
        public Color WeightColor
        {
            get => _WeightColor;
            set {
                _WeightColor = value;
                if(_WeightColor == Color.FromHex("#fa39b7"))
                {
                    HeightColor = Color.White;
                    BmiColor = Color.White;
                }
                RaisePropertyChanged(nameof(WeightColor));
            }
        }

        private Color _HeightColor;
        public Color HeightColor
        {
            get => _HeightColor;
            set
            {
                _HeightColor = value;
                if (_HeightColor == Color.FromHex("#fa39b7"))
                {
                    WeightColor = Color.White;
                    BmiColor = Color.White;
                }
                RaisePropertyChanged(nameof(HeightColor));
            }
        }

        private Color _MonthColor;
        public Color BmiColor
        {
            get => _MonthColor;
            set
            {
                _MonthColor = value;
                if (_MonthColor == Color.FromHex("#fa39b7"))
                {
                    HeightColor = Color.White;
                    WeightColor = Color.White;
                }
                RaisePropertyChanged(nameof(BmiColor));
            }
        }

        private double _MaximumValue;
        public double MaximumValue
        {
            get => _MaximumValue;
            set => SetProperty(ref _MaximumValue, value);
        }

        private string _Weight;
        public string Weight
        {
            get => _Weight;
            set => SetProperty(ref _Weight, value);
        }

        private string _Bmi;
        public string Bmi
        {
            get => _Bmi;
            set => SetProperty(ref _Bmi, value);
        }

        private string _Height;
        public string Height
        {
            get => _Height;
            set => SetProperty(ref _Height, value);
        }

        public DelegateCommand<string> HeathChartCommand { get; }

        private List<GetStudentHealthModel> dataList = new List<GetStudentHealthModel>();

        private ObservableCollection<GetStudentHealthModel> _History;
        public ObservableCollection<GetStudentHealthModel> History
        {
            get => _History;
            set => SetProperty(ref _History, value);
        }

        public DelegateCommand<object> DetailCommand { get; }
        private IInputAlertDialogService _popup;
        private IHealthCareService _healthService;
        #endregion

        #region Constructor
        public HealthChartViewModel(INavigationService navigationService, IInputAlertDialogService popup, IHealthCareService healthCareService) : base(navigationService)
        {
            _navigationService = navigationService;
            _popup = popup;
            _healthService = healthCareService;
            HeathChartCommand = new DelegateCommand<string>(OnHealthChartClicked);
            DetailCommand = new DelegateCommand<object>(OnDetailClick);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            HealthList = new ObservableCollection<ChartModel>();
            ColorList = new ObservableCollection<Color>();
            
            if (parameters?.ContainsKey("Information") == true)
            {
                dataList = (List<GetStudentHealthModel>)parameters["Information"] ?? new List<GetStudentHealthModel>();
                Weight = $"{dataList?.FirstOrDefault()?.Width} KG";
                Height = $"{dataList?.FirstOrDefault()?.Height} CM";
                Bmi = $"{dataList?.FirstOrDefault()?.BMI}";
                dataList = dataList.OrderBy(x => x.Date).ToList();
                History = new ObservableCollection<GetStudentHealthModel>(dataList);
            }

            if (parameters?.ContainsKey("Type") == true)
            {
                OnHealthChartClicked((string)parameters["Type"]);
            }
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private void OnHealthChartClicked(string type)
        {
            switch (type)
            {
                case "2":
                    if(HeightColor != Color.FromHex("#fa39b7"))
                    {
                        HealthList.Clear();
                        ColorList.Clear();
                        for(int i = 0; i < dataList.Count; i++)
                        {
                            HealthList.Add(new ChartModel($"Lần {i + 1}", dataList[i].Height ?? 0));
                            ColorList.Add(GetColor(dataList[i]?.BMI ?? 0));
                        }
                        MaximumValue = (dataList.Max(x => x.Height) ?? 0) + 20;
                    }
                    HeightColor = Color.FromHex("#fa39b7");
                    ChartTitle = "Chiều cao (đơn vị : cm)";
                    NormalIndexText = "Chiều cao bình thường";
                    OverIndexText = "Chiều cao CAO hơn so với tuổi";
                    UnderIndexText = "Chiều cao THẤP hơn so với tuổi";
                    break;
                case "3":
                    if (BmiColor != Color.FromHex("#fa39b7"))
                    {
                        HealthList.Clear();
                        ColorList.Clear();
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            HealthList.Add(new ChartModel($"Lần {i + 1}", dataList[i].BMI ?? 0));
                            ColorList.Add(GetColor(dataList[i]?.BMI ?? 0));
                        }
                        MaximumValue = (dataList.Max(x => x.BMI) ?? 0) + 2;
                    }
                    BmiColor = Color.FromHex("#fa39b7");
                    ChartTitle = "Chỉ số BMI";
                    NormalIndexText = "Chỉ số BMI bình thường";
                    OverIndexText = "Chỉ số BMI cao";
                    UnderIndexText = "Chỉ số BMI thấp";
                    break;
                default:
                    if (WeightColor != Color.FromHex("#fa39b7"))
                    {
                        HealthList.Clear();
                        ColorList.Clear();
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            HealthList.Add(new ChartModel($"Lần {i + 1}", dataList[i].Width ?? 0));
                            ColorList.Add(GetColor(dataList[i]?.BMI ?? 0));
                        }
                        MaximumValue = (dataList.Max(x => x.Width) ?? 0) + 10;
                    }
                    WeightColor = Color.FromHex("#fa39b7");
                    ChartTitle = "Cân nặng (đơn vị : kg)";
                    NormalIndexText = "Cân nặng bình thường";
                    OverIndexText = "Thừa cân, béo phì";
                    UnderIndexText = "Suy dinh dưỡng";
                    break;
            }
        }

        private Color GetColor(double bmiIndex)
        {
            if(bmiIndex < 18.5)
            {
                return Color.FromHex("#ffd04f");
            }
            else if (bmiIndex > 22.9)
            {
                return Color.FromHex("#ff3e3b");
            }
            else
            {
                return Color.FromHex("#0fb2f2");
            }
        }

        private async void OnDetailClick(object item)
        {
            try
            {
                var data = (Syncfusion.ListView.XForms.ItemTappedEventArgs)item;
                var info = (GetStudentHealthModel)data.ItemData;
                if (info != null)
                {
                    var result = await _popup.OpenMultipleDataInputAlertDialog(info.MonthAge?.ToString(), info.Height?.ToString(), info.Width?.ToString());
                    if (result != null)
                    {
                        if (string.IsNullOrEmpty(result.Value1) || string.IsNullOrEmpty(result.Value2) || string.IsNullOrEmpty(result.Value3))
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
                            var response = await _healthService.UpdateHealthInformation(model);
                            if (response?.Data == 1)
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
            }
            catch (Exception)
            {

            }
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
                Bmi = $"{info?.BMI}";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }

    public class ChartModel
    {
        public string Serial { get; set; }
        public double Index { get; set; }
        public ChartModel(string xValue, double yValue)
        {
            Serial = xValue;
            Index = yValue;
        }
    }
}

