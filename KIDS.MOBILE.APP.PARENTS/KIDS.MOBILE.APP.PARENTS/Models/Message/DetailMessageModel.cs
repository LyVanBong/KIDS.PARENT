using System;

using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Models.Message
{
    public class DetailMessageModel
    {
        public System.Guid CommunicationID { get; set; }
        public Nullable<System.Guid> Parent { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public string StudentID { get; set; }
        public Nullable<System.Guid> TeacherID { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.Guid> Approver { get; set; }
        public Nullable<System.DateTime> ApproverDate { get; set; }
        public string Content { get; set; }
        public Nullable<int> Views { get; set; }
        public string NguoiGui { get; set; }
        public string Picture { get; set; }
    }
}

