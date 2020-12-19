using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;

namespace KIDS.MOBILE.APP.PARENTS.Services.PushNotification
{
    public class PushNotificationService : IPushNotificationService
    {
        private IRequestProvider _requestProvider;
        public PushNotificationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<string>> GetDeviceId(string groupId, string id)
        {
            try
            {
                var para = new List<RequestParameter>
                {
                    new RequestParameter("",groupId),
                    new RequestParameter("",id),
                };
                var data = await _requestProvider.PostAsync<string>("PushNotifications/Select", para);
                return data;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<ResponseModel<string>> UpdateDeviceDd(string idSchool, string idClass, string idUser, string idDevice, string note)
        {
            try
            {
                var para = new List<RequestParameter>
                {
                    new RequestParameter("IdSchool",idSchool),
                    new RequestParameter("IdClass",idClass),
                    new RequestParameter("IdUser",idUser),
                    new RequestParameter("IdDevice",idDevice),
                    new RequestParameter("Note",note),
                };
                var data = await _requestProvider.PostAsync<string>("PushNotifications/Update", para);
                return data;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<ResponseModel<string>> SendNotifications(string idDevice, string contentVi, string contentEn, string data1, string data2, string titleVi,
            string titleEn)
        {
            try
            {
                var para = new List<RequestParameter>
                {
                    new RequestParameter("IdDevice",idDevice),
                    new RequestParameter("ContentVi",contentVi),
                    new RequestParameter("ContentEn",contentEn),
                    new RequestParameter("data1",data1),
                    new RequestParameter("data2",data2),
                    new RequestParameter("TitleVi",titleVi),
                    new RequestParameter("TitleEn",titleEn),
                };
                var data = await _requestProvider.PostAsync<string>("PushNotifications/Send", para);
                return data;
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
