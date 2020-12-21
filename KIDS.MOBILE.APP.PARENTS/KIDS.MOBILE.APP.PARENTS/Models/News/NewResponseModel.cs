using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.News
{
    public class NewResponseModel
    {
        public System.Guid NewsID { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.Guid> UserCreate { get; set; }
        public Nullable<bool> Approve { get; set; }
        public Nullable<int> Views { get; set; }
        public string NgayTao { get; set; }
        public string TenNguoiTao { get; set; }
        public string TenLop { get; set; }
        public Nullable<System.Guid> Approver { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<bool> Ghim { get; set; }
        public Nullable<System.Guid> SchoolID { get; set; }
    }
}
