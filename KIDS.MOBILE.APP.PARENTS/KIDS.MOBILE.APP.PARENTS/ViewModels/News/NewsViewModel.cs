using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.News;
using KIDS.MOBILE.APP.PARENTS.Views.News;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        #region Properties
        
        private INewService _newService;
        private ObservableCollection<NewModel> _newsList = new ObservableCollection<NewModel>();
        public ObservableCollection<NewModel> NewsList
        {
            get => _newsList;
            set => SetProperty(ref _newsList, value);
        }
        public DelegateCommand<object> SelectionCommand { get; }
        #endregion

        #region Contructor
        public NewsViewModel(INavigationService navigationService, INewService newService) : base(navigationService)
        {
            _navigationService = navigationService;
            _newService = newService;
            SelectionCommand = new DelegateCommand<object>(OnSelectionClicked);
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                var schoolId = AppConstants.User.DonVi;
                var classid = AppConstants.User.ClassID;
                var parentId = AppConstants.User.ParentID;
                await GetAllNews(schoolId, parentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        private async Task GetAllNews(string schoolId, string parentId)
        {
            var data = await _newService.GetAllNews(schoolId, parentId);
            if(data?.Data?.Any() == true)
            {
                var newsList = new List<NewModel>();
                foreach(var item in data.Data)
                {
                    newsList.Add(new NewModel
                    {
                        Id = item.NewsID,
                        Title = item.Title,
                        NewsTo = item.TenNguoiTao,
                        DateTime = item.NgayTao,
                        Image = $"{ AppConstants.UriBaseWebForm}{item.ImageURL}",
                        CommentNumber = item.Views.ToString(),
                        LikeNumber = item.Views.ToString()
                    });
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    NewsList = new ObservableCollection<NewModel>(newsList);
                });
            }
        }

        private async void OnSelectionClicked(object dataItem)
        {
            var data = (Syncfusion.ListView.XForms.ItemTappedEventArgs) dataItem;
            var param = new NavigationParameters();
            param.Add("NewsId", ((NewModel)data.ItemData).Id);

            await _navigationService.NavigateAsync(nameof(NewDetailPage), param);
        }
        #endregion
    }

    public class NewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string NewsTo { get; set; }
        public string DateTime { get; set; }
        public string Image { get; set; }
        public string LikeNumber { get; set; }
        public string CommentNumber { get; set; }
        public string CommentText { get => Resource._00083; }
    }
}
