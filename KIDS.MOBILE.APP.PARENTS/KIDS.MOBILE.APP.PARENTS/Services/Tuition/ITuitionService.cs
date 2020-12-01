using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Models.Tuition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Tuition
{
    public interface ITuitionService
    {
        Task<ResponseModel<List<GetCurrentTuitionModel>>> GetCurrentTuition(string studentId);
        Task<ResponseModel<List<GetListHistoryTuitionModel>>> GetListHistoryTuition(string studentId);
    }
}
