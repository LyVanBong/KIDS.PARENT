using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Resources;
using KIDS.MOBILE.APP.PARENTS.Services.News;
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
        
        #endregion
        public HomeViewModel(INavigationService navigationService, INewService newService) : base(navigationService)
        {
            _navigationService = navigationService;
            _newService = newService;
            ItemTappedCommand = new DelegateCommand<MenuItem>(OnMenuClicked);
        }
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            SchoolInteraction = Resource._00038;
            NeededTitle = Resource._00039;
            MenuItems = new ObservableCollection<MenuItem>(CreateMenuList());
            NeededItems = new ObservableCollection<NeededItem>(CreateNeededList());
            await GetAllNews(AppConstants.User.DonVi, AppConstants.User.ClassID);
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
                        Like = item.Views,
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
                new MenuItem{ Title = Resource._00044,Icon = ImageSource.FromFile("activity.png"),NavigateTo=nameof(ActivityPage)},
                new MenuItem{ Title = Resource._00045,Icon = ImageSource.FromFile("news.png"),NavigateTo=nameof(NewsPage)},
                new MenuItem{ Title = Resource._00046,Icon = ImageSource.FromFile("album.png"),NavigateTo=nameof(AlbumDetailpage)},
                new MenuItem{ Title = Resource._00047,Icon = ImageSource.FromFile("tuition.png"),NavigateTo=nameof(TuitionPage)},
                new MenuItem{ Title = Resource._00048,Icon = ImageSource.FromFile("health.png"),NavigateTo=nameof(HealthCarePage)},
                new MenuItem{ Title = Resource._00049,Icon = ImageSource.FromFile("survey.png"),NavigateTo=nameof(SurveyPage)},
                new MenuItem{ Title = Resource._00050,Icon = ImageSource.FromFile("review.png"),NavigateTo=nameof(MessagePage)},
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