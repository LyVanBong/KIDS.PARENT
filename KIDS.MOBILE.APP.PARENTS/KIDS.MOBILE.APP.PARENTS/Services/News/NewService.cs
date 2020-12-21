using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.News;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.News
{
    public class NewService : INewService
    {
        private IRequestProvider _requestProvider;
        public NewService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<NewResponseModel>>> GetAllNews(string schoolId, string parentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ParentID", parentId),
                    new RequestParameter("SchoolId", schoolId),
                };
                var data = await _requestProvider.GetAsync<List<NewResponseModel>>("News/SelectParent", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<GetNewsForParentModel>>> GetNewsForParent(string parentId, string classId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ClassID", classId),
                    new RequestParameter("ParentID", parentId),
                };
                var data = await _requestProvider.GetAsync<List<GetNewsForParentModel>>("News/SelectParent", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
