using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using TestABC.Common;
using TestABC.Models.Data;

namespace TestABC.Models.Data
{
    public class ToolDB
    {
        public ReturnToolList ListAll()
        {
            List<ToolList> lstToolList = null;
            ToolList toolList = null;
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                    
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolList = new List<ToolList>();
                                while (sqlDr.Read())
                                {
                                    toolList = new ToolList();
                                    toolList.ToolID = sqlDr["ToolID"].ToString();
                                    toolList.UserID = sqlDr["UserID"].ToString();
                                    if (!String.IsNullOrEmpty(toolList.UserID))
                                        toolList.UserName = (new UserDB()).getUserNameByID(Convert.ToInt16(toolList.UserID));
                                    else
                                        toolList.UserName = "";
                                    toolList.ToolType = sqlDr["ToolType"].ToString();
                                    if (!String.IsNullOrEmpty(toolList.ToolType))
                                        toolList.ToolTypeName = (new ToolTypeListDB()).GetToolTypeNamebyID(toolList.ToolType);
                                    else
                                        toolList.ToolTypeName = "";
                                    
                                    toolList.ItemCode = sqlDr["ItemCode"].ToString();
                                    toolList.Maker = sqlDr["Maker"].ToString();
                                    toolList.Specification = sqlDr["Specification"].ToString();
                                    toolList.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    toolList.StartUsing = DateTime.Parse(sqlDr["StartUsing"].ToString());
                                    //toolList.ReceiveDate = sqlDr["ReceiveDate"].ToString();
                                    //toolList.StartUsing = sqlDr["StartUsing"].ToString();

                                    toolList.LifeTime = sqlDr["LifeTime"].ToString();
                                    toolList.ExpireDate = DateTime.Parse(sqlDr["ExpireDate"].ToString());
                                    //toolList.ExpireDate = sqlDr["ExpireDate"].ToString();
                                    toolList.LineID = sqlDr["LineID"].ToString();
                                    toolList.Status = sqlDr["Status"].ToString();
                                    toolList.Remark = sqlDr["Remark"].ToString();
                                    toolList.ImageUrl = sqlDr["ImageUrl"].ToString();
                                    toolList.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    toolList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolList.Add(toolList);
                                }
                                returnToolList.Code = "00";
                                returnToolList.Message = "Lấy dữ liệu thành công.";
                                returnToolList.lstToolList = lstToolList;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnToolList.Code = "01";
                                returnToolList.Message = "Không tồn tại bản ghi nào.";
                                returnToolList.Total = 0;
                                returnToolList.lstToolList = null;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }

        public ReturnToolList GetbyID(string ToolListID)
        {
            List<ToolList> lstToolList = null;
            ToolList toolList = null;
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = ToolListID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolList = new List<ToolList>();
                                while (sqlDr.Read())
                                {
                                    toolList = new ToolList();
                                    toolList.ToolID = sqlDr["ToolID"].ToString();
                                    toolList.UserID = sqlDr["UserID"].ToString();
                                    toolList.ToolType = sqlDr["ToolType"].ToString();
                                    if (!String.IsNullOrEmpty(toolList.ToolType))
                                        toolList.ToolTypeName = (new ToolTypeListDB()).GetToolTypeNamebyID(toolList.ToolType);
                                    else
                                        toolList.ToolTypeName = "";
                                    toolList.ItemCode = sqlDr["ItemCode"].ToString();
                                    toolList.Maker = sqlDr["Maker"].ToString();
                                    toolList.Specification = sqlDr["Specification"].ToString();
                                    toolList.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    toolList.StartUsing = DateTime.Parse(sqlDr["StartUsing"].ToString());
                                    //toolList.ReceiveDate = sqlDr["ReceiveDate"].ToString();
                                    //toolList.StartUsing = sqlDr["StartUsing"].ToString();

                                    toolList.LifeTime = sqlDr["LifeTime"].ToString();
                                    toolList.ExpireDate = DateTime.Parse(sqlDr["ExpireDate"].ToString());
                                    //toolList.ExpireDate = sqlDr["ExpireDate"].ToString();
                                    toolList.LineID = sqlDr["LineID"].ToString();
                                    toolList.Status = sqlDr["Status"].ToString();
                                    toolList.Remark = sqlDr["Remark"].ToString();
                                    toolList.ImageUrl = sqlDr["ImageUrl"].ToString();
                                    toolList.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    toolList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolList.Add(toolList);
                                }
                                returnToolList.Code = "00";
                                returnToolList.Message = "Lấy dữ liệu thành công.";
                                //_ReturnToolList.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnToolList.lstToolList = lstToolList;
                                //}
                            }
                            else
                            {
                                returnToolList.Code = "01";
                                returnToolList.Message = "Không tồn tại bản ghi nào.";
                                returnToolList.Total = 0;
                                returnToolList.lstToolList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }

