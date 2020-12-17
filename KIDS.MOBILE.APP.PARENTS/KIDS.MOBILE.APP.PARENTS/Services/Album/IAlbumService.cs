using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Album;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.Album
{
    public interface IAlbumService
    {
        Task<ResponseModel<List<GetAlbumModel>>> GetAlbumListByClass(string classId, string schoolId);
        Task<ResponseModel<List<GetDetailAlbumModel>>> GetAlbumDetail(string albumId, string parentId);
    }
}
