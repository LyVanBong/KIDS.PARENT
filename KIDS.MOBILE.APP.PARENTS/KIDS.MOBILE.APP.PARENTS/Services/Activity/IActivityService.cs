using KIDS.MOBILE.APP.PARENTS.Models.Activity;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Activity
{
    public interface IActivityService
    {
        Task<ResponseModel<List<DailyActivityResponseModel>>> GetMorningActivity(string studentId, string classId, string date);
        Task<ResponseModel<List<DailyActivityResponseModel>>> GetAfternoonActivity(string studentId, string classId, string date);
        Task<ResponseModel<List<TodayMenuResponseModel>>> GetTodayMenu(string studentId, string gradeId, string date);
        Task<ResponseModel<List<TodaySleepResponseModel>>> GetTodaySleep(string studentId, string gradeId, string date);
        Task<ResponseModel<List<TodayPooResponseModel>>> GetTodayPoo(string studentId, string date);
    }
}
