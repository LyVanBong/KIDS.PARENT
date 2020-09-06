using Realms;
using Newtonsoft.Json;

namespace KIDS.MOBILE.APP.PARENTS.Models.Login
{
    public class UserModel : RealmObject
    {
        [PrimaryKey]
        [JsonProperty("NguoiSuDung")]
        public string NguoiSuDung { get; set; }

        [JsonProperty("NickName")]
        public string NickName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("USERID")]
        public string UserId { get; set; }

        [JsonProperty("isTeacher")]
        public string IsTeacher { get; set; }

        [JsonProperty("LoaiGiaoVien")]
        public string LoaiGiaoVien { get; set; }

        [JsonProperty("TenGV")]
        public string TenGV { get; set; }

        [JsonProperty("Picture")]
        public string Picture { get; set; }

        [JsonProperty("DonVi")]
        public string DonVi { get; set; }

        [JsonProperty("ClassID")]
        public string ClassID { get; set; }

        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        [JsonProperty("GradeID")]
        public string GradeID { get; set; }

        [JsonProperty("GradeName")]
        public string GradeName { get; set; }
    }
}
