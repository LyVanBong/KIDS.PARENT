using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Commons
{
    public static class Common
    {
        public static string ConvertDecimalToString(decimal number)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.CurrencyGroupSeparator = ",";
            nfi.CurrencySymbol = "";
            return number.ToString("C3", nfi);
        }
    }
}
