using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Activity
{
    public partial class ActivityPage : ContentPage
    {
        public ActivityPage()
        {
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            activityList.Behaviors.Add(new ListViewBehavior());
            Style style = new Style(typeof(GridCell));
            style.Setters.Add(new Setter() {
                Property= GridCell.ForegroundProperty,
                Value = Color.Black
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
            activityList.Behaviors.Clear();
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
    }

    public class DataGridBehavior : Behavior<SfDataGrid>
    {
        SfDataGrid dataGrid;

        protected override void OnAttachedTo(BindableObject bindable)
        {
            dataGrid = bindable as SfDataGrid;
            dataGrid.GridLoaded += _dataGrid_GridLoaded;
            base.OnAttachedTo(bindable);
        }

        private void _dataGrid_GridLoaded(object sender, Syncfusion.SfDataGrid.XForms.GridLoadedEventArgs e)
        {
            var _dataGrid = sender as SfDataGrid;
            var container = _dataGrid.GetType().GetProperty("VisualContainer", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dataGrid);
            var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "ExtentHeight").GetValue(container);
            _dataGrid.HeightRequest = extent;
        }
    }

    public class ListViewBehavior : Behavior<SfListView>
    {
        private SfListView listView;
        protected override void OnAttachedTo(SfListView bindable)
        {
            listView = bindable;
            listView.Loaded += OnListViewLoaded;
            base.OnAttachedTo(bindable);
        }

        private void OnListViewLoaded(object sender, ListViewLoadedEventArgs e)
        {
            //var container = listView.GetVisualContainer();
            //var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "TotalExtent").GetValue(container);
            //listView.HeightRequest = extent;
        }

        protected override void OnDetachingFrom(SfListView bindable)
        {
            listView.Loaded -= OnListViewLoaded;
            base.OnDetachingFrom(bindable);
        }
    }
}
