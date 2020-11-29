using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.News
{
    public class NewResponseModel
    {
        public Guid NewsID { get; set; }
        public Guid ClassID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public string URL { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid UserCreate { get; set; }
        public bool Approve { get; set; }
        public string NgayTao { get; set; }
        public string TenNguoiTao { get; set; }
        public string TenLop { get; set; }
        public Guid Approver { get; set; }
        public int Views { get; set; }
        public int Sort { get; set; }
        public bool Ghim { get; set; }
        public Guid SchoolID { get; set; }
    }
}
