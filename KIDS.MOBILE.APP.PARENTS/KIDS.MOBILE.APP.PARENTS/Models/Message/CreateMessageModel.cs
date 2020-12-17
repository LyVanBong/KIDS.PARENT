using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Message
{
    public class CreateMessageModel
    {
        public string CommunicationID { get; set; }
        public string ClassID { get; set; }
        public string TeacherID { get; set; }
        public int Type { get; set; }
        public string Parent { get; set; }
        public String Content { get; set; }
        public DateTime DateCreate { get; set; }
        public String StudentID { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid Approver { get; set; }
        public DateTime ApproverDate { get; set; } = DateTime.Now;
        public int Views { get; set; }
        public string Image { get; set; }
    }
}
