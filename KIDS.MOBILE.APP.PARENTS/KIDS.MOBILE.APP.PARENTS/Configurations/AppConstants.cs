using KIDS.MOBILE.APP.PARENTS.Models.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Configurations
{
    public static class AppConstants
    {
        /// <summary>
        /// Id thiet bi dung cho thong bao day onesignal
        /// </summary>
        internal const string PlayerId = "PLAYER _ID";
        /// <summary>
        /// Lưu thông tin user khi login thành công
        /// </summary>
        internal static UserModel User = new UserModel();
        /// <summary>
        /// Uri web form
        /// </summary>
        internal static string UriBaseWebForm = "http://school.hkids.edu.vn";
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
        #if DEBUG
        internal static string UrlApiApp = "http://api.hkids.edu.vn/api/v1/";
        #else
        internal static string UrlApiApp = "http://api.hkids.edu.vn/api/v1/";
        #endif
    }
}
