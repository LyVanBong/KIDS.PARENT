using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;

namespace KIDS.MOBILE.APP.PARENTS.Services.HealthCare
{
    public class HealthCareService : IHealthCareService
    {
        private IRequestProvider _requestProvider;
        public HealthCareService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<GetStudentHealthModel>>> GetStudentHealthInfo(string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId)
                };
                var data = await _requestProvider.GetAsync<List<GetStudentHealthModel>>("Health/Select", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> CreateHealthInformation(CreateHealthInformationModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ID", model.ID.ToString()),
                    new RequestParameter("StudentID", model.StudentID.ToString()),
                    new RequestParameter("ClassID", model.ClassID.ToString()),
                    new RequestParameter("Date", model.Date.ToString("yyyy-MM-dd")),
                    new RequestParameter("MonthAge", model.MonthAge.ToString()),
                    new RequestParameter("Height", model.Height.ToString()),
                    new RequestParameter("Width", model.Width.ToString())
                };
                var data = await _requestProvider.PostAsync<int>("Health/Insert", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
