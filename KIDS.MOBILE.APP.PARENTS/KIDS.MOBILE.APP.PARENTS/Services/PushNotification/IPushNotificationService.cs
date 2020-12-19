using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.PushNotification
{
    public interface IPushNotificationService
    {
        /// <summary>
        /// Lay id thiet bi
        /// </summary>
        /// <param name="groupId">
        /// 0 la select theo user, 1 class, 2 school, 3 all
        /// </param>
        /// <param name="id">id tương ứng mới group id ở trên</param>
        /// <returns></returns>
        Task<ResponseModel<string>> GetDeviceId(string groupId, string id);

        /// <summary>
        /// Cập nhật id thiết bị cho user
        /// </summary>
        /// <param name="idSchool"></param>
        /// <param name="idClass"></param>
        /// <param name="idUser"></param>
        /// <param name="idDevice"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        Task<ResponseModel<string>> UpdateDeviceDd(string idSchool, string idClass, string idUser, string idDevice, string note);
        /// <summary>
        /// Gửi thông báo
        /// </summary>
        /// <param name="idDevice"></param>
        /// <param name="contentVi"></param>
        /// <param name="contentEn"></param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="titleVi"></param>
        /// <param name="titleEn"></param>
        /// <returns></returns>
        Task<ResponseModel<string>> SendNotifications(string idDevice, string contentVi, string contentEn, string data1, string data2, string titleVi, string titleEn);
    }
}