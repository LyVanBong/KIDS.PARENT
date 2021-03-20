using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.Album;
using KIDS.MOBILE.APP.PARENTS.Services.News;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Album;
using KIDS.MOBILE.APP.PARENTS.Views;
using KIDS.MOBILE.APP.PARENTS.Views.Activity;
using KIDS.MOBILE.APP.PARENTS.Views.Album;
using KIDS.MOBILE.APP.PARENTS.Views.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Views.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Views.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Views.Message;
using KIDS.MOBILE.APP.PARENTS.Views.Pickup;
using KIDS.MOBILE.APP.PARENTS.Views.Tuition;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
        private INewService _newService;
        private string _schoolInteraction;
        public string SchoolInteraction
        {
            get => _schoolInteraction;
            set => SetProperty(ref _schoolInteraction, value);
        }

        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private string _neededTitle;
        public string NeededTitle
        {
            get => _neededTitle;
            set => SetProperty(ref _neededTitle, value);
        }

        private ObservableCollection<NeededItem> _neededItems = new ObservableCollection<NeededItem>();
        public ObservableCollection<NeededItem> NeededItems
        {
            get => _neededItems;
            set => SetProperty(ref _neededItems, value);
        }

        public DelegateCommand<MenuItem> ItemTappedCommand { get; set; }

        private ObservableCollection<AlbumDetailModel> _imageList = new ObservableCollection<AlbumDetailModel>();
        public ObservableCollection<AlbumDetailModel> ImageList
        {
            get => _imageList;
            set => SetProperty(ref _imageList, value);
        }
        private IAlbumService _albumService;
        public DelegateCommand<object> SelectionAlbumCommand { get; }
        public DelegateCommand NewsCommand { get; }
        public DelegateCommand AlbumCommand { get; }
        public DelegateCommand<object> NewsDetailCommand { get; }
        #endregion

        public HomeViewModel(INavigationService navigationService, INewService newService, IAlbumService albumService) : base(navigationService)
        {
            _navigationService = navigationService;
            _newService = newService;
            _albumService = albumService;
            ItemTappedCommand = new DelegateCommand<MenuItem>(OnMenuClicked);
            SelectionAlbumCommand = new DelegateCommand<object>(OnSelectionAlbumClicked);
            AlbumCommand = new DelegateCommand(OnAlbumClicked);
            NewsCommand = new DelegateCommand(OnNewClicked);
            NewsDetailCommand = new DelegateCommand<object>(OnNewsDetailClicked);
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            SchoolInteraction = Resource._00038;
            NeededTitle = Resource._00039;
            MenuItems = new ObservableCollection<MenuItem>(CreateMenuList());
            NeededItems = new ObservableCollection<NeededItem>(CreateNeededList());
            await GetAllNews(AppConstants.User.DonVi, AppConstants.User.ClassID);
            await GetImageList();
        }

        private async void OnMenuClicked(MenuItem sender)
        {
            if (!string.IsNullOrEmpty(sender.NavigateTo))
                await _navigationService.NavigateAsync(sender.NavigateTo);
        }

        private async Task GetAllNews(string schoolId, string classId)
        {
            var data = await _newService.GetAllNews(schoolId, classId);
            if (data?.Data?.Any() == true)
            {
                var newsList = new List<NeededItem>();
                foreach (var item in data.Data)
                {
                    newsList.Add(new NeededItem
                    {
                        Id = item.NewsID,
                        Title = item.Title,
                        Image = $"{ AppConstants.UriBaseWebForm}{item.ImageURL}",
                        Like = item.Views ?? 0,
                        Comment = item.Views.ToString()
                    });
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    NeededItems = new ObservableCollection<NeededItem>(newsList);
                });
            }
        }

        #region Public Methods
        #endregion

        #region Private Methods
        private List<MenuItem> CreateMenuList()
        {
            return new List<MenuItem>
            {
                new MenuItem{ Title = Resource._00040,Icon = ImageSource.FromFile("message.png"),NavigateTo=nameof(MessagePage)},
                new MenuItem{ Title = Resource._00041,Icon = ImageSource.FromFile("leave.png"),NavigateTo=nameof(LeaveRequestPage)},
                new MenuItem{ Title = Resource._00042,Icon = ImageSource.FromFile("medicine.png"),NavigateTo=nameof(MedicineAdvisePage)},
                new MenuItem{ Title = Resource._00043,Icon = ImageSource.FromFile("pickup.png"),NavigateTo=nameof(PickupPage)},
                new MenuItem{ Title = "Học tập hàng ngày",Icon = ImageSource.FromFile("activity.png"),NavigateTo=nameof(ActivityPage)},
                new MenuItem{ Title = "Ăn ngủ hàng ngày",Icon = ImageSource.FromFile("eat.png"),NavigateTo=nameof(SleepActivityPage)},
                new MenuItem{ Title = Resource._00045,Icon = ImageSource.FromFile("news.png"),NavigateTo=nameof(NewsPage)},
                new MenuItem{ Title = Resource._00046,Icon = ImageSource.FromFile("album.png"),NavigateTo=nameof(AlbumPage)},
                new MenuItem{ Title = Resource._00047,Icon = ImageSource.FromFile("tuition.png"),NavigateTo=nameof(TuitionPage)},
                new MenuItem{ Title = Resource._00048,Icon = ImageSource.FromFile("health.png"),NavigateTo=nameof(HealthCarePage)},
                //new MenuItem{ Title = Resource._00049,Icon = ImageSource.FromFile("survey.png"),NavigateTo=nameof(SurveyPage)},
                new MenuItem{ Title = Resource._00050,Icon = ImageSource.FromFile("review.png"),NavigateTo=nameof(MessagePage)},
                new MenuItem{ Title = "Camera",Icon = ImageSource.FromFile("cameravideo.png"),NavigateTo=""},
            };
        }

        private List<NeededItem> CreateNeededList()
        {
            return new List<NeededItem>
            {
                new NeededItem{Title="Phuong phap cung cap sat cho tr bang cu den", Content="Tr duoi 6 thang tuoi se duoc cung cap day du luong sat",Like=265,Comment="1 Binh luan"},
                new NeededItem{Title="Phuong phap cung cap sat cho tr bang cu den", Content="Tr duoi 6 thang tuoi se duoc cung cap day du luong sat",Like=265,Comment="1 Binh luan"},
                new NeededItem{Title="Phuong phap cung cap sat cho tr bang cu den", Content="Tr duoi 6 thang tuoi se duoc cung cap day du luong sat",Like=265,Comment="1 Binh luan"},
                new NeededItem{Title="Phuong phap cung cap sat cho tr bang cu den", Content="Tr duoi 6 thang tuoi se duoc cung cap day du luong sat",Like=265,Comment="1 Binh luan"},
                new NeededItem{Title="Phuong phap cung cap sat cho tr bang cu den", Content="Tr duoi 6 thang tuoi se duoc cung cap day du luong sat",Like=265,Comment="1 Binh luan"},
            };
        }

        private async Task GetImageList()
        {
            var parentId = AppConstants.User.ParentID;
            var schoolId = AppConstants.User.DonVi;
            var imageList = new List<AlbumDetailModel>();
            var data = await _albumService.GetAlbumListByClass(parentId, schoolId);
            if (data?.Data?.Any() == true)
            {
                foreach (var item in data.Data)
                {
                    imageList.Add(new AlbumDetailModel
                    {
                        Id = item.AlbumID,
                        Name = item.Description,
                        Source = new Uri($"{AppConstants.UriBaseWebForm}{item.Thumbnail}")
                    });
                }
            }
            ImageList = new ObservableCollection<AlbumDetailModel>(imageList);
        }

        private async void OnSelectionAlbumClicked(object data)
        {
            var item = ((Syncfusion.ListView.XForms.ItemTappedEventArgs)data).ItemData;
            var param = new NavigationParameters();
            param.Add("album", item);
            await _navigationService.NavigateAsync(nameof(AlbumDetailpage), param);
        }

        private async void OnNewClicked()
        {
            await _navigationService.NavigateAsync(nameof(NewsPage));
        }

        private async void OnAlbumClicked()
        {
            await _navigationService.NavigateAsync(nameof(AlbumPage));
        }

        private async void OnNewsDetailClicked(object data)
        {
            var item = (Syncfusion.ListView.XForms.ItemTappedEventArgs)data;
            var id = ((NeededItem)item.ItemData).Id;
            var uri = $"{AppConstants.UriNewsWebForm}{id}";
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        #endregion
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public string NavigateTo { get; set; }
    }

    public class NeededItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        private string content;
        public string Content 
        {
            get
            {
                if(content?.Length >= 100)
                {
                    return $"{content.Substring(0, 100)}...";
                }
                return content ?? string.Empty;
            }
            set
            {
                content = value;
            }
        }
        public int Like { get; set; }
        public string Comment { get; set; }
    }
}