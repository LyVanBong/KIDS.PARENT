using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using KIDS.MOBILE.APP.PARENTS.Droid;
using KIDS.MOBILE.APP.PARENTS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
[assembly: Dependency(typeof(ImageService))]
namespace KIDS.MOBILE.APP.PARENTS.Droid
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
        }

        public string SaveToGallery(Xamarin.Forms.View view)
        {
            try
            {
                var widthRequest = MainActivity.ScreenWidth;
                var heightRequest = view.Height * MainActivity.Density;

                var nativeView = ConvertFormsToNative(view, new Rectangle(0, 0, widthRequest, heightRequest));

                var bitMap = GetBitmapFromView(nativeView, (int)heightRequest, (int)widthRequest);

                var sdCardPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                var fileName = System.IO.Path.Combine(sdCardPath, DateTime.Now.ToString("yyyyMMddhhmmss") + ".png");
                using (var os = new FileStream(fileName, FileMode.CreateNew))
                {
                    bitMap.Compress(Bitmap.CompressFormat.Png, 100, os);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Cannot take screenshot. Please try again.";
            }
        }

        private Android.Views.View ConvertFormsToNative(Xamarin.Forms.View view, Rectangle size)
        {
            var vRenderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(view);
            Xamarin.Forms.Platform.Android.Platform.SetRenderer(view, vRenderer);
            vRenderer.UpdateLayout();
            var viewGroup = vRenderer.View;
            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
            viewGroup.LayoutParameters = layoutParams;
            viewGroup.Layout(0, 0, (int)size.Width, (int)size.Height);

            return viewGroup;
        }

        private Bitmap GetBitmapFromView(Android.Views.View view, int totalHeight, int totalWidth)
        {
            Bitmap returnedBitmap = Bitmap.CreateBitmap(totalWidth, totalHeight, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(returnedBitmap);
            Drawable bgDrawable = view.Background;
            if (bgDrawable != null)
                bgDrawable.Draw(canvas);
            else
                canvas.DrawColor(Android.Graphics.Color.Transparent);
            view.Layout(0, 0, totalWidth, totalHeight);
            view.Draw(canvas);
            return returnedBitmap;
        }

        public async Task CheckPermission()
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (permission != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
        }

    }
}