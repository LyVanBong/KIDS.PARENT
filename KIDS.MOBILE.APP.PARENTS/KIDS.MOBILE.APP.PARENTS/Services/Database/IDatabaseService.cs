using KIDS.MOBILE.APP.PARENTS.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIDS.MOBILE.APP.PARENTS.Services.Database
{
    public interface IDatabaseService
    {
        Task<bool> SetAccount(UserModel user);
        Task<UserModel> GetAccount();
        Task<bool> DeleteAccount();
        Task<bool> UpdateAccount(UserModel user);
    }
}
