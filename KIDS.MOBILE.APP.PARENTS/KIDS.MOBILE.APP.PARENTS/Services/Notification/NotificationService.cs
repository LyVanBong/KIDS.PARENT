using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Notification;

namespace KIDS.MOBILE.APP.PARENTS.Services.Notification
{
    public class NotificationService : INotificationService
    {
        public Task<IEnumerable<NotificationModel>> GetNotification()
        {
            throw new System.NotImplementedException();
        }
    }
}