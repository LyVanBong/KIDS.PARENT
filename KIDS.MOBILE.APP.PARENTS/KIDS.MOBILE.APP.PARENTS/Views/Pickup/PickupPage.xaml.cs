using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Services;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Pickup;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Pickup
{
    public partial class PickupPage : PageBase
    {
        public PickupPage()
        {
            InitializeComponent();
        }

        private PickupViewModel vm;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = (PickupViewModel)this.BindingContext;
        }
    }
}
