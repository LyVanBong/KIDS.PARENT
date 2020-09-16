using Realms;
using Newtonsoft.Json;

namespace KIDS.MOBILE.APP.PARENTS.Models.Login
{
    public class UserModel : RealmObject
    {
        [PrimaryKey]
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("DonVi")]
        public string DonVi { get; set; }

        [JsonProperty("NguoiSuDung")]
        public string NguoiSuDung { get; set; }

        [JsonProperty("NickName")]
        public string NickName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Role")]
        public string Role { get; set; }

        [JsonProperty("StudentID")]
        public string StudentID { get; set; }

        [JsonProperty("ParentID")]
        public string ParentID { get; set; }

        [JsonProperty("ParentName")]
        public string ParentName { get; set; }

        [JsonProperty("StudentName")]
        public string StudentName { get; set; }

        [JsonProperty("ClassID")]
        public string ClassID { get; set; }

        [JsonProperty("GradeID")]
        public string GradeID { get; set; }
    }


}
