using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.Notification
{
    public interface INotificationService
    {
        Task<ResponseModel<IEnumerable<NotificationModel>>> GetNotification(string classId, string schoolId, string studentId);
    }
}