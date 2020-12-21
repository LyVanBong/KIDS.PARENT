using System;

using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest
{
    public class GetAttendanceForMonthModel : ContentPage
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> StudentID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> CoMat { get; set; }
        public Nullable<bool> NghiCoPhep { get; set; }
        public Nullable<bool> NghiKhongPhep { get; set; }
        public Nullable<bool> DiMuon { get; set; }
        public Nullable<int> Hygiene { get; set; }
        public Nullable<System.DateTime> Leave { get; set; }
        public Nullable<bool> LeaveLate { get; set; }
        public Nullable<System.Guid> NguoiDon { get; set; }
        public Nullable<System.DateTime> SleepFrom { get; set; }
        public Nullable<System.DateTime> SleepTo { get; set; }
        public string Description { get; set; }
        public Nullable<System.Guid> UserCreate { get; set; }
        public string MealComment0 { get; set; }
        public string MealComment1 { get; set; }
        public string MealComment2 { get; set; }
        public string MealComment3 { get; set; }
        public string MealComment4 { get; set; }
        public string MealComment5 { get; set; }
        public string ArriveComment { get; set; }
        public string LeaveComment { get; set; }
        public string StudyCommentAM { get; set; }
        public string StudyCommentPM { get; set; }
        public string SleepComment { get; set; }
        public string HygieneComment { get; set; }
        public string OverallComment { get; set; }
        public string DayComment { get; set; }
        public string PhieuBeNgoan { get; set; }
        public string WeekComment { get; set; }
        public string WeekPhieuBeNgoan { get; set; }
        public Nullable<System.Guid> WeekCommentCate { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public Nullable<System.Guid> DiemKiemTraID { get; set; }
        public Nullable<double> Diem1 { get; set; }
        public Nullable<double> Diem2 { get; set; }
        public Nullable<double> Diem3 { get; set; }
        public Nullable<double> Diem4 { get; set; }
        public Nullable<double> DiemTB { get; set; }
        public string NhanXetKiemTra { get; set; }
        public Nullable<bool> Approve { get; set; }
        public Nullable<System.Guid> Approver { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ClassName { get; set; }
        public string TenNguoiDon { get; set; }
    }
}

