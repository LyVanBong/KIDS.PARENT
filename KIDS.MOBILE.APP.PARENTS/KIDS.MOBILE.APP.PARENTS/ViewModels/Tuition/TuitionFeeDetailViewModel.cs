using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Tuition;
using KIDS.MOBILE.APP.PARENTS.Services.Tuition;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Tuition
{
    public class TuitionFeeDetailViewModel : BaseViewModel
    {
        #region Properties
        private ITuitionService _tuitionService;

        private ObservableCollection<GetListHistoryTuitionModel> _informationList = new ObservableCollection<GetListHistoryTuitionModel>(new List<GetListHistoryTuitionModel> { new GetListHistoryTuitionModel()});
        public ObservableCollection<GetListHistoryTuitionModel> InformationList
        {
            get => _informationList;
            set => SetProperty(ref _informationList, value);
        }

        private TuitionInformationModel _TuitionInformation;
        public TuitionInformationModel TuitionInformation
        {
            get => _TuitionInformation;
            set => SetProperty(ref _TuitionInformation, value);
        }

        public string StudentName => AppConstants.User.StudentName;
        #endregion

        #region Constructor
        public TuitionFeeDetailViewModel(INavigationService navigationService, ITuitionService tuitionService) : base(navigationService)
        {
            _navigationService = navigationService;
            _tuitionService = tuitionService;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            try
            {
                IsLoading = true;
                base.Initialize(parameters);
                if (parameters.ContainsKey("tuition"))
                {
                    TuitionInformation = (TuitionInformationModel)parameters["tuition"];
                    await GetInformationList();
                }
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

        #region Public methods
        #endregion

        #region Private methods
        private async Task GetInformationList()
        {
            var listHistoryTuition = await _tuitionService.GetListHistoryTuition(TuitionInformation?.Id.ToString() ?? Guid.Empty.ToString());
            if(listHistoryTuition?.Data.Any() == true)
            {
                InformationList = new ObservableCollection<GetListHistoryTuitionModel>(listHistoryTuition.Data);
            }
        }
        #endregion
    }
}
