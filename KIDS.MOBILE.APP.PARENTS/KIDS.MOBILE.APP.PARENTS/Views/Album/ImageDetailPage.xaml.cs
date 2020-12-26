using KIDS.MOBILE.APP.PARENTS.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Album
{
    public partial class ImageDetailPage : ContentPage
    {
        public ImageDetailPage()
        {
            InitializeComponent();
        }

        private async void Save_Tapped(object sender, EventArgs e)
        {
            var imageService = DependencyService.Get<IImageService>();
            await imageService.CheckPermission();
            var result = imageService.SaveToGallery(picture);
            await DisplayAlert(result == "Success" ? "Success" : "Error", result, "Ok");
        }
    }
}