        public ReturnToolList Insert(ToolList toolList)
        {
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = toolList.ToolID;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.VarChar)).Value = toolList.UserID;
                        cmd.Parameters.Add(new SqlParameter("@ToolType", SqlDbType.VarChar)).Value = toolList.ToolType;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value = toolList.ItemCode;
                        cmd.Parameters.Add(new SqlParameter("@Maker", SqlDbType.VarChar)).Value = toolList.Maker;
                        cmd.Parameters.Add(new SqlParameter("@Specification", SqlDbType.NVarChar)).Value = toolList.Specification;
                        cmd.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime)).Value = toolList.ReceiveDate;
                        cmd.Parameters.Add(new SqlParameter("@StartUsing", SqlDbType.DateTime)).Value = toolList.StartUsing;
                        cmd.Parameters.Add(new SqlParameter("@LifeTime", SqlDbType.VarChar)).Value = toolList.LifeTime;
                        cmd.Parameters.Add(new SqlParameter("@ExpireDate", SqlDbType.DateTime)).Value = toolList.ExpireDate;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = toolList.LineID;
                        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar)).Value = toolList.Status;
                        cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar)).Value = toolList.Remark;
                        cmd.Parameters.Add(new SqlParameter("@ImageUrl", SqlDbType.VarChar)).Value = String.IsNullOrEmpty(toolList.ImageUrl) ? ""  : toolList.ImageUrl;                        
                        //cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime)).Value = toolList.CreatedDate;
                        //cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Int)).Value = toolList.isActive;

                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();

                        returnToolList.Code = "00";
                        returnToolList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }

        public ReturnToolList DeleteByID(string _ID)
        {
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnToolList.Code = "00";
                        returnToolList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;





            }
            return returnToolList;
        }

        //list by selected page number.
        public ReturnToolList ListbyPage(int pageNumber, int pageSize)
        {
            List<ToolList> lstToolList = null;
            ToolList toolList = null;
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolList_SelectByPage";
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
                                lstToolList = new List<ToolList>();
                                while (sqlDr.Read())
                                {
                                    toolList = new ToolList();
                                    toolList.ToolID = sqlDr["ToolID"].ToString();
                                    toolList.UserID = sqlDr["UserID"].ToString();
                                    toolList.UserName = sqlDr["UserName"].ToString();
                                    toolList.ToolType = sqlDr["ToolType"].ToString();
                                    if (!String.IsNullOrEmpty(toolList.ToolType))
                                        toolList.ToolTypeName = (new ToolTypeListDB()).GetToolTypeNamebyID(toolList.ToolType);
                                    else
                                        toolList.ToolTypeName = "";
                                    toolList.ItemCode = sqlDr["ItemCode"].ToString();
                                    toolList.Maker = sqlDr["Maker"].ToString();
                                    toolList.Specification = sqlDr["Specification"].ToString();
                                    toolList.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    toolList.StartUsing = DateTime.Parse(sqlDr["StartUsing"].ToString());
                                    //toolList.ReceiveDate = sqlDr["ReceiveDate"].ToString();
                                    //toolList.StartUsing = sqlDr["StartUsing"].ToString();

                                    toolList.LifeTime = sqlDr["LifeTime"].ToString();
                                    toolList.ExpireDate = DateTime.Parse(sqlDr["ExpireDate"].ToString());
                                    //toolList.ExpireDate = sqlDr["ExpireDate"].ToString();
                                    toolList.LineID = sqlDr["LineID"].ToString();
                                    toolList.Status = sqlDr["Status"].ToString();
                                    toolList.Remark = sqlDr["Remark"].ToString();
                                    toolList.ImageUrl = sqlDr["ImageUrl"].ToString();
                                    toolList.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    toolList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolList.Add(toolList);
                                }
                                returnToolList.Code = "00";
                                returnToolList.Message = "Lấy dữ liệu thành công.";
                                returnToolList.lstToolList = lstToolList;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnToolList.Code = "01";
                                returnToolList.Message = "Không tồn tại bản ghi nào.";
                                returnToolList.Total = 0;
                                returnToolList.lstToolList = null;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                        }
                        //get return Totalpage value.
                        if (returnToolList.Code == "00")
                            returnToolList.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }

        public ReturnToolList SearchTools(ReturnToolList searchToolLists, int pageSize)
        {
            List<ToolList> lstToolList = null;
            ToolList toolList = null;
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tToolList_SearchTools";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ToolID", SqlDbType.VarChar)).Value = string.Format("%{0}%", searchToolLists.aToolList.ToolID) ;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value =  string.Format("%{0}%", searchToolLists.aToolList.ItemCode);
                        cmd.Parameters.Add(new SqlParameter("@Maker", SqlDbType.VarChar)).Value = string.Format("%{0}%", searchToolLists.aToolList.Maker);
                        if (searchToolLists.aToolList.ReceiveDate == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime)).Value = searchToolLists.aToolList.ReceiveDate;
                        if (searchToolLists.aToolList.StartUsing == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@StartUsing", SqlDbType.DateTime)).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(new SqlParameter("@StartUsing", SqlDbType.DateTime)).Value = searchToolLists.aToolList.StartUsing;
                        if (searchToolLists.aToolList.ExpireDate == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@ExpireDate", SqlDbType.DateTime)).Value = DBNull.Value; 
                        else
                            cmd.Parameters.Add(new SqlParameter("@ExpireDate", SqlDbType.DateTime)).Value = searchToolLists.aToolList.ExpireDate;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = string.Format("%{0}%", searchToolLists.aToolList.LineID);
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = searchToolLists.PageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;
                        
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstToolList = new List<ToolList>();
                                while (sqlDr.Read())
                                {
                                    toolList = new ToolList();
                                    toolList.ToolID = sqlDr["ToolID"].ToString();
                                    toolList.UserID = sqlDr["UserID"].ToString();
                                    toolList.UserName = sqlDr["UserName"].ToString();
                                    toolList.ToolType = sqlDr["ToolType"].ToString();
                                    if (!String.IsNullOrEmpty(toolList.ToolType))
                                        toolList.ToolTypeName = (new ToolTypeListDB()).GetToolTypeNamebyID(toolList.ToolType);
                                    else
                                        toolList.ToolTypeName = "";
                                    toolList.ItemCode = sqlDr["ItemCode"].ToString();
                                    toolList.Maker = sqlDr["Maker"].ToString();
                                    toolList.Specification = sqlDr["Specification"].ToString();
                                    toolList.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    toolList.StartUsing = DateTime.Parse(sqlDr["StartUsing"].ToString());
                                    toolList.LifeTime = sqlDr["LifeTime"].ToString();
                                    toolList.ExpireDate = DateTime.Parse(sqlDr["ExpireDate"].ToString());
                                    toolList.LineID = sqlDr["LineID"].ToString();
                                    toolList.Status = sqlDr["Status"].ToString();
                                    toolList.Remark = sqlDr["Remark"].ToString();
                                    toolList.ImageUrl = sqlDr["ImageUrl"].ToString();
                                    toolList.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    toolList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolList.Add(toolList);
                                }
                                returnToolList.Code = "00";
                                returnToolList.Message = "Lấy dữ liệu thành công.";
                                returnToolList.lstToolList = lstToolList;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnToolList.Code = "01";
                                returnToolList.Message = "Không tồn tại bản ghi nào.";
                                returnToolList.Total = 0;
                                returnToolList.lstToolList = null;
                                returnToolList.UserName = MyShareInfo.UserName;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                        }
                        //get return Totalpage value.
                        if (returnToolList.Code == "00")
                            returnToolList.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }
        public ReturnToolList SelectByCondition(string where)
        {
            List<ToolList> lstToolList = null;
            ToolList toolList = null;
            ReturnToolList returnToolList = new ReturnToolList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tToolList_SelectByCondition";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstToolList = new List<ToolList>();
                                while (sqlDr.Read())
                                {
                                    toolList = new ToolList();
                                    toolList.ToolID = sqlDr["ToolID"].ToString();
                                    toolList.UserID = sqlDr["UserID"].ToString();
                                    toolList.UserName = sqlDr["UserName"].ToString();
                                    toolList.ToolType = sqlDr["ToolType"].ToString();
                                    toolList.ItemCode = sqlDr["ItemCode"].ToString();
                                    toolList.Maker = sqlDr["Maker"].ToString();
                                    toolList.Specification = sqlDr["Specification"].ToString();
                                    toolList.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    toolList.StartUsing = DateTime.Parse(sqlDr["StartUsing"].ToString());
                                    //toolList.ReceiveDate = sqlDr["ReceiveDate"].ToString();
                                    //toolList.StartUsing = sqlDr["StartUsing"].ToString();

                                    toolList.LifeTime = sqlDr["LifeTime"].ToString();
                                    toolList.ExpireDate = DateTime.Parse(sqlDr["ExpireDate"].ToString());
                                    //toolList.ExpireDate = sqlDr["ExpireDate"].ToString();
                                    toolList.LineID = sqlDr["LineID"].ToString();
                                    toolList.Status = sqlDr["Status"].ToString();
                                    toolList.Remark = sqlDr["Remark"].ToString();
                                    toolList.ImageUrl = sqlDr["ImageUrl"].ToString();
                                    toolList.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    toolList.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstToolList.Add(toolList);
                                }
                                returnToolList.Code = "00";
                                returnToolList.Message = "Lấy dữ liệu thành công.";
                                //_ReturnToolList.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnToolList.lstToolList = lstToolList;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                                //}
                            }
                            else
                            {
                                returnToolList.Code = "01";
                                returnToolList.Message = "Không tồn tại bản ghi nào.";
                                returnToolList.Total = 0;
                                returnToolList.lstToolList = null;
                                returnToolList.UserID = MyShareInfo.ID;
                                returnToolList.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnToolList.Code = "99";
                returnToolList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnToolList.Total = 0;
                returnToolList.lstToolList = null;
            }
            return returnToolList;
        }
    }
}