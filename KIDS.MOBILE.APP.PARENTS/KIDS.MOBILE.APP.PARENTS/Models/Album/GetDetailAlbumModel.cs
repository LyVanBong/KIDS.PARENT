using System;
namespace KIDS.MOBILE.APP.PARENTS.Models.Album
{
    public class GetDetailAlbumModel
    {
        public System.Guid ImageID { get; set; }
        public Nullable<System.Guid> AlbumID { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public int Sort { get; set; }
    }
}
