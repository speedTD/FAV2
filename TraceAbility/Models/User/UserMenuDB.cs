using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestABC.Common;
using TestABC.Models.Data;

using log4net;
namespace TestABC.Controllers
{
    public class UserMenuDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(UserMenuDB));
        public ReturnUserMenu UserAll()
        {
            List<UserMenu> lstUserMenu = null;
            UserMenu userMenu = null;
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUserMenu = new List<UserMenu>();
                                while (sqlDr.Read())
                                {
                                    userMenu = new UserMenu();
                                    userMenu.ID = int.Parse(sqlDr["ID"].ToString());
                                    userMenu.Pemission = sqlDr["Pemission"].ToString();
                                    userMenu.UserID = int.Parse(sqlDr["UserID"].ToString());
                                    userMenu.MenuID = int.Parse(sqlDr["MenuID"].ToString());

                                    lstUserMenu.Add(userMenu);
                                }
                                returnUserMenu.Code = "00";
                                returnUserMenu.Message = "Lấy dữ liệu thành công.";
                                returnUserMenu.lstUserMenu = lstUserMenu;
                            }
                            else
                            {
                                returnUserMenu.Code = "01";
                                returnUserMenu.Message = "Không tồn tại bản ghi nào.";
                                returnUserMenu.Total = 0;
                                returnUserMenu.lstUserMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }
        public ReturnUserMenu GetByUserID(int UserID)
        {
            List<UserMenu> lstUserMenu = null;
            UserMenu userMenu = null;
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_SelectByUserID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = UserID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUserMenu = new List<UserMenu>();
                                while (sqlDr.Read())
                                {
                                    userMenu = new UserMenu();
                                    userMenu.ID = int.Parse(sqlDr["ID"].ToString());
                                    userMenu.Pemission = sqlDr["Pemission"].ToString();
                                    userMenu.UserID = int.Parse(sqlDr["UserID"].ToString());
                                    userMenu.MenuID = int.Parse(sqlDr["MenuID"].ToString());
                                    userMenu.MenuName = sqlDr["MenuName"].ToString();
                                    lstUserMenu.Add(userMenu);
                                }
                                returnUserMenu.Code = "00";
                                returnUserMenu.Message = "Lấy dữ liệu thành công.";
                                returnUserMenu.lstUserMenu = lstUserMenu;
                            }
                            else
                            {
                                returnUserMenu.Code = "01";
                                returnUserMenu.Message = "Không tồn tại bản ghi nào.";
                                returnUserMenu.Total = 0;
                                returnUserMenu.lstUserMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }
        public ReturnUserMenu GetbyID(int ID)
        {
            List<UserMenu> lstUserMenu = null;
            UserMenu userMenu = null;
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUser_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUserMenu = new List<UserMenu>();
                                while (sqlDr.Read())
                                {
                                    userMenu = new UserMenu();
                                    userMenu.ID = int.Parse(sqlDr["ID"].ToString());
                                    userMenu.Pemission = sqlDr["Pemission"].ToString();
                                    userMenu.UserID = int.Parse(sqlDr["UserID"].ToString());
                                    userMenu.MenuID = int.Parse(sqlDr["MenuID"].ToString());
                                    lstUserMenu.Add(userMenu);
                                }
                                returnUserMenu.Code = "00";
                                returnUserMenu.Message = "Lấy dữ liệu thành công.";
                                returnUserMenu.lstUserMenu = lstUserMenu;
                            }
                            else
                            {
                                returnUserMenu.Code = "01";
                                returnUserMenu.Message = "Không tồn tại bản ghi nào.";
                                returnUserMenu.Total = 0;
                                returnUserMenu.lstUserMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }

        public ReturnUserMenu Insert(UserMenu userMenu)
        {
            ReturnUserMenu returnUser = new ReturnUserMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = userMenu.ID;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userMenu.UserID;
                        cmd.Parameters.Add(new SqlParameter("@MenuID", SqlDbType.Int)).Value = userMenu.MenuID;
                        cmd.Parameters.Add(new SqlParameter("@Permission", SqlDbType.NVarChar)).Value = userMenu.Pemission;
                        cmd.ExecuteNonQuery();

                        returnUser.Code = "00";
                        returnUser.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }

        public ReturnUserMenu DeleteByID(int _ID)
        {
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnUserMenu.Code = "00";
                        returnUserMenu.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }
        public ReturnUserMenu DeleteByUserID(int _UserID)
        {
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_DeleteByUserID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = _UserID;
                        cmd.ExecuteNonQuery();
                        returnUserMenu.Code = "00";
                        returnUserMenu.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
            }
            return returnUserMenu;
        }
        public ReturnUserMenu CheckPermission(int userID, int menuId, string UnumPermission)
        {
            List<UserMenu> lstUserMenu = null;
            UserMenu userMenu = null;
            ReturnUserMenu returnUserMenu = new ReturnUserMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUserMenu_CheckPermission";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
                        cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int)).Value = menuId;
                        cmd.Parameters.Add(new SqlParameter("@UnumPermission", SqlDbType.NVarChar)).Value = UnumPermission;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUserMenu = new List<UserMenu>();
                                while (sqlDr.Read())
                                {
                                    userMenu = new UserMenu();
                                    userMenu.ID = int.Parse(sqlDr["ID"].ToString());
                                    userMenu.Pemission = sqlDr["Pemission"].ToString();
                                    userMenu.UserID = int.Parse(sqlDr["UserID"].ToString());
                                    userMenu.MenuID = int.Parse(sqlDr["MenuID"].ToString());
                                    lstUserMenu.Add(userMenu);
                                }
                                returnUserMenu.Code = "00";
                                returnUserMenu.Message = "Lấy dữ liệu thành công.";
                                returnUserMenu.lstUserMenu = lstUserMenu;
                            }
                            else
                            {
                                returnUserMenu.Code = "01";
                                returnUserMenu.Message = "Không tồn tại bản ghi nào.";
                                returnUserMenu.Total = 0;
                                returnUserMenu.lstUserMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUserMenu.Code = "99";
                returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUserMenu.Total = 0;
                returnUserMenu.lstUserMenu = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }

        //public ReturnUserMenu CheckUserPermission(int userID, string ControllerName, string UnumPermission)
        //{
        //    List<UserMenu> lstUserMenu = null;
        //    UserMenu userMenu = null;
        //    ReturnUserMenu returnUserMenu = new ReturnUserMenu();
        //    try
        //    {
        //        using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("", sqlConnection))
        //            {
        //                //cmd.Connection = _sqlConnection;
        //                cmd.CommandText = "sp_tUserMenu_CheckUserPermission";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
        //                cmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int)).Value = menuId;
        //                cmd.Parameters.Add(new SqlParameter("@UnumPermission", SqlDbType.NVarChar)).Value = UnumPermission;

        //                using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
        //                {
        //                    if (sqlDr.HasRows)
        //                    {
        //                        lstUserMenu = new List<UserMenu>();
        //                        while (sqlDr.Read())
        //                        {
        //                            userMenu = new UserMenu();
        //                            userMenu.ID = int.Parse(sqlDr["ID"].ToString());
        //                            userMenu.Pemission = sqlDr["Pemission"].ToString();
        //                            userMenu.UserID = int.Parse(sqlDr["UserID"].ToString());
        //                            userMenu.MenuID = int.Parse(sqlDr["MenuID"].ToString());
        //                            lstUserMenu.Add(userMenu);
        //                        }
        //                        returnUserMenu.Code = "00";
        //                        returnUserMenu.Message = "Lấy dữ liệu thành công.";
        //                        returnUserMenu.lstUserMenu = lstUserMenu;
        //                    }
        //                    else
        //                    {
        //                        returnUserMenu.Code = "01";
        //                        returnUserMenu.Message = "Không tồn tại bản ghi nào.";
        //                        returnUserMenu.Total = 0;
        //                        returnUserMenu.lstUserMenu = null;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        returnUserMenu.Code = "99";
        //        returnUserMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
        //        returnUserMenu.Total = 0;
        //        returnUserMenu.lstUserMenu = null;
        //        mylog4net.Error("", ex);
        //    }
        //    return returnUserMenu;
        //}

        
    }
}