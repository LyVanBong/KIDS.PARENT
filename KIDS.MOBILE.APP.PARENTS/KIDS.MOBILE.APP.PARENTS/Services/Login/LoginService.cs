using KIDS.MOBILE.APP.PARENTS.Models.Login;
using KIDS.MOBILE.APP.PARENTS.Models.RequestProvider;
using KIDS.MOBILE.APP.PARENTS.Models.Response;
using KIDS.MOBILE.APP.PARENTS.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Login
{
    public class LoginService : ILoginService
    {
        private IRequestProvider _requestProvider;
        public LoginService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<ResponseModel<UserModel>> LoginAppByUserPwd(string user,string pass)
        {
            try
            {
                var parameters = new List<RequestParameter>
                {
                    new RequestParameter("UserName",user),
                    new RequestParameter("Password",pass)
                };
                var data = await _requestProvider.PostAsync<UserModel>("login", parameters);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
