using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.HealthCare;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.HealthCare
{
    public interface IHealthCareService
    {
        Task<ResponseModel<List<GetStudentHealthModel>>> GetStudentHealthInfo(string studentId);
        Task<ResponseModel<int>> CreateHealthInformation(CreateHealthInformationModel model); 
    }
}
