using System;
using System.Collections.Generic;
using KIDS.MOBILE.APP.PARENTS.ViewModels;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Message
{
    public partial class MessagePage : PageBase
    {
        MessageViewModel vm;
        public MessagePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (MessageViewModel)this.BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm?.OnAppearing();
        }
    }
}
