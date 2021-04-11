using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Commons;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Tuition;
using KIDS.MOBILE.APP.PARENTS.Views.Tuition;
using Prism.Commands;
using Prism.Navigation;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Tuition
{
    public class TuitionViewModel : BaseViewModel
    {
        #region Properties
        private ITuitionService _tuitionService;
        private ObservableCollection<TuitionInformationModel> _informationList = new ObservableCollection<TuitionInformationModel>();
        public ObservableCollection<TuitionInformationModel> InformationList
        {
            get => _informationList;
            set => SetProperty(ref _informationList, value);
        }

        private string _totalCompleted;
        public string TotalCompleted
        {
            get => _totalCompleted;
            set => SetProperty(ref _totalCompleted, value);
        }

        private string _nCompleted;
        public string UnCompleted
        {
            get => _nCompleted;
            set => SetProperty(ref _nCompleted, value);
        }

        private string _tuitionStatus;
        public string TuitionStatus
        {
            get => _tuitionStatus;
            set => SetProperty(ref _tuitionStatus, value);
        }

        private string _tuitionStatusColor;
        public string TuitionStatusColor
        {
            get => _tuitionStatusColor;
            set => SetProperty(ref _tuitionStatusColor, value);
        }

        private string _completed;
        public string Completed
        {
            get => _completed;
            set => SetProperty(ref _completed, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _expired;
        public string Expired
        {
            get => _expired;
            set => SetProperty(ref _expired, value);
        }

        private TuitionInformationModel _SelectedTuition;
        public TuitionInformationModel SelectedTuition
        {
            get => _SelectedTuition;
            set => SetProperty(ref _SelectedTuition, value);
        }

        public DelegateCommand<object> SelectedTuitionCommand { get; }
        #endregion

        #region Constructor
        public TuitionViewModel(INavigationService navigationService, ITuitionService tuitionService) : base(navigationService)
        {
            _navigationService = navigationService;
            _tuitionService = tuitionService;
            SelectedTuitionCommand = new DelegateCommand<object>(OnSelectionClicked);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            
            var studentId = AppConstants.User.StudentID;
            Task.Run(async () =>
            {
                var listTuition = await _tuitionService.GetCurrentTuition(studentId.ToString());
                if(listTuition?.Data?.Any() == true)
                {
                    var currentTuition = listTuition.Data.First();
                    UnCompleted = currentTuition.TrangThai == "CHƯA ĐÓNG" ? $"{string.Format("{0:n}", currentTuition.TongCong)} {Resource._00082}" : $"0 {Resource._00082}";
                    TuitionStatus = currentTuition.TrangThai;
                    TuitionStatusColor = currentTuition.TrangThai == "CHƯA ĐÓNG" ? "#FF0000" : "#0000FF";
                    Completed = $"{string.Format("{0:n}", currentTuition.TongCong)} {Resource._00082}";
                    Title = currentTuition.TenDotThu;
                    Expired = currentTuition.ThoiGianThu != null ? $"{Resource._00089}{currentTuition.ThoiGianThu.Value.ToShortDateString()}" : $"{Resource._00089}";

                    decimal total = 0;
                    decimal reduce = 0;
                    var historyList = new List<TuitionInformationModel>();
                    foreach (var item in listTuition.Data)
                    {
                        total += item.TongCong ?? 0;
                        historyList.Add(new TuitionInformationModel
                        {
                            Id = item.DotThu_HocSinhID ?? Guid.Empty,
                            Title = item.TenDotThu,
                            Expired = item.ThoiGianThu != null ? $"{Resource._00084} {item.ThoiGianThu.Value.ToShortDateString()}" : string.Empty,
                            Completed = $"{string.Format("{0:n}", item.TongCong)} {Resource._00082}",
                            Status = $"{item.TrangThai}",
                            TuitionStatusColor = item.TrangThai == "CHƯA ĐÓNG" ? "#FF0000" : "#0000FF"
                        });
                    }
                    TotalCompleted = $"{string.Format("{0:n}", total)} VND";
                    InformationList = new ObservableCollection<TuitionInformationModel>(historyList);
                }
            });
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private async void OnSelectionClicked(object data)
        {
            var item = ((Syncfusion.ListView.XForms.ItemTappedEventArgs)data).ItemData;
            var param = new NavigationParameters();
            param.Add("tuition", item);
            await _navigationService.NavigateAsync(nameof(TuitionFeeDetailPage), param);
        }
        #endregion
    }

    public class TuitionInformationModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Expired { get; set; }
        public string Completed { get; set; }
        public string Status { get; set; }
        public string TuitionStatusColor { get; set; }
    }
}
