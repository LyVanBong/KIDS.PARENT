using Realms;
using Newtonsoft.Json;

namespace KIDS.MOBILE.APP.PARENTS.Models.Login
{
    public class UserModel : RealmObject
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string DonVi { get; set; }
        public string NguoiSuDung { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string StudentID { get; set; }
        public string ParentID { get; set; }
        public string ParentName { get; set; }
        public string StudentName { get; set; }
        public string ClassID { get; set; }
        public string GradeID { get; set; }
        public string ClassName { get; set; }
        public string GradeName { get; set; }
        public string Picture { get; set; }
        public string Banner { get; set; }
        public string Banner2 { get; set; }
        public string Banner3 { get; set; }
    }
}
