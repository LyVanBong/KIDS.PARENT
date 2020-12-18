using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Message
{
    public interface IMessageService
    {
        Task<ResponseModel<List<StudentMessageSentModel>>> GetAllSentMessage(string studentId);
        Task<ResponseModel<int>> CreateMessage(CreateMessageModel model);
        Task<ResponseModel<int>> UpdateMessage(CreateMessageModel model);
        Task<ResponseModel<List<DetailMessageModel>>> GetCommentOnMessage(string parentId);
    }
}
