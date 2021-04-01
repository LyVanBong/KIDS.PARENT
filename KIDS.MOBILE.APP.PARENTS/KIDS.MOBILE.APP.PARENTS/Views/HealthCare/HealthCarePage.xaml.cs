using System;
using System.Collections.Generic;
using KIDS.MOBILE.APP.PARENTS.ViewModels.HeatlCare;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.HealthCare
{
    public partial class HealthCarePage : PageBase
    {
        private HealthCareViewModel vm;
        public HealthCarePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (HealthCareViewModel)this.BindingContext;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDisappering();
        }
    }
}
