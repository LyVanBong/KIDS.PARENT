using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Message
{
    public interface IMessageService
    {
        Task<ResponseModel<List<StudentMessageSentModel>>> GetAllSentMessage(string studentId);
    }
}
