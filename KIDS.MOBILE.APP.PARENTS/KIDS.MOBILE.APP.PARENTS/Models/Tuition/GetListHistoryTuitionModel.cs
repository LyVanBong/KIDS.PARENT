using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Tuition
{
    public class GetListHistoryTuitionModel
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> DotThu_HocSinhID { get; set; }
        public System.Guid KhoanThu { get; set; }
        public Nullable<int> SoBuoi { get; set; }
        public Nullable<decimal> MucThu { get; set; }
        public Nullable<decimal> MienGiam { get; set; }
        public string SoTienDisplay { get => string.Format("{0:n}", SoTien ?? 0); }
        public Nullable<decimal> SoTien { get; set; }
        public string MienGiamDisplay { get => string.Format("{0:n}", MienGiam ?? 0); }
        public Nullable<System.DateTime> Ngay { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> UserCreate { get; set; }
        public int Sort { get; set; }
        public string TenKhoanThu { get; set; }
    }
}
