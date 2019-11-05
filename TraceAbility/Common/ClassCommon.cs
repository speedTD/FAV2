using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace TestABC.Models.Data
{

    public static class SMCommon
    {
        //value permision.
        public static string View = "View";
        public static string Add = "Add";
        public static string Edit = "Edit";
        public static string Delete = "Delete";
        public static string Check = "Check";

        //report.
        public static Color ReportMachineMtn_Result_OK_Color = Color.FromArgb(255, 242, 204);
        public static Color ReportMachineMtn_Result_NG_Color = Color.Red;
        
        //Machine Mtn
        public static string MachineMtnResult_OK = "OK";
        public static string MachineMtnResult_NG = "NG";
        
        //FrequencyID for Machine Maintenance.
        public static int MachineMtnFrequency_Daily = 1;
        public static int MachineMtnFrequency_Weekly = 2;
        public static int MachineMtnFrequency_Monthly = 3;
        public static int MachineMtnFrequency_3Months = 4;
        public static int MachineMtnFrequency_6Months = 5;
        public static int MachineMtnFrequency_Yearly = 6;
        

        public static bool CheckUserPermission(string controllerName, string checkPermission, ReturnUserPermission userPermission)
        {
            if (userPermission == null)
                return false;
            UserPermission x = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(checkPermission)));
            if (x != null)
                return true;
            return false;
        }
        public static PermisionControllerVM getPermisionControllerViewModel(string controllerName, ReturnUserPermission userPermission)
        {
            PermisionControllerVM permisionControllerViewModel = new PermisionControllerVM();
            permisionControllerViewModel.Code = "00";
            permisionControllerViewModel.isAllow_View = false;
            permisionControllerViewModel.isAllow_Add = false;
            permisionControllerViewModel.isAllow_Edit = false;
            permisionControllerViewModel.isAllow_Delete = false;
            permisionControllerViewModel.isAllow_Check = false;
            if (userPermission == null)
                return permisionControllerViewModel;

            try
            {
                permisionControllerViewModel.isAllow_View = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(SMCommon.View))) != null ? true : false;
                permisionControllerViewModel.isAllow_Add = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(SMCommon.Add))) != null ? true : false;
                permisionControllerViewModel.isAllow_Edit = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(SMCommon.Edit))) != null ? true : false;
                permisionControllerViewModel.isAllow_Delete = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(SMCommon.Delete))) != null ? true : false;
                permisionControllerViewModel.isAllow_Check = userPermission.lstUserPermission.Find(o => (o.ControllerName == controllerName) && (o.Permission.Contains(SMCommon.Check))) != null ? true : false;
            }
            catch (Exception ex)
            {
                permisionControllerViewModel.Code = "99";
                permisionControllerViewModel.Message = "Lỗi kiểm tra quyền/Error while checking permission! : " + ex.ToString();
                permisionControllerViewModel.isAllow_View = false;
                permisionControllerViewModel.isAllow_Add = false;
                permisionControllerViewModel.isAllow_Edit = false;
                permisionControllerViewModel.isAllow_Delete = false;
                permisionControllerViewModel.isAllow_Check = false;
            }
            return permisionControllerViewModel;
        }

        public static double MeasureTextHeight(string text, ExcelFont font, int width)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return 0.0;
                var bitmap = new Bitmap(1, 1);
                var graphics = Graphics.FromImage(bitmap);

                var pixelWidth = Convert.ToInt32(width / 6.5);  //7.5 pixels per excel column width
                var drawingFont = new Font(font.Name, font.Size);
                var size = graphics.MeasureString(text, drawingFont, pixelWidth);

                //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
                var sizeHeight = Convert.ToDouble(size.Height);
                return Math.Min(sizeHeight * 72 / 96, 409);
            }
            catch (Exception ex)
            {
                return 0.0;
            }
        }
        public static bool ConvertToBoolean(string value)
        {
            bool result = false;
            Boolean.TryParse(value, out result);
            return result;
        }
        public static string MD5Endcoding(string password)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", string.Empty);
        }

    }
}