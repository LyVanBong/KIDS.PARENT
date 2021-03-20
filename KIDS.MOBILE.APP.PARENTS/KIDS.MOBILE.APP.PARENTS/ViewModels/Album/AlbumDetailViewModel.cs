using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.Album;
using KIDS.MOBILE.APP.PARENTS.Views.Album;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Album
{
    public class AlbumDetailViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<AlbumDetailModel> _imageList = new ObservableCollection<AlbumDetailModel>();
        public ObservableCollection<AlbumDetailModel> ImageList
        {
            get => _imageList;
            set {
                _imageList = value;
                RaisePropertyChanged(nameof(ImageList));
            }
        }

        private ObservableCollection<AlbumDetailModel> _loadingList = new ObservableCollection<AlbumDetailModel>();
        public ObservableCollection<AlbumDetailModel> LoadingList
        {
            get => _loadingList;
            set
            {
                _loadingList = value;
                RaisePropertyChanged(nameof(LoadingList));
            }
        }

        private bool _IsImageLoading;
        public bool IsImageLoading
        {
            get => _IsImageLoading;
            set => SetProperty(ref _IsImageLoading, value);
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        public DelegateCommand<object> SelectionCommand { get; }
        private IAlbumService _albumService;
        private AlbumDetailModel selectionAlbum;
        #endregion

        #region Contructor
        public AlbumDetailViewModel(INavigationService navigationService, IAlbumService albumService) : base(navigationService)
        {
            _navigationService = navigationService;
            _albumService = albumService;
            SelectionCommand = new DelegateCommand<object>(OnSelectionClicked);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                IsImageLoading = true;
                selectionAlbum = parameters.GetValue<AlbumDetailModel>("album");
                Title = selectionAlbum?.Name;
                await GetImageList();
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
        List<AlbumDetailModel> imageList = new List<AlbumDetailModel>();
        List<AlbumDetailModel> loadingList = new List<AlbumDetailModel>();
        private async Task GetImageList()
        {
            var parentId = AppConstants.User.ParentID;
            
            var data = await _albumService.GetAlbumDetail(selectionAlbum?.Id.ToString() ?? Guid.Empty.ToString(), parentId);
            if(data?.Data?.Any() == true)
            {
                foreach(var item in data.Data)
                {
                    loadingList.Add(new AlbumDetailModel
                    {
                        Source = ImageSource.FromFile("loading.gif")
                    });
                    imageList.Add(new AlbumDetailModel
                    {
                        Source = new Uri($"{AppConstants.UriBaseWebForm}{item.ImageURL}")
                    }); ;
                }
            }

            ImageList = new ObservableCollection<AlbumDetailModel>(imageList);
            LoadingList = new ObservableCollection<AlbumDetailModel>(loadingList);
            int count = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsImageLoading = false;
                    });
                    return false;
                }
                count++;
                return true;
            });
        }

        private async void OnSelectionClicked(object data)
        {
            var item = ((Syncfusion.ListView.XForms.ItemTappedEventArgs)data).ItemData;
            var param = new NavigationParameters();
            param.Add("image", item);
            await _navigationService.NavigateAsync(nameof(ImageDetailPage), param);
        }
        #endregion
    }

    public class AlbumDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ImageSource Source { get; set; }
    }
}

