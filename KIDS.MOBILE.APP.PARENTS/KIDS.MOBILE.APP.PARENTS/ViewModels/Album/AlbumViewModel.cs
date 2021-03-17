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
    public class AlbumViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<AlbumDetailModel> _imageList = new ObservableCollection<AlbumDetailModel>();
        public ObservableCollection<AlbumDetailModel> ImageList
        {
            get => _imageList;
            set => SetProperty(ref _imageList, value);
        }
        private IAlbumService _albumService;
        public DelegateCommand<object> SelectionCommand { get; }
        #endregion

        #region Contructor
        public AlbumViewModel(INavigationService navigationService, IAlbumService albumService) : base(navigationService)
        {
            _navigationService = navigationService;
            _albumService = albumService;
            SelectionCommand = new DelegateCommand<object>(OnSelectionClick);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
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

        private async void OnSelectionClick(object data)
        {
            var item = ((Syncfusion.ListView.XForms.ItemTappedEventArgs)data).ItemData;
            var param = new NavigationParameters();
            param.Add("album", item);
            await _navigationService.NavigateAsync(nameof(AlbumDetailpage), param);
        }
        #endregion
    }
}

