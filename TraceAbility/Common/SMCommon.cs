using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

//namespace TestABC.Common
//{
//    public static class SMCommon
//    {
//        public static bool ConvertToBoolean(string value)
//        {
//            bool result = false;
//            Boolean.TryParse(value,out result);
//            return result;
//        }
//        public static string MD5Endcoding(string password)
//        {
//            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", string.Empty);
//        }
//        //public static bool isNumeric(string value)
//        //{
//        ////    bool result = false;
//        ////    Boolean.TryParse(value, out result);
//        ////    return result;
//        //}
//    }
//}