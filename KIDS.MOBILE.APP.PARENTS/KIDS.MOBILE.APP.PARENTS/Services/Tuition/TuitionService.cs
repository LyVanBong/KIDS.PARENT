using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Models.Tuition;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Tuition
{
    public class TuitionService : ITuitionService
    {
        private IRequestProvider _requestProvider;
        public TuitionService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<ResponseModel<List<GetCurrentTuitionModel>>> GetCurrentTuition(string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                };
                var data = await _requestProvider.GetAsync<List<GetCurrentTuitionModel>>("Finance/Select/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<GetListHistoryTuitionModel>>> GetListHistoryTuition(string id)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("DotThu_HocSinhID", "ef3a1009-e305-4811-a4e1-727f1924cb63"),
                };
                var data = await _requestProvider.GetAsync<List<GetListHistoryTuitionModel>>("Finance/Select/Detail", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
