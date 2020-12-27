using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Message
{
    public class StudentMessageSentModel
    {
        public System.Guid CommunicationID { get; set; }
        public string StudentID { get; set; }
        public Nullable<System.Guid> TeacherID { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> Type { get; set; }
        public string Content { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public string NguoiGui { get; set; }
        public string Picture { get; set; }
        public string Approver { get; set; }
        public Nullable<System.DateTime> ApproverDate { get; set; }
    }
}
