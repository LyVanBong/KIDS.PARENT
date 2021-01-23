using System;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace KIDS.MOBILE.APP.PARENTS.Views
{
    public class PageBase : ContentPage, INavigationAware
    {
        public PageBase()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -20;
            Padding = safeInsets;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
