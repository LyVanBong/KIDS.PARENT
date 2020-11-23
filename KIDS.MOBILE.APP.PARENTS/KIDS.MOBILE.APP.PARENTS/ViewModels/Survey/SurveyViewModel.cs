using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Survey
{
    public class SurveyViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private ObservableCollection<SurveyModel> _surveysList = new ObservableCollection<SurveyModel>();
        public ObservableCollection<SurveyModel> SurveysList
        {
            get => _surveysList;
            set => SetProperty(ref _surveysList, value);
        }

        public SurveyViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            SurveysList = new ObservableCollection<SurveyModel>(GetSurveysList());
        }

        private List<SurveyModel> GetSurveysList()
        {
            return new List<SurveyModel>
            {
                new SurveyModel
                {
                    Title = "Khảo sát học sinh Lê Khắc Đăng Khoa: Nhà trường",
                    CreatedDateTime = "Tạo ngày 15/08/2018",
                    Content = "Đăng ký gửi trẻ thứ 7 ngày 18/8/2018",
                    ExpiryDate = "Đã hết hạn 17/08/2018"
                },
                new SurveyModel
                {
                    Title = "Khảo sát học sinh Lê Khắc Đăng Khoa: Nhà trường",
                    CreatedDateTime = "Tạo ngày 15/08/2018",
                    Content = "Đăng ký gửi trẻ thứ 7 ngày 18/8/2018",
                    ExpiryDate = "Đã hết hạn 17/08/2018"
                },
                new SurveyModel
                {
                    Title = "Khảo sát học sinh Lê Khắc Đăng Khoa: Nhà trường",
                    CreatedDateTime = "Tạo ngày 15/08/2018",
                    Content = "Đăng ký gửi trẻ thứ 7 ngày 18/8/2018",
                    ExpiryDate = "Đã hết hạn 17/08/2018"
                },
                new SurveyModel
                {
                    Title = "Khảo sát học sinh Lê Khắc Đăng Khoa: Nhà trường",
                    CreatedDateTime = "Tạo ngày 15/08/2018",
                    Content = "Đăng ký gửi trẻ thứ 7 ngày 18/8/2018",
                    ExpiryDate = "Đã hết hạn 17/08/2018"
                }
            };
        }
    }

    public class SurveyModel
    {
        public string Title { get; set; }
        public string CreatedDateTime { get; set; }
        public string Content { get; set; }
        public string ExpiryDate { get; set; }
    }
}
