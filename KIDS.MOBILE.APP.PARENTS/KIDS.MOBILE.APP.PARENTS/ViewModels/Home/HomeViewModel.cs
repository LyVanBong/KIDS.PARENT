using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KIDS.MOBILE.APP.PARENTS.Resources;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties
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

        public ICommand ItemTappedCommand { get; set; }
        private INavigationService _navigationService;
        #endregion
        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            SchoolInteraction = Resource._00038;
            NeededTitle = Resource._00039;
            MenuItems = new ObservableCollection<MenuItem>(CreateMenuList());
            NeededItems = new ObservableCollection<NeededItem>(CreateNeededList());
            ItemTappedCommand = new Command<MenuItem>(async (sender) => await OnMenuClicked(sender)); ;
        }

        private async Task OnMenuClicked(MenuItem sender)
        {
            if (!string.IsNullOrEmpty(sender.NavigateTo))
                await _navigationService.NavigateAsync(sender.NavigateTo);
        }

        #region Public Methods
        #endregion

        #region Private Methods
        private List<MenuItem> CreateMenuList()
        {
            return new List<MenuItem>
            {
                new MenuItem{ Title = Resource._00040,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00041,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00042,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00043,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00044,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00045,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00046,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00047,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00048,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00049,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
                new MenuItem{ Title = Resource._00050,Icon = ImageSource.FromResource("home.png"),NavigateTo=""},
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

    public class GiftItem
    {
        public string TiTle { get; set; }
        public ImageSource Icon { get; set; }
        public string Time { get; set; }
    }

    public class NeededItem
    {
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public string Content { get; set; }
        public int Like { get; set; }
        public string Comment { get; set; }
    }
}