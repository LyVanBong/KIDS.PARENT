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
    }
}
