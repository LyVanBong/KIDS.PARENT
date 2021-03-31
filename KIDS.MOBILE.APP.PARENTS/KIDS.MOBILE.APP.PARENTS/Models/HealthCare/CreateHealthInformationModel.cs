using System;
namespace KIDS.MOBILE.APP.PARENTS.Models.HealthCare
{
    public class CreateHealthInformationModel
    {
        public Guid ID { get; set; }
        public string StudentID { get; set; }
        public string ClassID { get; set; }
        public DateTime Date { get; set; }
        public double MonthAge { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
