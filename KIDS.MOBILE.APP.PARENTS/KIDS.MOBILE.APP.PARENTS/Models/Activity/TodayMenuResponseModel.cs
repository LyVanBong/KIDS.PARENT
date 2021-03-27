using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Models.Activity
{
    public class TodayMenuResponseModel
    {
        public Nullable<System.Guid> ID { get; set; }
        public string MonAn { get; set; }
        public string BuaAn { get; set; }
        public Nullable<int> Sort { get; set; }
        public string MealComment { get; set; }
        public string TenMonAn { get; set; }
    }
}
