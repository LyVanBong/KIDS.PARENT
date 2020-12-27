using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Album;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;

namespace KIDS.MOBILE.APP.PARENTS.Services.Album
{
    public class AlbumService : IAlbumService
    {
        private IRequestProvider _requestProvider;
        public AlbumService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<GetAlbumModel>>> GetAlbumListByClass(string parentId, string schoolId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ParentId", parentId),
                    new RequestParameter("SchoolId", schoolId)
                };
                var data = await _requestProvider.GetAsync<List<GetAlbumModel>>("Album/SelectParent", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<GetDetailAlbumModel>>> GetAlbumDetail(string albumId, string parentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("AlbumID", albumId),
                    new RequestParameter("GiaoVien_PhuHuynhClick", parentId)
                };
                var data = await _requestProvider.GetAsync<List<GetDetailAlbumModel>>("Album/Detail", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
