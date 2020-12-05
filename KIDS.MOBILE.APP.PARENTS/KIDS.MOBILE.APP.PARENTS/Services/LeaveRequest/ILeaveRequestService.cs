using KIDS.MOBILE.APP.PARENTS.Models.LeaveRequest;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services
{
    public interface ILeaveRequestService
    {
        Task<ResponseModel<List<GetAttendanceResponseModel>>> GetAttendanceInformation(string studentId,string classId, string datefrom, string dateTo);
    }
}
