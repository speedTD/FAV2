using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using TestABC.Common;
using TestABC.Models.Data;

using log4net;
namespace TestABC.Models.Data
{
    public class MachineMtnContentListDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnContentListDB));
        public ReturnMachineMtnContentList MachineMtnContentListAll()
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList GetbyID(int ID)
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineMtnContentList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList Insert(MachineMtnContentList machineMtnContentList)
        {
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtnContentList.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.VarChar)).Value = machineMtnContentList.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@MachinePartID", SqlDbType.NVarChar)).Value = machineMtnContentList.MachinePartID;
                        cmd.Parameters.Add(new SqlParameter("@ContentMtn", SqlDbType.NVarChar)).Value = machineMtnContentList.ContentMtn.Trim();
                        cmd.Parameters.Add(new SqlParameter("@ToolMtn", SqlDbType.NVarChar)).Value = machineMtnContentList.ToolMtn != null ? machineMtnContentList.ToolMtn.Trim() : "";
                        cmd.Parameters.Add(new SqlParameter("@MethodMtn", SqlDbType.NVarChar)).Value = machineMtnContentList.MethodMtn != null ? machineMtnContentList.MethodMtn.Trim() : "";
                        cmd.Parameters.Add(new SqlParameter("@Standard", SqlDbType.NVarChar)).Value = machineMtnContentList.Standard != null ? machineMtnContentList.Standard.Trim() : "";
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = machineMtnContentList.FrequencyID;                        
                        cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit)).Value = machineMtnContentList.IsActive;
                        cmd.Parameters.Add(new SqlParameter("@Min", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(machineMtnContentList.Min) ? "" : machineMtnContentList.Min.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Max", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(machineMtnContentList.Max) ? "" : machineMtnContentList.Max.Trim();
                        cmd.ExecuteNonQuery();

                        returnMachineTypeMtnContentList.Code = "00";
                        returnMachineTypeMtnContentList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList DeleteByID(int _ID)
        {
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnMachineTypeMtnContentList.Code = "00";
                        returnMachineTypeMtnContentList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
            }
            return returnMachineTypeMtnContentList;
        }
        public ReturnMachineMtnContentList GetByMachineID(string machineID, short FrequencyID)
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_SelectByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineID.Trim();
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = FrequencyID;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                                returnMachineTypeMtnContentList.UserID = MyShareInfo.ID;
                                returnMachineTypeMtnContentList.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList GetByMachineTypeID(int machineTypeID)
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_SelectByMachineID_NEW";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.VarChar)).Value = machineTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.Total = lstMachineTypeMtnContentList.Count();
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }
        public ReturnMachineMtnContentList SearchMachineMtnContentList(ReturnMachineMtnContentList machinemtnContent, int pageSize)
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                { 
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_Search";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = machinemtnContent.aMachineTypeMtnContentList.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@MachinePartName", SqlDbType.NVarChar)).Value = machinemtnContent.aMachineTypeMtnContentList.MachinePartName;
                        cmd.Parameters.Add(new SqlParameter("@ContentMtn", SqlDbType.NVarChar)).Value = machinemtnContent.aMachineTypeMtnContentList.ContentMtn;
                        cmd.Parameters.Add(new SqlParameter("@ToolMtn", SqlDbType.NVarChar)).Value = machinemtnContent.aMachineTypeMtnContentList.ToolMtn;
                        cmd.Parameters.Add(new SqlParameter("@MethodMtn", SqlDbType.NVarChar)).Value = machinemtnContent.aMachineTypeMtnContentList.MethodMtn;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = machinemtnContent.aMachineTypeMtnContentList.FrequencyID;
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = machinemtnContent.PageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                                returnMachineTypeMtnContentList.UserID = MyShareInfo.ID;
                                returnMachineTypeMtnContentList.UserName = MyShareInfo.UserName;
                                returnMachineTypeMtnContentList.PageNumber = machinemtnContent.PageNumber;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.PageNumber = machinemtnContent.PageNumber;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                            //get return Totalpage value. 
                        }
                        if (returnMachineTypeMtnContentList.Code == "00")
                        {
                            returnMachineTypeMtnContentList.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList ListByPage(int page,int pageSize)
        {
            List<MachineMtnContentList> lstMachineTypeMtnContentList = null;
            MachineMtnContentList machineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_SelectByPage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = page;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentList = new MachineMtnContentList();
                                    machineMtnContentList.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentList.MachineTypeID = Convert.ToInt16(sqlDr["MachineTypeID"].ToString());
                                    machineMtnContentList.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                    machineMtnContentList.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentList.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentList.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentList.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentList.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtnContentList.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    machineMtnContentList.IsActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    machineMtnContentList.Min = sqlDr["Min"].ToString();
                                    machineMtnContentList.Max = sqlDr["Max"].ToString();
                                    lstMachineTypeMtnContentList.Add(machineMtnContentList);
                                }
                                returnMachineTypeMtnContentList.Code = "00";
                                returnMachineTypeMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = lstMachineTypeMtnContentList;
                                returnMachineTypeMtnContentList.UserID = MyShareInfo.ID;
                                returnMachineTypeMtnContentList.UserName = MyShareInfo.UserName;
                                returnMachineTypeMtnContentList.PageNumber = page;
                            }
                            else
                            {
                                returnMachineTypeMtnContentList.Code = "01";
                                returnMachineTypeMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeMtnContentList.Total = 0;
                                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                            }
                            //get return Totalpage value. 
                        }
                        if (returnMachineTypeMtnContentList.Code == "00")
                            returnMachineTypeMtnContentList.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeMtnContentList;
        }
        public ReturnMachineMtnContentList ImportExcel(Import_MachineMtnType_ContentList import_aMachineMtnType_ContentList)
        {
            ReturnMachineMtnContentList returnMachineTypeMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_ImportExcel";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtnContentList.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = import_aMachineMtnType_ContentList.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@MachinePart", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.MachinePart.Trim();
                        cmd.Parameters.Add(new SqlParameter("@ContentMtn", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.ContentMtn.Trim();
                        cmd.Parameters.Add(new SqlParameter("@ToolMtn", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.ToolMtn.Trim();
                        cmd.Parameters.Add(new SqlParameter("@MethodMtn", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.MethodMtn.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Standard", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.Standard.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Min", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.Min.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Max", SqlDbType.NVarChar)).Value = import_aMachineMtnType_ContentList.Max.Trim();
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = import_aMachineMtnType_ContentList.FrequencyID;
                        int x= cmd.ExecuteNonQuery();

                        returnMachineTypeMtnContentList.Code = "00";
                        returnMachineTypeMtnContentList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeMtnContentList.Code = "99";
                returnMachineTypeMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeMtnContentList.Total = 0;
                returnMachineTypeMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("public ReturnMachineMtnContentList ImportExcel(Import_MachineMtnType_ContentList import_aMachineMtnType_ContentList): ", ex);
            }
            return returnMachineTypeMtnContentList;
        }

        public ReturnMachineMtnContentList CountbyCondition(MachineMtnContentList machineMtnContentList)
        {
            List<MachineMtnContentList> lstMachineMtnContentList = null;
            ReturnMachineMtnContentList returnMachineMtnContentList = new ReturnMachineMtnContentList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnContentList_CountbyCondition";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = machineMtnContentList.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@MachinePartID", SqlDbType.Int)).Value = machineMtnContentList.MachinePartID;
                        cmd.Parameters.Add(new SqlParameter("@ContentMtn", SqlDbType.NVarChar)).Value = machineMtnContentList.ContentMtn;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.Int)).Value = machineMtnContentList.FrequencyID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                //lstMachineMtnContentList = new List<MachineMtnContentList>();
                                while (sqlDr.Read())
                                {
                                    returnMachineMtnContentList.Total = Convert.ToInt16(sqlDr[0].ToString());
                                }
                                returnMachineMtnContentList.Code = "00";
                                returnMachineMtnContentList.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnContentList.lstMachineMtnContentList = lstMachineMtnContentList;
                            }
                            else
                            {
                                returnMachineMtnContentList.Code = "01";
                                returnMachineMtnContentList.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnContentList.Total = 0;
                                returnMachineMtnContentList.lstMachineMtnContentList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnContentList.Code = "99";
                returnMachineMtnContentList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnContentList.Total = 0;
                returnMachineMtnContentList.lstMachineMtnContentList = null;
                mylog4net.Error("public ReturnMachineMtnContentList CountbyCondition(MachineMtnContentList machineMtnContentList) :", ex);
            }
            return returnMachineMtnContentList;
        }
    }
}