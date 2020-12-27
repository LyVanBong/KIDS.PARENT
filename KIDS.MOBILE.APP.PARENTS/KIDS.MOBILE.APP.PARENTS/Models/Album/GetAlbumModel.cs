using System;
namespace KIDS.MOBILE.APP.PARENTS.Models.Album
{
    public class GetAlbumModel
    {
        public System.Guid AlbumID { get; set; }
        public Nullable<System.Guid> ClassID { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.Guid> UserCreate { get; set; }
        public Nullable<int> Views { get; set; }
        public string NgayTao { get; set; }
        public string TenNguoiTao { get; set; }
        public string TenLop { get; set; }
        public Nullable<System.Guid> Approver { get; set; }
        public Nullable<bool> Approve { get; set; }
        public Nullable<System.Guid> SchoolID { get; set; }
    }
}
