using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.Notification
{
    public interface INotificationService
    {
        /// <summary>
        /// Đếm số thông báo
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        Task<ResponseModel<int>> CountNotification();
        /// <summary>
        /// Lấy danh sách thông báo
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<ResponseModel<IEnumerable<NotificationModel>>> GetNotification(string SchoolId, string studentId);
    }
}