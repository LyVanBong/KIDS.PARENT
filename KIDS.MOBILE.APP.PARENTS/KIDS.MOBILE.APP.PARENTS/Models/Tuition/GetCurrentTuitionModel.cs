using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Tuition
{
    public class GetCurrentTuitionModel
    {
        public Guid ID { get; set; }
        public DateTime? ThoiGianThu { get; set; }
        public DateTime? DenNgay { get; set; }
        public string TenDotThu { get; set; }
        public decimal? TongCong { get; set; }
        public string TrangThai { get; set; }
    }
}
