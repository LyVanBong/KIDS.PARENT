using KIDS.MOBILE.APP.PARENTS.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Configurations
{
    public static class AppConstants
    {
        /// <summary>
        /// Lưu thông tin user khi login thành công
        /// </summary>
        internal static UserModel User = new UserModel();
        /// <summary>
        /// Uri web form
        /// </summary>
        internal static string UriBaseWebForm = "http://kids.ntdauto.com";
        /// <summary>
        /// Kiểm tra người dùng có lưu tài khoản hay không
        /// </summary>
        internal static string SaveAccount = "SaveAccount";
        /// <summary>
        /// Dữ liệu người dùng sau khi login thành công
        /// </summary>
        internal static string UserLogin = "UserLogin";
        /// <summary>
        /// Config realm db
        /// </summary>
        internal static string RealmConfiguration = "KIDS.MOBILE.APP.PARENTS.BSOFTGROUP.realm";
        /// <summary>
        /// Base Url API
        /// </summary>
        internal static string UrlApiApp = "http://webapi.ntdauto.com/api/v1/";
    }
}
