using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Message
{
    public class MessageService : IMessageService
    {
        private IRequestProvider _requestProvider;
        public MessageService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<StudentMessageSentModel>>> GetAllSentMessage(string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId)
                };
                var data = await _requestProvider.GetAsync<List<StudentMessageSentModel>>("Communication/Select/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
