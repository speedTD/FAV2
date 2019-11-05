using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data;
using System.Data.SqlClient;
using TestABC.Common;

namespace TestABC.Models.Data
{
    public class UserRoleDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(UserDB));
        public ReturnUserWithRoles SaveUserWith_aRole(int userID, Role role)
        {
            ReturnUserWithRoles ReturnUserWithRoles = new ReturnUserWithRoles();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserRole_SaveUserRoleByChecked";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
                        //cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.NVarChar)).Value = user.FullName;
                        //cmd.Parameters.Add(new SqlParameter("@Checked", SqlDbType.Bit)).Value = user.UserName;
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@RoleID", role.ID);
                        cmd.Parameters.AddWithValue("@Checked", role.Checked);
                        cmd.ExecuteNonQuery();
                        ReturnUserWithRoles.Code = "00";
                        ReturnUserWithRoles.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnUserWithRoles.Code = "99";
                ReturnUserWithRoles.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                ReturnUserWithRoles.Total = 0;
                ReturnUserWithRoles.lstUserWithRoles = null;
                mylog4net.Error("public ReturnUserWithRoles SaveUserWith_aRole(int userID, Role role) ", ex);
            }
            return ReturnUserWithRoles;
        }

    }
}