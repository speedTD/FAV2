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
    public class MenuDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MenuDB));
        public ReturnMenu MenuAll()
        {
            List<Menu> lstMenu = null;
            Menu menu = null;
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMenu_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<Menu>();
                                while (sqlDr.Read())
                                {
                                    menu = new Menu();
                                    menu.ID = int.Parse(sqlDr["ID"].ToString());
                                    menu.MenuName = sqlDr["MenuName"].ToString();
                                    menu.DisplayName = sqlDr["DisplayName"].ToString();
                                    menu.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menu.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menu.UrlLink = sqlDr["UrlLink"].ToString();
                                    if (menu.isActive == 1)
                                        lstMenu.Add(menu);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenu = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public ReturnMenu GetbyID(int ID)
        {
            List<Menu> lstMenu = null;
            Menu menu = null;
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMenu_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<Menu>();
                                while (sqlDr.Read())
                                {
                                    menu = new Menu();
                                    menu.ID = int.Parse(sqlDr["ID"].ToString());
                                    menu.MenuName = sqlDr["MenuName"].ToString();
                                    menu.DisplayName = sqlDr["DisplayName"].ToString();
                                    menu.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menu.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menu.UrlLink = sqlDr["UrlLink"].ToString();

                                    lstMenu.Add(menu);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenu = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public ReturnMenu GetbyParentMenuID(int ParentMenuID)
        {
            List<Menu> lstMenu = null;
            Menu menu = null;
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMenu_SelectByParentMenuID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ParentMenuID", SqlDbType.Int)).Value = ParentMenuID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<Menu>();
                                while (sqlDr.Read())
                                {
                                    menu = new Menu();
                                    menu.ID = int.Parse(sqlDr["ID"].ToString());
                                    menu.MenuName = sqlDr["MenuName"].ToString();
                                    menu.DisplayName = sqlDr["DisplayName"].ToString();
                                    menu.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menu.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menu.UrlLink = sqlDr["UrlLink"].ToString();
                                    if (menu.isActive == 1)
                                        lstMenu.Add(menu);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenu = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public ReturnMenu Insert(Menu menu)
        {
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMenu_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = menu.ID;
                        cmd.Parameters.Add(new SqlParameter("@MenuName", SqlDbType.NVarChar)).Value = menu.MenuName;
                        cmd.Parameters.Add(new SqlParameter("@DisplayName", SqlDbType.NVarChar)).Value = menu.DisplayName;
                        cmd.Parameters.Add(new SqlParameter("@ParentMenuID", SqlDbType.Int)).Value = menu.ParentMenuID;
                        cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Int)).Value = menu.isActive;
                        cmd.Parameters.Add(new SqlParameter("@UrlLink", SqlDbType.VarChar)).Value = menu.UrlLink;
                        cmd.ExecuteNonQuery();

                        returnMenu.Code = "00";
                        returnMenu.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public ReturnMenu DeleteByID(int _ID)
        {
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMenu_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnMenu.Code = "00";
                        returnMenu.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }
        public ReturnMenu SelectMenuLevel2()
        {
            List<Menu> lstMenu = null;
            Menu menu = null;
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMenu_SelectAllMenu";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<Menu>();
                                while (sqlDr.Read())
                                {
                                    menu = new Menu();
                                    menu.ID = int.Parse(sqlDr["ID"].ToString());
                                    menu.MenuName = sqlDr["MenuName"].ToString();
                                    menu.DisplayName = sqlDr["DisplayName"].ToString();
                                    menu.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menu.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menu.UrlLink = sqlDr["UrlLink"].ToString();
                                    if (menu.isActive == 1)
                                        lstMenu.Add(menu);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenu = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }
        public ReturnMenu SelectTempSorted()
        {
            List<Menu> lstMenu = null;
            Menu menu = null;
            ReturnMenu returnMenu = new ReturnMenu();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMenu_SelectTempSorted";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<Menu>();
                                while (sqlDr.Read())
                                {
                                    menu = new Menu();
                                    menu.ID = int.Parse(sqlDr["ID"].ToString());
                                    menu.MenuName = sqlDr["MenuName"].ToString();
                                    menu.DisplayName = sqlDr["DisplayName"].ToString();
                                    menu.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menu.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menu.UrlLink = sqlDr["UrlLink"].ToString();
                                    if (menu.isActive == 1)
                                        lstMenu.Add(menu);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenu = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenu = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenu = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public MenuViewModel GetMenuViewModelForDynamicMenu_ByUserID(int userID)
        {
            User currentUser = new User() { ID = userID };
            ReturnMenuRole returnMenuRole = GetMenusByUserID(currentUser);
            var menuViewModel = new MenuViewModel
            {
                returnMenuRole = returnMenuRole,
                user = currentUser
            };
            return menuViewModel;
        }

    //Get all menus (include Role's permission)
    public ReturnMenuRole GetListAllMenuWithRolePermission(int roleID)
        {
            List<MenuRole> lstMenu = null;
            MenuRole menuRole = null;
            ReturnMenuRole returnMenu = new ReturnMenuRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "tMenu_listFullMenu_ByOrder_WithRole";
                        cmd.Parameters.Add(new SqlParameter("@isReturnAllMenu", SqlDbType.Int)).Value = 1; // get full menu item.
                        cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int)).Value = roleID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<MenuRole>();
                                while (sqlDr.Read())
                                {
                                    menuRole = new MenuRole();
                                    menuRole.ID = int.Parse(sqlDr["ID"].ToString());
                                    menuRole.MenuName = sqlDr["MenuName"].ToString();
                                    menuRole.DisplayName = sqlDr["DisplayName"].ToString();
                                    menuRole.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menuRole.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menuRole.UrlLink = sqlDr["UrlLink"].ToString();
                                    menuRole.Permission = sqlDr["Permission"].ToString();
                                    if (menuRole.isActive == 1)
                                        lstMenu.Add(menuRole);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenuRole = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenuRole = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenuRole = null;
                mylog4net.Error("", ex);
            }
            return returnMenu;
        }

        public ReturnMenuRole GetMenusByUserID(User user)
        {
            List<MenuRole> lstMenu = null;
            MenuRole menuRole = null;
            ReturnMenuRole returnMenu = new ReturnMenuRole();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "tMenu_listMenu_ByOrder_WithUserID";
                        cmd.Parameters.Add(new SqlParameter("@isReturnAllMenu", SqlDbType.Int)).Value = 0; // get  menu item corresponding to UserID.
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = user.ID;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMenu = new List<MenuRole>();
                                while (sqlDr.Read())
                                {
                                    menuRole = new MenuRole();
                                    menuRole.ID = int.Parse(sqlDr["ID"].ToString());
                                    menuRole.MenuName = sqlDr["MenuName"].ToString();
                                    menuRole.DisplayName = sqlDr["DisplayName"].ToString();
                                    menuRole.ParentMenuID = int.Parse(sqlDr["ParentMenuID"].ToString());
                                    menuRole.isActive = int.Parse(sqlDr["isActive"].ToString());
                                    menuRole.UrlLink = sqlDr["UrlLink"].ToString();
                                    menuRole.Permission = sqlDr["Permission"].ToString();
                                    if (menuRole.isActive == 1)
                                        lstMenu.Add(menuRole);
                                }
                                returnMenu.Code = "00";
                                returnMenu.Message = "Lấy dữ liệu thành công.";
                                returnMenu.lstMenuRole = lstMenu;
                            }
                            else
                            {
                                returnMenu.Code = "01";
                                returnMenu.Message = "Không tồn tại bản ghi nào.";
                                returnMenu.Total = 0;
                                returnMenu.lstMenuRole = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMenu.Code = "99";
                returnMenu.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMenu.Total = 0;
                returnMenu.lstMenuRole = null;
                mylog4net.Error("public ReturnMenuRole GetMenusByUserID(User user) ", ex);
            }
            return returnMenu;
        }

    }
}