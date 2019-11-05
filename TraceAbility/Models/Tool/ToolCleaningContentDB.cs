using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using TestABC.Common;

using log4net;
namespace TestABC.Models.Data
{
    public class ToolCleaningContentDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ToolCleaningContentDB));
        public ReturnToolCleaningContent GetAll()
        {
            List<ToolCleaningContent> lstToolCleaningContent = null;
            ToolCleaningContent toolCleaningContent = null;
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolCleaningContent_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolCleaningContent = new List<ToolCleaningContent>();
                                while (sqlDr.Read())
                                {
                                    toolCleaningContent = new ToolCleaningContent();
                                    toolCleaningContent.ID = Convert.ToInt32(sqlDr["ID"].ToString()); 
                                    toolCleaningContent.ToolID = sqlDr["ToolID"].ToString();
                                    toolCleaningContent.LineUsing = sqlDr["LineUsing"].ToString();
                                    toolCleaningContent.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    toolCleaningContent.Result = sqlDr["Result"].ToString();
                                    toolCleaningContent.NGContents = sqlDr["NGContents"].ToString();
                                    toolCleaningContent.RepairDate = DateTime.Parse(sqlDr["RepairDate"].ToString());
                                    toolCleaningContent.RepairContents = sqlDr["RepairContents"].ToString();
                                    toolCleaningContent.RepairID = sqlDr["RepairID"].ToString();
                                    toolCleaningContent.CheckBy = sqlDr["CheckBy"].ToString();
                                    toolCleaningContent.ImageLink = sqlDr["ImageLink"].ToString();
                                    lstToolCleaningContent.Add(toolCleaningContent);
                                }
                                returnToolCleaningContent.Code = "00";
                                returnToolCleaningContent.Message = "Lấy dữ liệu thành công.";
                                returnToolCleaningContent.LstToolCleaningContent = lstToolCleaningContent;
                            }
                            else
                            {
                                returnToolCleaningContent.Code = "01";
                                returnToolCleaningContent.Message = "Không tồn tại bản ghi nào.";
                                returnToolCleaningContent.Total = 0;
                                returnToolCleaningContent.LstToolCleaningContent = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }

        public ReturnToolCleaningContent GetbyID(string ID)
        {
            List<ToolCleaningContent> lstToolCleaningContent = null;
            ToolCleaningContent toolCleaningContent = null;
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolCleaningContent_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.VarChar)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolCleaningContent = new List<ToolCleaningContent>();
                                while (sqlDr.Read())
                                {
                                    toolCleaningContent = new ToolCleaningContent();
                                    toolCleaningContent.ID = Convert.ToInt32(sqlDr["ID"].ToString()); 
                                    toolCleaningContent.ToolID = sqlDr["ToolID"].ToString();
                                    toolCleaningContent.LineUsing = sqlDr["LineUsing"].ToString();
                                    toolCleaningContent.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    toolCleaningContent.Result = sqlDr["Result"].ToString();
                                    toolCleaningContent.NGContents = sqlDr["NGContents"].ToString();
                                    toolCleaningContent.RepairDate = DateTime.Parse(sqlDr["RepairDate"].ToString());
                                    toolCleaningContent.RepairContents = sqlDr["RepairContents"].ToString();
                                    toolCleaningContent.RepairID = sqlDr["RepairID"].ToString();
                                    toolCleaningContent.CheckBy = sqlDr["CheckBy"].ToString();
                                    toolCleaningContent.ImageLink = sqlDr["ImageLink"].ToString();
                                    lstToolCleaningContent.Add(toolCleaningContent);
                                }
                                returnToolCleaningContent.Code = "00";
                                returnToolCleaningContent.Message = "Lấy dữ liệu thành công.";

                                returnToolCleaningContent.LstToolCleaningContent = lstToolCleaningContent;
                                //}
                            }
                            else
                            {
                                returnToolCleaningContent.Code = "01";
                                returnToolCleaningContent.Message = "Không tồn tại bản ghi nào.";
                                returnToolCleaningContent.Total = 0;
                                returnToolCleaningContent.LstToolCleaningContent = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }
        public ReturnToolCleaningContent GetbyIDRepairDate(ToolCleaningContent _searchToolCleaningContent)
        {
            List<ToolCleaningContent> lstToolCleaningContent = null;
            ToolCleaningContent toolCleaningContent = null;
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolCleaningContent_SelectByToolIDRepairDate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = _searchToolCleaningContent.ToolID;
                        cmd.Parameters.Add(new SqlParameter("@RepairDate", SqlDbType.DateTime)).Value = _searchToolCleaningContent.RepairDate;
                        cmd.Parameters.Add(new SqlParameter("@Shift", SqlDbType.VarChar)).Value = _searchToolCleaningContent.Shift;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolCleaningContent = new List<ToolCleaningContent>();
                                while (sqlDr.Read())
                                {
                                    toolCleaningContent = new ToolCleaningContent();
                                    toolCleaningContent.ID = Convert.ToInt32(sqlDr["ID"].ToString()); 
                                    toolCleaningContent.ToolID = sqlDr["ToolID"].ToString();
                                    toolCleaningContent.LineUsing = sqlDr["LineUsing"].ToString();
                                    toolCleaningContent.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    toolCleaningContent.Result = sqlDr["Result"].ToString();
                                    toolCleaningContent.NGContents = sqlDr["NGContents"].ToString();
                                    toolCleaningContent.RepairDate = DateTime.Parse(sqlDr["RepairDate"].ToString());
                                    toolCleaningContent.RepairContents = sqlDr["RepairContents"].ToString();
                                    toolCleaningContent.RepairID = sqlDr["RepairID"].ToString();
                                    toolCleaningContent.CheckBy = sqlDr["CheckBy"].ToString();
                                    toolCleaningContent.ImageLink = sqlDr["ImageLink"].ToString();
                                    lstToolCleaningContent.Add(toolCleaningContent);
                                }
                                returnToolCleaningContent.Code = "00";
                                returnToolCleaningContent.Message = "Lấy dữ liệu thành công.";
                                returnToolCleaningContent.LstToolCleaningContent = lstToolCleaningContent;
                                //}
                            }
                            else
                            {
                                returnToolCleaningContent.Code = "01";
                                returnToolCleaningContent.Message = "Không tồn tại bản ghi nào.";
                                returnToolCleaningContent.Total = 0;
                                returnToolCleaningContent.LstToolCleaningContent = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }


        public ReturnToolCleaningContent Insert(ToolCleaningContent toolCleaningContent)
        {
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolCleaningContent_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = toolCleaningContent.ToolID;
                        cmd.Parameters.Add(new SqlParameter("@LineUsing", SqlDbType.VarChar)).Value = toolCleaningContent.LineUsing;
                        cmd.Parameters.Add(new SqlParameter("@Shift", SqlDbType.SmallInt)).Value = toolCleaningContent.Shift;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = toolCleaningContent.Result;
                        cmd.Parameters.Add(new SqlParameter("@NGContents", SqlDbType.NVarChar)).Value = toolCleaningContent.NGContents;       
                        cmd.Parameters.Add(new SqlParameter("@RepairDate", SqlDbType.DateTime)).Value = toolCleaningContent.RepairDate;
                        cmd.Parameters.Add(new SqlParameter("@RepairContents", SqlDbType.NVarChar)).Value = toolCleaningContent.RepairContents;
                        cmd.Parameters.Add(new SqlParameter("@RepairID", SqlDbType.VarChar)).Value = toolCleaningContent.RepairID;
                        cmd.Parameters.Add(new SqlParameter("@CheckBy", SqlDbType.VarChar)).Value = toolCleaningContent.CheckBy;
                        cmd.Parameters.Add(new SqlParameter("@ImageLink", SqlDbType.VarChar)).Value = String.IsNullOrEmpty(toolCleaningContent.ImageLink) ? "" : toolCleaningContent.ImageLink;
                        cmd.ExecuteNonQuery();

                        returnToolCleaningContent.Code = "00";
                        returnToolCleaningContent.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }


        public ReturnToolCleaningContent DeleteByID(string _ID)
        {
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolCleaningContent_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnToolCleaningContent.Code = "00";
                        returnToolCleaningContent.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
            }
            return returnToolCleaningContent;
        }

        //paging
        public ReturnToolCleaningContent ListbyPage(int pageNumber, int pageSize)
        {
            List<ToolCleaningContent> lstToolCleaningContent = null;
            ToolCleaningContent toolCleaningContent = null;
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolCleaningContent_SelectByPage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolCleaningContent = new List<ToolCleaningContent>();
                                while (sqlDr.Read())
                                {
                                    toolCleaningContent = new ToolCleaningContent();
                                    toolCleaningContent.ID = Convert.ToInt32(sqlDr["ID"].ToString()); 
                                    toolCleaningContent.ToolID = sqlDr["ToolID"].ToString();
                                    toolCleaningContent.LineUsing = sqlDr["LineUsing"].ToString();
                                    toolCleaningContent.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    toolCleaningContent.Result = sqlDr["Result"].ToString();
                                    toolCleaningContent.NGContents = sqlDr["NGContents"].ToString();
                                    toolCleaningContent.RepairDate = DateTime.Parse(sqlDr["RepairDate"].ToString());
                                    toolCleaningContent.RepairContents = sqlDr["RepairContents"].ToString();
                                    toolCleaningContent.RepairID = sqlDr["RepairID"].ToString();
                                    toolCleaningContent.CheckBy = sqlDr["CheckBy"].ToString();
                                    toolCleaningContent.ImageLink = sqlDr["ImageLink"].ToString();
                                    lstToolCleaningContent.Add(toolCleaningContent);
                                }
                                returnToolCleaningContent.Code = "00";
                                returnToolCleaningContent.Message = "Lấy dữ liệu thành công.";
                                returnToolCleaningContent.LstToolCleaningContent = lstToolCleaningContent;
                            }
                            else
                            {
                                returnToolCleaningContent.Code = "01";
                                returnToolCleaningContent.Message = "Không tồn tại bản ghi nào.";
                                returnToolCleaningContent.Total = 0;
                                returnToolCleaningContent.LstToolCleaningContent = null;
                            }
                        }
                        //get return Totalpage value.
                        if (returnToolCleaningContent.Code == "00")
                            returnToolCleaningContent.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }
        public ReturnToolCleaningContent SearchToolCleaning(ReturnToolCleaningContent searchToolCleaning, int pageSize)
        {
            List<ToolCleaningContent> lstToolCleaningContent = null;
            ToolCleaningContent toolCleaningContent = null;
            ReturnToolCleaningContent returnToolCleaningContent = new ReturnToolCleaningContent();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolCleaningContent_SearchToolCleaning";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = searchToolCleaning.aToolCleaningContent.ToolID;
                        cmd.Parameters.Add(new SqlParameter("@LineUsing", SqlDbType.VarChar)).Value = searchToolCleaning.aToolCleaningContent.LineUsing;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = searchToolCleaning.aToolCleaningContent.Result;
                        cmd.Parameters.Add(new SqlParameter("@CheckBy", SqlDbType.VarChar)).Value = searchToolCleaning.aToolCleaningContent.CheckBy;
                        if (searchToolCleaning.aToolCleaningContent.FromDate == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime)).Value = searchToolCleaning.aToolCleaningContent.FromDate;
                        if (searchToolCleaning.aToolCleaningContent.ToDate == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime)).Value = searchToolCleaning.aToolCleaningContent.ToDate;
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = searchToolCleaning.PageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolCleaningContent = new List<ToolCleaningContent>();
                                while (sqlDr.Read())
                                {
                                    toolCleaningContent = new ToolCleaningContent();
                                    toolCleaningContent.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    toolCleaningContent.ToolID = sqlDr["ToolID"].ToString();
                                    toolCleaningContent.LineUsing = sqlDr["LineUsing"].ToString();
                                    toolCleaningContent.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    toolCleaningContent.Result = sqlDr["Result"].ToString();
                                    toolCleaningContent.NGContents = sqlDr["NGContents"].ToString();
                                    toolCleaningContent.RepairDate = DateTime.Parse(sqlDr["RepairDate"].ToString());
                                    toolCleaningContent.RepairContents = sqlDr["RepairContents"].ToString();
                                    toolCleaningContent.RepairID = sqlDr["RepairID"].ToString();
                                    toolCleaningContent.CheckBy = sqlDr["CheckBy"].ToString();
                                    toolCleaningContent.ImageLink = sqlDr["ImageLink"].ToString();
                                    lstToolCleaningContent.Add(toolCleaningContent);
                                }
                                returnToolCleaningContent.Code = "00";
                                returnToolCleaningContent.Message = "Lấy dữ liệu thành công.";
                                returnToolCleaningContent.LstToolCleaningContent = lstToolCleaningContent;
                            }
                            else
                            {
                                returnToolCleaningContent.Code = "01";
                                returnToolCleaningContent.Message = "Không tồn tại bản ghi nào.";
                                returnToolCleaningContent.Total = 0;
                                returnToolCleaningContent.LstToolCleaningContent = null;
                            }
                        }
                        //get return Totalpage value.
                        if (returnToolCleaningContent.Code == "00")
                            returnToolCleaningContent.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolCleaningContent.Code = "99";
                returnToolCleaningContent.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolCleaningContent.Total = 0;
                returnToolCleaningContent.LstToolCleaningContent = null;
                mylog4net.Error("", ex);
            }
            return returnToolCleaningContent;
        }
    }
}