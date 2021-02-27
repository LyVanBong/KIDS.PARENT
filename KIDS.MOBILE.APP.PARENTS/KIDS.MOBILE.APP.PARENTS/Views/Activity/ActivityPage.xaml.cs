using System.Linq;
using System.Reflection;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Activity;
using Prism.Mvvm;
using Syncfusion.ListView.XForms;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Activity
{
    public partial class ActivityPage : PageBase
    {
        ActivityViewModel vm;
        public ActivityPage()
        {
            InitializeComponent();
            ViewModelLocator.SetAutowireViewModel(this, true);
            vm = (ActivityViewModel) this.BindingContext;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (ActivityViewModel)this.BindingContext;
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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            activityList.Behaviors.Clear();
        }

        private async  void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            await vm?.GetDailyActivity(e.DateAdded.First());
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
