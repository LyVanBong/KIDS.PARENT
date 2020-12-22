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
        private PickupViewModel vm;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = (PickupViewModel)this.BindingContext;
        }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            if (Data == null) return;
            var item = Data?.FirstOrDefault(x => x.Date?.Date == e.Date.Date);
            var model = new AttendanceCalendarModel(e.Date,
                item == null ? string.Empty : (item?.CoMat == true ? "done.png" : "error.png"));
            e.CellBindingContext = model;
        }

        private async void Calendar_MonthChanged(object sender, MonthCellLoadedEventArgs e)
        {
            try
            {
                try
                {
                    loadingView.IsVisible = true;
                    calendar.IsVisible = false;
                    Data = await vm.GetAttendanceForMonth(e.Date);
                    calendar.IsVisible = true;
                    //calendar.Refresh();
                }
                finally
                {
                    loadingView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
