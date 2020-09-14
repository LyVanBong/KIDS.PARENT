using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Lấy thông tin học sinh
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<ResponseModel<IEnumerable<StudentModel>>> GetStudent(string studentId);
        /// <summary>
        /// Lấy thông tin phụ huynh của học sinh
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<ResponseModel<IEnumerable<ParentModel>>> GetParentOfStudent(string studentId);
        /// <summary>
        /// lấy thông tin giáo viên
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        Task<ResponseModel<IEnumerable<TeacherModel>>> GetTeacher(string teacherid);
        /// <summary>
        /// Cập nhật mật khẩu của User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        Task<ResponseModel<int>> UpdateUser(string user, string pass);
    }
}
