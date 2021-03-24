using System;
namespace KIDS.MOBILE.APP.PARENTS.Models.HealthCare
{
    public class GetStudentHealthModel
    {
        public Nullable<System.Guid> ID { get; set; }
        public Nullable<System.Guid> StudentID { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public Nullable<double> BMI { get; set; }
        public Nullable<int> MonthAge { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<System.Guid> HeightStatus { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<System.Guid> WidthStatus { get; set; }
        public Nullable<System.Guid> UserCreate { get; set; }
    }
}
