using KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.HealthCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HealthChartPage : PageBase
    {
        HealthChartViewModel vm;
        public HealthChartPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (HealthChartViewModel)this.BindingContext;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDisappering();
        }
    }
}