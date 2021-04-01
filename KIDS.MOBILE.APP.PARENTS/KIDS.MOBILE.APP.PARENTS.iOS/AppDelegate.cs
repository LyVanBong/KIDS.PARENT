using FFImageLoading.Forms.Platform;
using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.SfDataGrid.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using UIKit;

namespace KIDS.MOBILE.APP.PARENTS.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            OtherLibraries();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }

        private void OtherLibraries()
        {
            Rg.Plugins.Popup.Popup.Init();
            SfLinearProgressBarRenderer.Init();
            SfCircularProgressBarRenderer.Init();
            SfChartRenderer.Init();
            SfComboBoxRenderer.Init();
            SfDataGridRenderer.Init();
            SfTimePickerRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfTabViewRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfCalendarRenderer.Init();
            CachedImageRenderer.Init();
            SfListViewRenderer.Init();
            SfCardViewRenderer.Init();
            CachedImageRenderer.Init();
        }
    }
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
