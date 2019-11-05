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
    public class UserDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(UserDB));
        public ReturnUser UserAll()
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    //user.RoleID = int.Parse(sqlDr["RoleID"].ToString());
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }

        public ReturnUser GetbyID(int ID)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
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
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    //user.RoleID = int.Parse(sqlDr["RoleID"].ToString());

                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser GetbyFactoryID(string factoryID)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUser_SelectByFactoryID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@factoryID", SqlDbType.NVarChar)).Value = factoryID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    //user.RoleID = int.Parse(sqlDr["RoleID"].ToString());

                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser Insert(User user)
        {
            ReturnUser returnUser = new ReturnUser();
            if(user.MobileNumber == null)
            {
                user.MobileNumber = "";
            }
            try
            {
                //user.PassWord = SMCommon.MD5Endcoding(user.PassWord).ToLower();
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = user.ID;
                        cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar)).Value = user.FullName;
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = user.UserName;
                        cmd.Parameters.Add(new SqlParameter("@PassWord", SqlDbType.VarChar)).Value = user.PassWord;
                        cmd.Parameters.Add(new SqlParameter("@MobileNumber", SqlDbType.NVarChar)).Value = user.MobileNumber;
                        cmd.Parameters.Add(new SqlParameter("@FactoryID", SqlDbType.NVarChar)).Value = user.FactoryID;
                        cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Int)).Value = user.isActive;                        
                        //cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int)).Value = user.RoleID;
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
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }

        public ReturnUser DeleteByID(int _ID)
        {
            ReturnUser returnUser = new ReturnUser();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
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
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }

        public ReturnUser CheckLogin(string username, string password)
        {
            User user = null;
            List<User> lstUser;
            ReturnUser returnUser = new ReturnUser();
            
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUser_CheckLogin";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar)).Value = username;
                        cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = password;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    // user.RoleID = int.Parse(sqlDr["RoleID"].ToString());

                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser SelectByUserName(string userName)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUser_SelectByUserName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = userName;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    // user.RoleID = int.Parse(sqlDr["RoleID"].ToString());

                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public string getUserNameByID(int UserID)
        {
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tUser_getUserNameByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = UserID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                while (sqlDr.Read())
                                {
                                    return sqlDr[0].ToString();
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        
        public ReturnUser SelectDistinctUserID()
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_SelectDistinctUserID";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["UserID"].ToString());
                                    user.UserName = sqlDr["UserName"].ToString();
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser UserExceptUserMenu()
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_tUserExceptUserMenu";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.UserName = sqlDr["UserName"].ToString();
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser SearchByUserName(string where)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUserMenu_SearchByUserName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["UserID"].ToString());
                                    user.UserName = sqlDr["UserName"].ToString();
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser SearchByUserNameAndPassword(string userName, string password)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_SearchByUserNameAndPassword";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = userName;
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = password;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    //user.RoleID = int.Parse(sqlDr["RoleID"].ToString());
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUser CheckPermission_ByUserNameAndPassword(string userName, string password, string permission)
        {
            List<User> lstUser = null;
            User user = null;
            ReturnUser returnUser = new ReturnUser();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tUser_CheckPermission";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = userName;
                        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = password;
                        cmd.Parameters.Add(new SqlParameter("@Permission", SqlDbType.NVarChar)).Value = permission;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstUser = new List<User>();
                                while (sqlDr.Read())
                                {
                                    user = new User();
                                    user.ID = int.Parse(sqlDr["ID"].ToString());
                                    user.FullName = sqlDr["FullName"].ToString();
                                    user.UserName = sqlDr["UserName"].ToString();
                                    user.PassWord = sqlDr["PassWord"].ToString();
                                    user.MobileNumber = sqlDr["MobileNumber"].ToString();
                                    user.FactoryID = sqlDr["FactoryID"].ToString();
                                    user.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    //user.RoleID = int.Parse(sqlDr["RoleID"].ToString());
                                    lstUser.Add(user);
                                }
                                returnUser.Code = "00";
                                returnUser.Message = "Lấy dữ liệu thành công.";
                                returnUser.lstUser = lstUser;
                            }
                            else
                            {
                                returnUser.Code = "01";
                                returnUser.Message = "Không tồn tại bản ghi nào.";
                                returnUser.Total = 0;
                                returnUser.lstUser = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnUser.Code = "99";
                returnUser.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnUser.Total = 0;
                returnUser.lstUser = null;
                mylog4net.Error("", ex);
            }
            return returnUser;
        }
        public ReturnUserPermission ListAllControllerName_PermissionByUserID(int userID)
        {
            List<UserPermission> lstUserPermission = null;
            UserPermission userPermission = null;
            ReturnUserPermission returnUserMenu = new ReturnUserPermission();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMenu_ListAllControllerName_PermissionByUserID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstUserPermission = new List<UserPermission>();
                                while (sqlDr.Read())
                                {
                                    userPermission = new UserPermission();
                                    userPermission.RoleID = int.Parse(sqlDr["RoleID"].ToString());
                                    userPermission.MenuID = int.Parse(sqlDr["MenuID"].ToString());
                                    userPermission.Permission = sqlDr["Permission"].ToString();
                                    userPermission.ControllerName = sqlDr["ControllerName"].ToString();
                                    lstUserPermission.Add(userPermission);
                                }
                                returnUserMenu.Code = "00";
                                returnUserMenu.Message = "Lấy dữ liệu thành công.";
                                returnUserMenu.lstUserPermission = lstUserPermission;
                            }
                            else
                            {
                                returnUserMenu.Code = "01";
                                returnUserMenu.Message = "Không tồn tại bản ghi nào.";
                                returnUserMenu.Total = 0;
                                returnUserMenu.lstUserPermission = null;
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
                returnUserMenu.lstUserPermission = null;
                mylog4net.Error("", ex);
            }
            return returnUserMenu;
        }

        
    }
}