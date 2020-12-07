using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest
{
    public class CreateLeaveRequestModel
    {
        public Guid ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string StudentID { get; set; }
        public string ClassID { get; set; }
        public bool Status { get; set; }
        public Guid Approver { get; set; }
        public string Description { get; set; }
    }
}
