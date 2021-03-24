using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            set => SetProperty(ref _healthList, value);
        }
        #endregion

        #region Constructor
        public HealthChartViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            HealthList = new ObservableCollection<ChartModel>(GetHealthList());
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private List<ChartModel> GetHealthList()
        {
            return new List<ChartModel> {
                new ChartModel("Jan", 50),
                new ChartModel("Feb", 70),
                new ChartModel("Mar", 65),
                new ChartModel("Apr", 57),
                new ChartModel("May", 48),
            };
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

