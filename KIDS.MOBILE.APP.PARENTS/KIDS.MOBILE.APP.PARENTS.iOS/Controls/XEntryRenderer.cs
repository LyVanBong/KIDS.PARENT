using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using Foundation;
using KIDS.MOBILE.APP.PARENTS.Controls.Renderers.Entry;
using KIDS.MOBILE.APP.PARENTS.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace KIDS.MOBILE.APP.PARENTS.iOS.Controls
{
    public class XEntryRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            var element = this.Element as XEntry;

            if (Control != null && element != null)
            {
                Control.Layer.MasksToBounds = true;
                Control.Layer.CornerRadius = element.CornerRadius;
                Control.Layer.BorderWidth = 1f;
                Control.Layer.BorderColor = element.BorderColor.ToCGColor();
                Control.SpellCheckingType = UITextSpellCheckingType.No;             // No Spellchecking
                Control.AutocorrectionType = UITextAutocorrectionType.No;           // No Autocorrection
                Control.AutocapitalizationType = UITextAutocapitalizationType.None; // No Autocapitalization
                Control.InputAccessoryView.Hidden = true;//For Removing Done button from editor keyboard.
                Control.KeyboardType = UIKeyboardType.ASCIICapable;//For Removing Emoticons from keyboard.
            }
        }
    }
}
