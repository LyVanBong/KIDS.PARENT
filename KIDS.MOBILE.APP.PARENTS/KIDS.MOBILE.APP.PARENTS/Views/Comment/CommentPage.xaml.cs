using System;
using System.Collections.Generic;
using System.Linq;
using KIDS.MOBILE.APP.PARENTS.ViewModels.Comment;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Views.Comment
{
    public partial class CommentPage : PageBase
    {
        CommentViewModel vm;
        public CommentPage()
        {
            InitializeComponent();
            dailyStack.IsVisible = true;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = (CommentViewModel)this.BindingContext;
        }

        private async void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            vm.IsLoading = true;
            await vm?.GetAttendanceForMonth(e.DateAdded.First());
            vm.IsLoading = false;
        }

        private void DailyComment_Clicked(object sender, EventArgs e)
        {
            if (!dailyStack.IsVisible)
            {
                weeklyStack.IsVisible = false;
                dailyStack.IsVisible = true;
                dailyComment.TextColor = Color.White;
                dailyComment.BackgroundColor = Color.FromHex("#fa39b7");
                weeklyComment.TextColor = Color.Black;
                weeklyComment.BackgroundColor = Color.White;
            }
        }

        private void WeeklyComment_Clicked(object sender, EventArgs e)
        {
            if (dailyStack.IsVisible)
            {
                weeklyStack.IsVisible = true;
                dailyStack.IsVisible = false;
                weeklyComment.TextColor = Color.White;
                weeklyComment.BackgroundColor = Color.FromHex("#fa39b7");
                dailyComment.TextColor = Color.Black;
                dailyComment.BackgroundColor = Color.White;
            }
        }
    }
}
