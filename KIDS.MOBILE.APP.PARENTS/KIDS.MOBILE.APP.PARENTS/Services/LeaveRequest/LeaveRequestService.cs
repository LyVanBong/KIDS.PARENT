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
    public class LeaveRequestService : ILeaveRequestService
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

        public async Task<ResponseModel<List<GetListLeaveRequestModel>>> GetListLeaveRequest(string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentId", studentId)
                };
                var data = await _requestProvider.GetAsync<List<GetListLeaveRequestModel>>("Application/Select/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> CreateLeaveRequest(CreateLeaveRequestModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("FromDate", model.FromDate.ToShortDateString()),
                    new RequestParameter("ToDate", model.ToDate.ToShortDateString()),
                    new RequestParameter("Date", model.Date.ToShortDateString()),
                    new RequestParameter("Content", model.Content),
                    new RequestParameter("StudentID", model.StudentID),
                    new RequestParameter("ClassID", model.ClassID)
                };
                var data = await _requestProvider.PostAsync<int>("Application/Insert", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> UpdateLeaveRequest(CreateLeaveRequestModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("FromDate", model.FromDate.ToShortDateString()),
                    new RequestParameter("ToDate", model.ToDate.ToShortDateString()),
                    new RequestParameter("Date", model.Date.ToShortDateString()),
                    new RequestParameter("Content", model.Content),
                    new RequestParameter("ID", model.ID.ToString())
                };
                var data = await _requestProvider.PostAsync<int>("Application/Update", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> DeleteLeaveRequest(CreateLeaveRequestModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ID", model.ID.ToString())
                };
                var data = await _requestProvider.PostAsync<int>("Application/Delete", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
