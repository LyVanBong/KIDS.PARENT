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

            menuData.Columns[0].CellStyle = style;
            menuData.Columns[1].CellStyle = style;
            menuData.Behaviors.Add(new DataGridBehavior());
            menuData.QueryRowHeight += MenuData_QueryRowHeight;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            menuData.QueryRowHeight -= MenuData_QueryRowHeight;
        }

        private void MenuData_QueryRowHeight(object sender, Syncfusion.SfDataGrid.XForms.QueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = SfDataGridHelpers.GetRowHeight(menuData, e.RowIndex);
                e.Handled = true;
            }
        }

        private void activityList_ItemsSourceChanged(object sender, GridItemsSourceChangedEventArgs e)
        {
            var height = (menuData.View.Records.Count * menuData.RowHeight) + menuData.HeaderRowHeight;
            this.menuData.HeightRequest = (double)height;
        }

        private async void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            await vm?.GetDailyActivity(e.DateAdded.First());
        }
    }
}