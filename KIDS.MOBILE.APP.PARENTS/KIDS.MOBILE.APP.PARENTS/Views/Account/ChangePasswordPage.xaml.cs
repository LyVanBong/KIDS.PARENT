using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }
        private void FocusePassword_OnFocused(object sender, FocusEventArgs e)
        {
        }
        private void UnfocusePassword_OnUnfocused(object sender, FocusEventArgs e)
        {
        }
    }
}