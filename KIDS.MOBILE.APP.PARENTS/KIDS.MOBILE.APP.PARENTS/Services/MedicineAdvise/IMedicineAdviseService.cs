using KIDS.MOBILE.APP.PARENTS.Models.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.MedicineAdvise
{
    public interface IMedicineAdviseService
    {
        Task<ResponseModel<List<GetPrescriptionModel>>> GetAllMedicineAdvise(string studentId);
        Task<ResponseModel<int>> CreateMessage(MedicineTicketModel model);
        Task<ResponseModel<int>> UpdateMessage(PrescriptionModel model);
        Task<ResponseModel<int>> DeleteMessage(PrescriptionModel model);
        Task<MedicineTicketModel> GetMedicineAdviseDetail(Guid id);
    }
}
