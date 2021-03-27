using System;
using System.Collections.Generic;
using KIDS.MOBILE.APP.PARENTS.ViewModels.MedicineAdvise;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.MedicineAdvise
{
    public partial class MedicineAdvisePage : PageBase
    {
        MedicineAdviseViewModel vm;

        public MedicineAdvisePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (MedicineAdviseViewModel)this.BindingContext;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    vm.OnAppearing();
        //}
    }
}
