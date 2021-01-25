using System;
using System.Collections.Generic;

namespace KIDS.MOBILE.APP.PARENTS.Models.MedicineAdvise
{
    public class PrescriptionModel
    {
        public Guid ID { get; set; }
        public Guid PrescriptionID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string StudentID { get; set; }
        public string ClassID { get; set; }
        public Boolean Status { get; set; }
        public Guid Approver { get; set; }
        public String Description { get; set; }
    }

    public partial class GetPrescriptionModel
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

    public class MedicineTicketModel
    {
        public Guid? Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Guid StudentID { get; set; }
        public Guid ClassID { get; set; }
        public Boolean Status { get; set; }
        public Guid Approver { get; set; }
        public String Description { get; set; }
        public List<MedicineDetailTicketModel> MedicineList { get; set; }
    }

    public class MedicineDetailTicketModel
    {
        public Guid? Id { get; set; }
        public string Picture { get; set; }
        public string Note { get; set; }
        public string Unit { get; set; }
    }
}
