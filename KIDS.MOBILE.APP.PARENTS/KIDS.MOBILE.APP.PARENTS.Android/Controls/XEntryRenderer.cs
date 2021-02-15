using System;
using KIDS.MOBILE.APP.PARENTS.Controls.Renderers.Entry;
using KIDS.MOBILE.APP.PARENTS.Droid.Controls;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Views;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace KIDS.MOBILE.APP.PARENTS.Droid.Controls
{
    public class XEntryRenderer : EditorRenderer
    {
        public XEntry ElementV2 => Element as XEntry;

        public XEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                UpdateBackground(Control);
            }

            if (e.NewElement != null)
            {
                var element = e.NewElement as XEntry;
                this.Control.Hint = element.Placeholder;
                this.Control.SetCursorVisible(true);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            UpdateBackground(Control);
            if (e.PropertyName == XEntry.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as XEntry;
                this.Control.Hint = element.Placeholder;
            }
        }

        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Android.Graphics.Color.White);
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
    }
}
