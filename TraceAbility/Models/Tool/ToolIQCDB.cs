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
    public class ToolIQCDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ToolIQCDB));
        protected string s = "ToolIQC";

        public ReturnToolIQC ListAll()
        {
            List<ToolIQC> lstToolIQC = null;
            ToolIQC toolIQC = null;
            ReturnToolIQC returnToolIQC = new ReturnToolIQC();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolIQC_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolIQC = new List<ToolIQC>();
                                while (sqlDr.Read())
                                {
                                    toolIQC = new ToolIQC();
                                    toolIQC.ID = sqlDr["ID"].ToString();
                                    toolIQC.ToolTypeID = int.Parse(sqlDr["ToolTypeID"].ToString());
                                    toolIQC.ToolTypeName = sqlDr["ToolTypeName"].ToString();
                                    toolIQC.PrefixToolID = sqlDr["PrefixToolID"].ToString();
                                    toolIQC.FromToolID = sqlDr["FromToolID"].ToString();
                                    toolIQC.ToToolID = sqlDr["ToToolID"].ToString();
                                    toolIQC.FileUrl = sqlDr["FileUrl"].ToString();
                                    toolIQC.FactoryID = sqlDr["FactoryID"].ToString();
                                    toolIQC.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                    toolIQC.OperatorName = (new UserDB()).getUserNameByID(toolIQC.OperatorID);
                                    toolIQC.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstToolIQC.Add(toolIQC);
                                }
                                returnToolIQC.Code = "00";
                                returnToolIQC.Message = "Lấy dữ liệu thành công.";
                                returnToolIQC.lstToolIQC = lstToolIQC;
                            }
                            else
                            {
                                returnToolIQC.Code = "01";
                                returnToolIQC.Message = "Không tồn tại bản ghi nào.";
                                returnToolIQC.Total = 0;
                                returnToolIQC.lstToolIQC = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolIQC.Code = "99";
                returnToolIQC.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolIQC.Total = 0;
                returnToolIQC.lstToolIQC = null;
                mylog4net.Error("", ex);
            }
            return returnToolIQC;
        }

        public ReturnToolIQC GetbyID(string id)
        {
            List<ToolIQC> lstToolIQC = null;
            ToolIQC toolIQC = null;
            ReturnToolIQC returnToolIQC = new ReturnToolIQC();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolIQC_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolIQC = new List<ToolIQC>();
                                while (sqlDr.Read())
                                {
                                    toolIQC = new ToolIQC();
                                    toolIQC.ID = sqlDr["ID"].ToString();
                                    toolIQC.ToolTypeID = int.Parse(sqlDr["ToolTypeID"].ToString());
                                    toolIQC.PrefixToolID = sqlDr["PrefixToolID"].ToString();
                                    toolIQC.FromToolID = sqlDr["FromToolID"].ToString();
                                    toolIQC.ToToolID = sqlDr["ToToolID"].ToString();
                                    toolIQC.FileUrl =sqlDr["FileUrl"].ToString();
                                    toolIQC.FactoryID = sqlDr["FactoryID"].ToString();
                                    toolIQC.OperatorID = Convert.ToInt16(sqlDr["OperatorID"].ToString());
                                    toolIQC.OperatorName = (new UserDB()).getUserNameByID(toolIQC.OperatorID);

                                    toolIQC.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstToolIQC.Add(toolIQC);
                                }
                                returnToolIQC.Code = "00";
                                returnToolIQC.Message = "Lấy dữ liệu thành công.";
                                returnToolIQC.Total = lstToolIQC.Count;
                                returnToolIQC.lstToolIQC = lstToolIQC;
                            }
                            else
                            {
                                returnToolIQC.Code = "01";
                                returnToolIQC.Message = "Không tồn tại bản ghi nào.";
                                returnToolIQC.Total = 0;
                                returnToolIQC.lstToolIQC = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolIQC.Code = "99";
                returnToolIQC.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolIQC.Total = 0;
                returnToolIQC.lstToolIQC = null;
                mylog4net.Error("", ex);
            }
            return returnToolIQC;
        }
        public ReturnToolIQC CountByFileName(string filename)
        {
            List<ToolIQC> lstToolIQC = null;
            ToolIQC toolIQC = null;
            ReturnToolIQC returnToolIQC = new ReturnToolIQC();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolIQC_SelectByfilename";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@filename", SqlDbType.NVarChar)).Value = filename;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolIQC = new List<ToolIQC>();
                                while (sqlDr.Read())
                                {
                                    toolIQC = new ToolIQC();
                                    toolIQC.ID = sqlDr["ID"].ToString();
                                    toolIQC.ToolTypeID = int.Parse(sqlDr["ToolTypeID"].ToString());
                                    toolIQC.PrefixToolID = sqlDr["PrefixToolID"].ToString();
                                    toolIQC.FromToolID = sqlDr["FromToolID"].ToString();
                                    toolIQC.ToToolID = sqlDr["ToToolID"].ToString();
                                    toolIQC.FileUrl = sqlDr["FileUrl"].ToString();
                                    toolIQC.FactoryID = sqlDr["FactoryID"].ToString();
                                    toolIQC.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                    toolIQC.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstToolIQC.Add(toolIQC);
                                }
                                returnToolIQC.Code = "00";
                                returnToolIQC.Message = "Lấy dữ liệu thành công.";
                                returnToolIQC.Total = lstToolIQC.Count;
                                returnToolIQC.lstToolIQC = lstToolIQC;
                            }
                            else
                            {
                                returnToolIQC.Code = "01";
                                returnToolIQC.Message = "Không tồn tại bản ghi nào.";
                                returnToolIQC.Total = 0;
                                returnToolIQC.lstToolIQC = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolIQC.Code = "99";
                returnToolIQC.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolIQC.Total = 0;
                returnToolIQC.lstToolIQC = null;
                mylog4net.Error("", ex);
            }
            return returnToolIQC;
        }

        public ReturnToolIQC Insert(ToolIQC _toolIQC)
        {
            ReturnToolIQC returnToolIQC = new ReturnToolIQC();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolIQC_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _toolIQC.ID;
                        cmd.Parameters.Add(new SqlParameter("@ToolTypeID", SqlDbType.Int)).Value = _toolIQC.ToolTypeID;
                        cmd.Parameters.Add(new SqlParameter("@PrefixToolID", SqlDbType.VarChar)).Value = _toolIQC.PrefixToolID;
                        cmd.Parameters.Add(new SqlParameter("@FromToolID", SqlDbType.VarChar)).Value = _toolIQC.FromToolID;
                        cmd.Parameters.Add(new SqlParameter("@ToToolID", SqlDbType.VarChar)).Value = _toolIQC.ToToolID;
                        cmd.Parameters.Add(new SqlParameter("@FileUrl", SqlDbType.NVarChar)).Value = _toolIQC.FileUrl;
                        cmd.Parameters.Add(new SqlParameter("@FactoryID", SqlDbType.VarChar)).Value = _toolIQC.FactoryID;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.VarChar)).Value = _toolIQC.OperatorID;
                        cmd.ExecuteNonQuery();
                        returnToolIQC.Code = "00";
                        returnToolIQC.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolIQC.Code = "99";
                returnToolIQC.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolIQC.Total = 0;
                returnToolIQC.lstToolIQC = null;
                mylog4net.Error("", ex);
            }
            return returnToolIQC;
        }


        public ReturnToolIQC DeleteByID(int _ID)
        {
            ReturnToolIQC returnToolIQC = new ReturnToolIQC();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolIQC_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnToolIQC.Code = "00";
                        returnToolIQC.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolIQC.Code = "99";
                returnToolIQC.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolIQC.Total = 0;
                returnToolIQC.lstToolIQC = null;
                mylog4net.Error("", ex);
            }
            return returnToolIQC;
        }
    }
}