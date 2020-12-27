using KIDS.MOBILE.APP.PARENTS.Models.News;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.News
{
    public interface INewService
    {
        Task<ResponseModel<List<NewResponseModel>>> GetAllNews(string schoolId, string classId);
        Task<ResponseModel<List<GetNewsForParentModel>>> GetNewsForParent(string parentId, string classId);
    }
}
