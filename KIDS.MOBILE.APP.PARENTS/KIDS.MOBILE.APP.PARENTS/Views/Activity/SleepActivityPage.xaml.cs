using KIDS.MOBILE.APP.PARENTS.ViewModels.Activity;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KIDS.MOBILE.APP.PARENTS.Views.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepActivityPage : PageBase
    {
        SleepActivityViewModel vm;
        public SleepActivityPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (SleepActivityViewModel)this.BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Style style = new Style(typeof(GridCell));
            style.Setters.Add(new Setter()
            {
                Property = GridCell.ForegroundProperty,
                Value = Color.Black,
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            vm.IsLoading = true;
            await vm?.GetDailyActivity(e.DateAdded.First());
            vm.IsLoading = false;
        }
    }
}