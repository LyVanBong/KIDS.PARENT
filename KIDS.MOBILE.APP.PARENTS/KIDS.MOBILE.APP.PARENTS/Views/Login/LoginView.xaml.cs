using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void FocusePassword_OnFocused(object sender,FocusEventArgs e)
        {
            ViewPass.IsVisible = true;
        }
        private void UnfocusePassword_OnUnfocused(object sender, FocusEventArgs e)
        {
            ViewPass.IsVisible = false;
        }
        private void CheckSaveAccount_OnTapped(object sender, EventArgs e)
        {
            SaveAccount.IsChecked = SaveAccount.IsChecked ? false : true;
        }
    }
}