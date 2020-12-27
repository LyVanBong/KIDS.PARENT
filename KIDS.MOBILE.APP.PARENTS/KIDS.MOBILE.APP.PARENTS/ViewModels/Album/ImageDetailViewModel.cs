using System;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.ViewModels.Album
{
    public class ImageDetailViewModel : BaseViewModel
    {
        #region Properties
        private string _Uri;
        public string Uri
        {
            get => _Uri;
            set => SetProperty(ref _Uri, value);
        }
        private AlbumDetailModel selectionImage;
        #endregion

        #region Contructor
        public ImageDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                IsLoading = true;
                selectionImage = parameters.GetValue<AlbumDetailModel>("image");
                Uri = selectionImage?.Uri;
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
        #endregion
    }
}

