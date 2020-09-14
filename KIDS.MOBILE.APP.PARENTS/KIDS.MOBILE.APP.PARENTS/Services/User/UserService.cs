using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.User
{
    public class UserService:IUserService
    {
        private IRequestProvider _requestProvider;
        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<ResponseModel<IEnumerable<StudentModel>>> GetStudent(string studentID)
        {
            try
            {
                var parameters = new List<RequestParameter>
                {
                    new RequestParameter("StudentID",studentID),
                };
                var data = await _requestProvider.GetAsync<IEnumerable<StudentModel>>("Student/Profile", parameters);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel<IEnumerable<ParentModel>>> GetParentOfStudent(string studentId)
        {
            try
            {
                var parameters = new List<RequestParameter>
                {
                    new RequestParameter("StudentID",studentId),
                };
                var data = await _requestProvider.GetAsync<IEnumerable<ParentModel>>("Student/Select/Parent", parameters);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<IEnumerable<TeacherModel>>> GetTeacher(string teacherid)
        {
            try
            {
                var parameters = new List<RequestParameter>
                {
                    new RequestParameter("teacherid",teacherid),
                };
                var data = await _requestProvider.GetAsync<IEnumerable<TeacherModel>>("Teacher/Profile", parameters);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel<int>> UpdateUser(string user,string pass)
        {
            try
            {
                var parameters = new List<RequestParameter>
                {
                    new RequestParameter("UserName",user),
                    new RequestParameter("PassWord",pass)
                };
                var data = await _requestProvider.PostAsync<int>("ChangePassWord", parameters);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
