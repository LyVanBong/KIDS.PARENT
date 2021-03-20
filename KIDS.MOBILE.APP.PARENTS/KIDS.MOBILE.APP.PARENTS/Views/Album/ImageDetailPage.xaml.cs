using KIDS.MOBILE.APP.PARENTS.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Album
{
    public partial class ImageDetailPage : PageBase
    {
        public ImageDetailPage()
        {
            InitializeComponent();
        }

        private async void Save_Tapped(object sender, EventArgs e)
        {
            try
            {
                var imageService = DependencyService.Get<IImageService>();
                try
                {
                    await imageService.CheckPermission();
                }
                catch (Exception ex)
                {

                }
                var result = imageService.SaveToGallery(picture);
                await DisplayAlert(result == "Success" ? "Success" : "Error", result, "Ok");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
