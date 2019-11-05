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
    public class ToolTypeListDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ToolTypeListDB));
        public ReturnToolTypeList ListAll()
        {
            List<ToolTypeList> lstToolTypeList = null;
            ToolTypeList toolTypeList = null;
            ReturnToolTypeList returnToolTypeList = new ReturnToolTypeList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolTypeList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolTypeList = new List<ToolTypeList>();
                                while (sqlDr.Read())
                                {
                                    toolTypeList = new ToolTypeList();
                                    toolTypeList.ToolTypeID = sqlDr["ToolTypeID"].ToString();
                                    toolTypeList.ToolTypeName = sqlDr["ToolTypeName"].ToString();
                                    toolTypeList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolTypeList.Add(toolTypeList);
                                }
                                returnToolTypeList.Code = "00";
                                returnToolTypeList.Message = "Lấy dữ liệu thành công.";
                                returnToolTypeList.LstToolTypeList = lstToolTypeList;
                            }
                            else
                            {
                                returnToolTypeList.Code = "01";
                                returnToolTypeList.Message = "Không tồn tại bản ghi nào.";
                                returnToolTypeList.Total = 0;
                                returnToolTypeList.LstToolTypeList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolTypeList.Code = "99";
                returnToolTypeList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolTypeList.Total = 0;
                returnToolTypeList.LstToolTypeList = null;
                mylog4net.Error("", ex);
            }
            return returnToolTypeList;
        }

        public ReturnToolTypeList GetbyID(string ToolTypeID)
        {
            List<ToolTypeList> lstToolTypeList = null;
            ToolTypeList toolTypeList = null;
            ReturnToolTypeList returnToolTypeList = new ReturnToolTypeList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolTypeList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeID", SqlDbType.VarChar)).Value = ToolTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolTypeList = new List<ToolTypeList>();
                                while (sqlDr.Read())
                                {
                                    toolTypeList = new ToolTypeList();
                                    toolTypeList.ToolTypeID = sqlDr["ToolTypeID"].ToString();
                                    toolTypeList.ToolTypeName = sqlDr["ToolTypeName"].ToString();
                                    toolTypeList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolTypeList.Add(toolTypeList);
                                }
                                returnToolTypeList.Code = "00";
                                returnToolTypeList.Message = "Lấy dữ liệu thành công.";
                                returnToolTypeList.LstToolTypeList = lstToolTypeList;
                            }
                            else
                            {
                                returnToolTypeList.Code = "01";
                                returnToolTypeList.Message = "Không tồn tại bản ghi nào.";
                                returnToolTypeList.Total = 0;
                                returnToolTypeList.LstToolTypeList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolTypeList.Code = "99";
                returnToolTypeList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolTypeList.Total = 0;
                returnToolTypeList.LstToolTypeList = null;
                mylog4net.Error("", ex);
            }
            return returnToolTypeList;
        }
        public string GetToolTypeNamebyID(string ToolTypeID)
        {
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolTypeList_GetToolTypeNamebyID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeID", SqlDbType.VarChar)).Value = ToolTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                while (sqlDr.Read())
                                {
                                    return sqlDr[0].ToString();
                                }
                            }
                            else
                            {
                                return "";
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
        public ReturnToolTypeList GetbyToolTypeName(string toolTypeName)
        {
            List<ToolTypeList> lstToolTypeList = null;
            ToolTypeList toolTypeList = null;
            ReturnToolTypeList returnToolTypeList = new ReturnToolTypeList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolTypeList_SelectByToolTypeName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeName", SqlDbType.NVarChar)).Value = toolTypeName;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolTypeList = new List<ToolTypeList>();
                                while (sqlDr.Read())
                                {
                                    toolTypeList = new ToolTypeList();
                                    toolTypeList.ToolTypeID = sqlDr["ToolTypeID"].ToString();
                                    toolTypeList.ToolTypeName = sqlDr["ToolTypeName"].ToString();
                                    toolTypeList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolTypeList.Add(toolTypeList);
                                }
                                returnToolTypeList.Code = "00";
                                returnToolTypeList.Message = "Lấy dữ liệu thành công.";
                                returnToolTypeList.LstToolTypeList = lstToolTypeList;
                            }
                            else
                            {
                                returnToolTypeList.Code = "01";
                                returnToolTypeList.Message = "Không tồn tại bản ghi nào.";
                                returnToolTypeList.Total = 0;
                                returnToolTypeList.LstToolTypeList = new List<ToolTypeList>();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolTypeList.Code = "99";
                returnToolTypeList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolTypeList.Total = 0;
                returnToolTypeList.LstToolTypeList = new List<ToolTypeList>();
                mylog4net.Error("", ex);
            }
            return returnToolTypeList;
        }

        public ReturnToolTypeList Insert(ToolTypeList toolTypeList)
        {
            ReturnToolTypeList returnToolTypeList = new ReturnToolTypeList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolTypeList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeID", SqlDbType.VarChar)).Value = toolTypeList.ToolTypeID;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeName", SqlDbType.NVarChar)).Value = toolTypeList.ToolTypeName;
                        cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit)).Value = 1;

                        cmd.ExecuteNonQuery();

                        returnToolTypeList.Code = "00";
                        returnToolTypeList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolTypeList.Code = "99";
                returnToolTypeList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolTypeList.Total = 0;
                returnToolTypeList.LstToolTypeList = null;
                mylog4net.Error("", ex);
            }
            return returnToolTypeList;
        }


        public ReturnToolTypeList DeleteByID(string _ID)
        {
            ReturnToolTypeList returnToolTypeList = new ReturnToolTypeList();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolTypeList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeID", SqlDbType.VarChar)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnToolTypeList.Code = "00";
                        returnToolTypeList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolTypeList.Code = "99";
                returnToolTypeList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolTypeList.Total = 0;
                returnToolTypeList.LstToolTypeList = null;
                mylog4net.Error("", ex);
            }
            return returnToolTypeList;
        }
    }
}