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
        public string NhanXet { get; set; } = null;
        public string Title { get; set; }
    }
}
