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
        private IAlbumService _albumService;
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
            var classId = AppConstants.User.ClassID;
            var schoolId = AppConstants.User.DonVi;
            var imageList = new List<AlbumDetailModel>();
            var data = await _albumService.GetAlbumListByClass(classId, schoolId);
            if(data?.Data?.Any() == true)
            {
                foreach(var item in data.Data)
                {
                    imageList.Add(new AlbumDetailModel
                    {
                        Uri = $"{AppConstants.UrlApiApp}{item.Thumbnail}"
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

