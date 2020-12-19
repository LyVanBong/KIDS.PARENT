using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Tuition
{
    public class TuitionViewModel : BaseViewModel
    {
        #region Properties
        private INavigationService _navigationService;
        private ObservableCollection<TuitionInformationModel> _informationList = new ObservableCollection<TuitionInformationModel>();
        public ObservableCollection<TuitionInformationModel> InformationList
        {
            get => _informationList;
            set => SetProperty(ref _informationList, value);
        }
        #endregion

        #region Constructor
        public TuitionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            InformationList = new ObservableCollection<TuitionInformationModel>(GetInformationList());
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private List<TuitionInformationModel> GetInformationList()
        {
            return new List<TuitionInformationModel>
            {
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                },
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                },
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                },
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                },
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                },
                new TuitionInformationModel
                {
                    Title = "Hoc phi thang 11.2020",
                    Expired = "Han dong 30/11/2020",
                    Completed = "1.160.000 VND",
                    Incompleted = "1.345.000 VND"
                }
            };
        }
        #endregion
    }

    public class TuitionInformationModel
    {
        public string Title { get; set; }
        public string Expired { get; set; }
        public string Completed { get; set; }
        public string Incompleted { get; set; }
    }
}
