using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KIDS.MOBILE.APP.PARENTS.Helpers
{
    public class HashFunctionHelper
    {
        private static HashAlgorithm[] _hash = { new MD5CryptoServiceProvider(), new SHA1CryptoServiceProvider(), new SHA256CryptoServiceProvider(), new SHA512CryptoServiceProvider() };
        /// <summary>
        /// Hàm chuyển đổi chuỗi xang mã băm
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string GetHashCode(string input, int hash)
        {
            var str = Encoding.UTF8.GetBytes(input);
            var data = _hash[hash].ComputeHash(str);
            var output = string.Empty;
            for (int i = 0; i< data.Length; ++i)
            {
                output += data[i].ToString("X2");
            }
            return output;
        }
    }
}
