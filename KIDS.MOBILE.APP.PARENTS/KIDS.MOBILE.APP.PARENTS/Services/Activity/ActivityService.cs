using KIDS.MOBILE.APP.PARENTS.Models.Activity;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private IRequestProvider _requestProvider;
        public ActivityService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<DailyActivityResponseModel>>> GetMorningActivity(string studentId, string classId, string date)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("Date", date),
                    new RequestParameter("ClassID", classId),
                };
                var data = await _requestProvider.GetAsync<List<DailyActivityResponseModel>>("Daily/Morning/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<DailyActivityResponseModel>>> GetAfternoonActivity(string studentId, string classId, string date)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("Date", date),
                    new RequestParameter("ClassID", classId),
                };
                var data = await _requestProvider.GetAsync<List<DailyActivityResponseModel>>("Daily/Afternoon/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<TodayMenuResponseModel>>> GetTodayMenu(string studentId, string gradeId, string date)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("Date", date),
                    new RequestParameter("Grade", gradeId),
                };
                var data = await _requestProvider.GetAsync<List<TodayMenuResponseModel>>("Daily/SelectMeal/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<TodayPooResponseModel>>> GetTodayPoo(string studentId, string date)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("Date", date)
                };
                var data = await _requestProvider.GetAsync<List<TodayPooResponseModel>>("Daily/SelectHygiene/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<TodaySleepResponseModel>>> GetTodaySleep(string studentId, string gradeId, string date)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId),
                    new RequestParameter("Date", date),
                    new RequestParameter("Grade", gradeId),
                };
                var data = await _requestProvider.GetAsync<List<TodaySleepResponseModel>>("Daily/SelectSleep/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
