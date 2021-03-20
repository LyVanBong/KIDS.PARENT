
using System;
using System.Threading.Tasks;
using CoreGraphics;
using KIDS.MOBILE.APP.PARENTS.iOS;
using KIDS.MOBILE.APP.PARENTS.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ImageService))]
namespace KIDS.MOBILE.APP.PARENTS.iOS
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
        }

        public Task CheckPermission()
        {
            throw new System.NotImplementedException();
        }

        public string SaveToGallery(View view)
        {
            var rect = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, view.Height);
            var nativeView = ConvertToNativeView(view, rect);
            UIGraphics.BeginImageContextWithOptions(nativeView.Bounds.Size, false, 0);
            nativeView.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            nativeView.DrawViewHierarchy(rect, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            image.SaveToPhotosAlbum((sender, args) =>
            {
                Console.WriteLine("Image save successfully");
            });
            return "Success";
        }

        private UIView ConvertToNativeView(View view, CGRect size)
        {
            var renderer = Platform.CreateRenderer(view);
            renderer.NativeView.Frame = size;
            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleAspectFill;
            renderer.Element.Layout(size.ToRectangle());
            var nativeView = renderer.NativeView;
            nativeView.SetNeedsLayout();
            return nativeView;
        }

    }
}
