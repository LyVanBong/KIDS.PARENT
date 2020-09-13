using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;

namespace KIDS.MOBILE.APP.PARENTS.Services.Notification
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationModel>> GetNotification();
    }
}