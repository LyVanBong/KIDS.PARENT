using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Activity
{
    public class DailyActivityResponseModel
    {
        public Nullable<System.Guid> ID { get; set; }
        public string Contents { get; set; }
        public string ThoiGian { get; set; }
        public string Title { get; set; }
        public string TitleStudy { get; set; }
        public string NhanXetHoc { get; set; }
        public string NhanXetAn { get; set; }
        public string NhanXetNgu { get; set; }
        public string NhanXetWC { get; set; }
        public string NhanXetNgay { get; set; }
        public string PhieuBeNgoanNgay { get; set; }
        public string NhanXetTuan { get; set; }
        public string PhieuBeNgoanTuan { get; set; }
    }
}
