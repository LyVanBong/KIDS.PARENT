using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Configurations;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;

namespace KIDS.MOBILE.APP.PARENTS.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private IRequestProvider _requestProvider;
        public NotificationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<int>> CountNotification()
        {
            try
            {
                var user = new { StudentId = AppConstants.User.ParentID, SchoolId = AppConstants.User.DonVi };
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentId",user.StudentId),
                    new RequestParameter("SchoolId",user.SchoolId),
                };
                var data = await _requestProvider.GetAsync<int>("Notification/CountForStudent", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<IEnumerable<NotificationModel>>> GetNotification(string SchoolId, string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("SchoolId",SchoolId),
                    new RequestParameter("StudentID",studentId),
                };
                var data = await _requestProvider.GetAsync<IEnumerable<NotificationModel>>("Notification/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}