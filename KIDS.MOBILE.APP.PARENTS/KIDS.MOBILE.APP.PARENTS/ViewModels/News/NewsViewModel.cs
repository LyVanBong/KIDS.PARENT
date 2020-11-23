using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        #region Properties
        private INavigationService _navigationService;
        private ObservableCollection<NewModel> _newsList = new ObservableCollection<NewModel>();
        public ObservableCollection<NewModel> NewsList
        {
            get => _newsList;
            set => SetProperty(ref _newsList, value);
        }
        #endregion

        #region Contructor
        public NewsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            NewsList = new ObservableCollection<NewModel>(GetNewsList());
        }
        #endregion

        #region Public methods
        #endregion

        #region Private methods
        private List<NewModel> GetNewsList()
        {
            List<NewModel> result = new List<NewModel>();
            result.Add(new NewModel
            {
                Title="CHIA SE VE HIEN TUONG CAM GIO",
                NewsTo = "Toan truong",
                DateTime=DateTime.UtcNow.ToLongDateString(),
                Image = "",
                LikeNumber = "2",
                CommentNumber ="10"
            });
            result.Add(new NewModel
            {
                Title = "CHIA SE VE HIEN TUONG CAM GIO",
                NewsTo = "Toan truong",
                DateTime = DateTime.UtcNow.ToLongDateString(),
                Image = "",
                LikeNumber = "2",
                CommentNumber = "10"
            });
            result.Add(new NewModel
            {
                Title = "CHIA SE VE HIEN TUONG CAM GIO",
                NewsTo = "Toan truong",
                DateTime = DateTime.UtcNow.ToLongDateString(),
                Image = "",
                LikeNumber = "2",
                CommentNumber = "10"
            });
            return result;
        }
        #endregion
    }

    public class NewModel
    {
        public string Title { get; set; }
        public string NewsTo { get; set; }
        public string DateTime { get; set; }
        public ImageSource Image { get; set; }
        public string LikeNumber { get; set; }
        public string CommentNumber { get; set; }
        public string CommentText { get => "Binh luan"; }
    }
}
