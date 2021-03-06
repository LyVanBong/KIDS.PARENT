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

        public async Task<ResponseModel<List<StudentMessageSentModel>>> GetAllSentMessage(string studentId, string classId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("ClassID", classId)
                };
                var data = await _requestProvider.GetAsync<List<StudentMessageSentModel>>("Communication/Select/TeacherToStudent", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<DetailMessageModel>>> GetCommentOnMessage(string parentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("Parent", parentId)
                };
                var data = await _requestProvider.GetAsync<List<DetailMessageModel>>("Communication/Select/Reply", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> CreateMessage(CreateMessageModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ClassID", model.ClassID),
                    new RequestParameter("TeacherID", model.TeacherID),
                    new RequestParameter("Parent", model.Parent),
                    new RequestParameter("Content", model.Content),
                    new RequestParameter("DateCreate", model.DateCreate.ToString("yyyy/MM/dd hh:mm")),
                    new RequestParameter("StudentID", model.StudentID),
                    new RequestParameter("Type", model.Type.ToString()),
                };
                var data = await _requestProvider.PostAsync<int>("Communication/Insert", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> UpdateMessage(CreateMessageModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("CommunicationID", model.CommunicationID),
                    new RequestParameter("Content", model.Content),
                    new RequestParameter("DateCreate", model.DateCreate.ToString("DateCreate")),
                    new RequestParameter("StudentID", model.StudentID)
                };
                var data = await _requestProvider.PostAsync<int>("Communication/Update", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
