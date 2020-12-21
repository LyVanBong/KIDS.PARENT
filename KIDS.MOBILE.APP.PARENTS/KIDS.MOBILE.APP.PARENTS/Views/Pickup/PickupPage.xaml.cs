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
    public partial class PickupPage : ContentPage
    {
        public PickupPage()
        {
            InitializeComponent();
        }
        private List<GetAttendanceForMonthModel> Data = null;
        protected async override void OnAppearing()
        {
            try
            {
                loadingView.IsVisible = true;
                calendar.IsVisible = false;
                base.OnAppearing();
                var vm = (PickupViewModel)this.BindingContext;

                Data = await vm.GetAttendanceForMonth();
                calendar.IsVisible = true;
                calendar.Refresh();
            }
            finally
            {
                loadingView.IsVisible = false;
            }
        }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            try
            {
                var item = Data?.FirstOrDefault(x => x.Date?.Date == e.Date.Date);
                var model = new AttendanceCalendarModel(e.Date,
                    item == null ? string.Empty : (item?.CoMat == true ? "done.png" : "error.png"));
                e.CellBindingContext = model;
                //var grid = new Grid
                //{
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.FillAndExpand,
                //    RowDefinitions =
                //{
                //    new RowDefinition{Height= new GridLength(1, GridUnitType.Star)},
                //    new RowDefinition{Height= new GridLength(1, GridUnitType.Star)}
                //}
                //};
                //grid.Children.Add(new Label
                //{
                //    Text = e.Date.Day.ToString(),
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    TextColor = Color.Black,
                //    FontSize = 10
                //}, 0, 0);

                //grid.Children.Add(new Image
                //{
                //    WidthRequest=30,
                //    HeightRequest=30,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    Source = item == null ? null : (item?.CoMat == true ? "done.png" : "error.png")
                //}, 0, 1);

                //e.View = grid;
                //calendar.SelectedDate = e.Date;
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class AttendanceCalendarModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string image;
        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    image = value;
                    OnPropertyChanged(nameof(Image));
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public bool IsVisible
        {
            get => !string.IsNullOrEmpty(Image);
        }

        public DateTime Date { get; set; }

        public AttendanceCalendarModel(DateTime date, string image)
        {
            this.Date = date;
            this.Image = image;
        }
    }
}
