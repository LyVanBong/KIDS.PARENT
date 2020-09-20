using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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