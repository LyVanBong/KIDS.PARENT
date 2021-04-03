using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : PageBase
    {
        int SlidePosition = 0;

        public HomePage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                SlidePosition++;
                if (SlidePosition == 3) SlidePosition = 0;
                carouselView.Position = SlidePosition;
                return true;
            });
        }
    }
}