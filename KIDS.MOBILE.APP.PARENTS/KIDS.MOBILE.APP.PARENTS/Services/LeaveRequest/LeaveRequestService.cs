using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.LeaveRequest
{
    public class LeaveRequestService
    {
        private IRequestProvider _requestProvider;
        public LeaveRequestService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<GetAttendanceResponseModel>>> GetAttendanceInformation(string studentId, string classId, string datefrom, string dateTo)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ClassId", classId),
                    new RequestParameter("StudentId", studentId),
                    new RequestParameter("FromDate", datefrom),
                    new RequestParameter("ToDate", dateTo),
                };
                var data = await _requestProvider.GetAsync<List<GetAttendanceResponseModel>>("Attendance/StudentCount", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
