using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Services.Album;
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
            set => SetProperty(ref _imageList, value);
        }
        private string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }
        private IAlbumService _albumService;
        private AlbumDetailModel selectionAlbum;
        #endregion

        #region Contructor
        public AlbumDetailViewModel(INavigationService navigationService, IAlbumService albumService) : base(navigationService)
        {
            _navigationService = navigationService;
            _albumService = albumService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
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
        private async Task GetImageList()
        {
            var parentId = AppConstants.User.ParentID;
            var imageList = new List<AlbumDetailModel>();
            var data = await _albumService.GetAlbumDetail(selectionAlbum?.Id.ToString() ?? Guid.Empty.ToString(), parentId);
            if(data?.Data?.Any() == true)
            {
                foreach(var item in data.Data)
                {
                    imageList.Add(new AlbumDetailModel
                    {
                        Uri = $"{AppConstants.UriBaseWebForm}{item.ImageURL}"
                    });
                }
            }
            ImageList = new ObservableCollection<AlbumDetailModel>(imageList);
        }
        #endregion
    }

    public class AlbumDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}

