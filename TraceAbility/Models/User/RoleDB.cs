using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestABC.Common;

using log4net;

namespace TestABC.Models.Data
{
    public class RoleDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(RoleDB));
        public ReturnRole RoleAll()
        {
            List<Role> lstRole = null;
            Role role = null;
            ReturnRole returnRole = new ReturnRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tRole_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstRole = new List<Role>();
                                while (sqlDr.Read())
                                {
                                    role = new Role();
                                    role.ID = int.Parse(sqlDr["ID"].ToString());
                                    role.RoleName = sqlDr["RoleName"].ToString();
                                    role.Title = sqlDr["Title"].ToString();

                                    lstRole.Add(role);
                                }
                                returnRole.Code = "00";
                                returnRole.Message = "Lấy dữ liệu thành công.";
                                returnRole.lstRole = lstRole;
                            }
                            else
                            {
                                returnRole.Code = "01";
                                returnRole.Message = "Không tồn tại bản ghi nào.";
                                returnRole.Total = 0;
                                returnRole.lstRole = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnRole.Code = "99";
                returnRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnRole.Total = 0;
                returnRole.lstRole = null;
                mylog4net.Error("", ex);
            }
            return returnRole;
        }
        public ReturnRole ListByUserID(int userID)
        {
            List<Role> lstRole = null;
            Role role = null;
            ReturnRole returnRole = new ReturnRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "[sp_tRole_SelectByUserID]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstRole = new List<Role>();
                                while (sqlDr.Read())
                                {
                                    role = new Role();
                                    role.ID = int.Parse(sqlDr["ID"].ToString());
                                    role.RoleName = sqlDr["RoleName"].ToString();
                                    role.Title = sqlDr["Title"].ToString();

                                    lstRole.Add(role);
                                }
                                returnRole.Code = "00";
                                returnRole.Message = "Lấy dữ liệu thành công.";
                                returnRole.lstRole = lstRole;
                            }
                            else
                            {
                                returnRole.Code = "01";
                                returnRole.Message = "Không tồn tại bản ghi nào.";
                                returnRole.Total = 0;
                                returnRole.lstRole = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnRole.Code = "99";
                returnRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnRole.Total = 0;
                returnRole.lstRole = null;
                mylog4net.Error("", ex);
            }
            return returnRole;
        }
        public ReturnRole GetbyID(int ID)
        {
            List<Role> lstRole = null;
            Role role = null;
            ReturnRole returnRole = new ReturnRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tRole_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstRole = new List<Role>();
                                while (sqlDr.Read())
                                {
                                    role = new Role();
                                    role.ID = int.Parse(sqlDr["ID"].ToString());
                                    role.RoleName = sqlDr["RoleName"].ToString();
                                    role.Title = sqlDr["Title"].ToString();

                                    lstRole.Add(role);
                                }
                                returnRole.Code = "00";
                                returnRole.Message = "Lấy dữ liệu thành công.";
                                returnRole.lstRole = lstRole;
                            }
                            else
                            {
                                returnRole.Code = "01";
                                returnRole.Message = "Không tồn tại bản ghi nào.";
                                returnRole.Total = 0;
                                returnRole.lstRole = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnRole.Code = "99";
                returnRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnRole.Total = 0;
                returnRole.lstRole = null;
                mylog4net.Error("", ex);
            }
            return returnRole;
        }

        public ReturnRole Insert(Role role)
        {
            ReturnRole returnRole = new ReturnRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tRole_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = role.ID;
                        cmd.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar)).Value = role.RoleName;
                        cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar)).Value = role.Title;
                        cmd.ExecuteNonQuery();

                        returnRole.Code = "00";
                        returnRole.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnRole.Code = "99";
                returnRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnRole.Total = 0;
                returnRole.lstRole = null;
            }
            return returnRole;
        }
        public ReturnRole DeleteByID(int _ID)
        {
            ReturnRole returnRole = new ReturnRole();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tRole_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnRole.Code = "00";
                        returnRole.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnRole.Code = "99";
                returnRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnRole.Total = 0;
                returnRole.lstRole = null;
                mylog4net.Error("", ex);
            }
            return returnRole;
        }

        public ReturnMenuRole SaveRoleWithPermissionMenus(MenuRole menuRole)
        {
            ReturnMenuRole returnMenuRole = new ReturnMenuRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tRoleMenu_SaveRoleWithPermissionMenus";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RoleID", menuRole.RoleID);
                        cmd.Parameters.AddWithValue("@MenuID", menuRole.ID);
                        cmd.Parameters.AddWithValue("@Permission", menuRole.Permission != null ? menuRole.Permission : "");
                        cmd.ExecuteNonQuery();
                        returnMenuRole.Code = "00";
                        returnMenuRole.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenuRole.Code = "99";
                returnMenuRole.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenuRole.Total = 0;
                returnMenuRole.lstMenuRole = null;
                mylog4net.Error("public ReturnUserWithRoles SaveUserWith_aRole(int userID, Role role) ", ex);
            }
            return returnMenuRole;
        }



    }
}