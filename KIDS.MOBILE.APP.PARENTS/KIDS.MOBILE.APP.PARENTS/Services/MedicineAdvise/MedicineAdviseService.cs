using KIDS.MOBILE.APP.PARENTS.Models.MedicineAdvise;
using KIDS.MOBILE.APP.PARENTS.Models.Message;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.MedicineAdvise
{
    public class MedicineAdviseService : IMedicineAdviseService
    {
        private IRequestProvider _requestProvider;
        public MedicineAdviseService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ResponseModel<List<GetPrescriptionModel>>> GetAllMedicineAdvise(string studentId)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("StudentID", studentId)
                };
                var data = await _requestProvider.GetAsync<List<GetPrescriptionModel>>("Prescription/Select/Student", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MedicineTicketModel> GetMedicineAdviseDetail(Guid id)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("id", id.ToString())
                };
                var data = await _requestProvider.GetAsync<MedicineTicketModel>("Prescription/PrescriptionDetail", para);
                return data?.Data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> CreatePrescription(MedicineTicketModel model)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                Console.WriteLine("Test : " + jsonBody);
                var data = await _requestProvider.PostJsonBodyAsync<int>("Prescription/Create", jsonBody);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> UpdatePrescription(MedicineTicketModel model)
        {
            try
            {
                var jsonBody = JsonConvert.SerializeObject(model);
                var data = await _requestProvider.PostJsonBodyAsync<int>("Prescription/Update", jsonBody);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<int>> DeletePrescription(PrescriptionModel model)
        {
            try
            {
                var para = new List<RequestParameter>()
                {
                    new RequestParameter("ID", model.ID.ToString())
                };
                var data = await _requestProvider.PostAsync<int>("Prescription/Delete", para);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
