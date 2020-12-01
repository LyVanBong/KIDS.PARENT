using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Tuition;
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
        #endregion

        #region Constructor
        public TuitionViewModel(INavigationService navigationService, ITuitionService tuitionService) : base(navigationService)
        {
            _navigationService = navigationService;
            _tuitionService = tuitionService;
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
                    UnCompleted = currentTuition.TrangThai == "CHƯA ĐÓNG" ? $"{currentTuition.TongCong} {Resource._00082}" : $"0 {Resource._00082}";
                }
            });
            Task.Run(async () =>
            {
                var listHistoryTuition = await _tuitionService.GetListHistoryTuition(studentId.ToString());
                if (listHistoryTuition?.Data?.Any() == true)
                {
                    decimal total = 0;
                    decimal reduce = 0;
                    var historyList = new List<TuitionInformationModel>();
                    foreach (var item in listHistoryTuition.Data)
                    {
                        total += item.SoTien ?? 0;
                        reduce += item.MienGiam ?? 0;
                        historyList.Add(new TuitionInformationModel
                        {
                            Title = item.TenKhoanThu,
                            Expired = item.Ngay != null ? $"{Resource._00084} {item.Ngay.Value.ToShortDateString()}" : string.Empty,
                            Completed = $"{item.SoTien} {Resource._00082}",
                            Reduced = $"{item.MienGiam} {Resource._00082}"
                        });
                    }
                    InformationList = new ObservableCollection<TuitionInformationModel>(historyList);
                }
            });
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        //private List<TuitionInformationModel> GetInformationList()
        //{
        //    return new List<TuitionInformationModel>
        //    {
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        },
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        },
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        },
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        },
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        },
        //        new TuitionInformationModel
        //        {
        //            Title = "Hoc phi thang 11.2020",
        //            Expired = "Han dong 30/11/2020",
        //            Completed = "1.160.000 VND",
        //            Reduced = "1.345.000 VND"
        //        }
        //    };
        //}
        #endregion
    }

    public class TuitionInformationModel
    {
        public string Title { get; set; }
        public string Expired { get; set; }
        public string Completed { get; set; }
        public string Reduced { get; set; }
    }
}
