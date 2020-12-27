using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest
{
    public class GetListLeaveRequestModel
    {
        public Nullable<System.Guid> ID { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public Nullable<System.Guid> StudentID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.Guid> Approver { get; set; }
        public Nullable<System.DateTime> DateApprove { get; set; }
        public string Description { get; set; }
        public string NguoiGui { get; set; }
        public string Picture { get; set; }
        public string StatusName { get; set; }
    }
}
