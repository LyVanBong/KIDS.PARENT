using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #endregion

        #region Contructor
        public AlbumDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                GetImageList();
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
        private void GetImageList()
        {
            var imageList = new List<AlbumDetailModel>();
            for(int i = 0; i < 20; i++)
            {
                imageList.Add(new AlbumDetailModel());
            }
            ImageList = new ObservableCollection<AlbumDetailModel>(imageList);
        }
        #endregion
    }

    public class AlbumDetailModel
    {
        public string Uri { get; set; }
    }
}

