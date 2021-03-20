using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KIDS.MOBILE.APP.PARENTS.Controls.Style
{
    public class SfDataGridData : DataGridStyle
    {
        public SfDataGridData()
        {
        }
        public override float GetBorderWidth()
        {
            return 5;
        }
        public override float GetHeaderBorderWidth()
        {
            return 5;
        }

        public override Color GetHeaderBackgroundColor()
        {
            return Color.FromRgb(100, 204, 245);
        }

        public override Color GetHeaderForegroundColor()
        {
            return Color.White;
        }

        public override Color GetRecordBackgroundColor()
        {
            return Color.White;
        }

        public override Color GetRecordForegroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetSelectionBackgroundColor()
        {
            return Color.Transparent;
        }

        public override Color GetSelectionForegroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetCaptionSummaryRowBackgroundColor()
        {
            return Color.FromRgb(02, 02, 02);
        }

        public override Color GetCaptionSummaryRowForeGroundColor()
        {
            return Color.FromRgb(255, 255, 255);
        }

        public override Color GetBorderColor()
        {
            return Color.FromRgb(29, 242, 29);
        }

        public override Color GetLoadMoreViewBackgroundColor()
        {
            return Color.FromRgb(242, 242, 242);
        }

        public override Color GetLoadMoreViewForegroundColor()
        {
            return Color.FromRgb(34, 31, 31);
        }

        public override Color GetAlternatingRowBackgroundColor()
        {
            return Color.White;
        }

        public override GridLinesVisibility GetGridLinesVisibility()
        {
            return GridLinesVisibility.Both;
        }
        
    }
}
