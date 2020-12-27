using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<ResponseModel<T>> GetAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters = null);
        Task<ResponseModel<T>> PostAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters = null);

        Task<ResponseModel<T>> PutAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters = null);

        Task<ResponseModel<T>> DeleteAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters = null);
        Task<ResponseModel<T>> PostAsync<T>(string uri, IReadOnlyCollection<RequestParameter> parameters, Dictionary<string, string> files = null);
    }
}
