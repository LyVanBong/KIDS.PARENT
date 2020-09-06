using System.Threading.Tasks;
using KIDS.MOBILE.APP.PARENTS.Models.Login;
using KIDS.MOBILE.APP.PARENTS.Models.Response;

namespace KIDS.MOBILE.APP.PARENTS.Services.Login
{
    public interface ILoginService 
    {
        Task<ResponseModel<UserModel>> LoginAppByUserPwd(string user,string pass);
    }
}
