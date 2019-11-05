using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestABC.Common;
using TestABC.Models.Data;

using log4net;

namespace TestABC.Models.Data
{
    public class ProductionControlDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProductionControlDB));

        public ReturnProductionControl ProductionControlAll()
        {
            List<ProductionControl> lstProductionControl = null;
            ProductionControl productionControl = null;
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstProductionControl = new List<ProductionControl>();
                                while (sqlDr.Read())
                                {
                                    productionControl = new ProductionControl();
                                    productionControl.ID = int.Parse(sqlDr["ID"].ToString());
                                    productionControl.IndicationID = sqlDr["IndicationID"].ToString();
                                    productionControl.MachineID = sqlDr["MachineID"].ToString();
                                    productionControl.MachineName = sqlDr["MachineName"].ToString();
                                    productionControl.PdtCtrlDateTime = Convert.ToDateTime(sqlDr["PdtCtrlDateTime"].ToString());
                                    productionControl.UserID = sqlDr["UserID"].ToString();
                                    productionControl.UserName = sqlDr["UserName"].ToString();
                                    productionControl.ItemName = sqlDr["ItemName"].ToString();
                                    productionControl.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionControl.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionControl.SeqNo = sqlDr["SeqNo"].ToString();
                                    productionControl.Result = sqlDr["Result"].ToString();
                                    productionControl.ProgramName = sqlDr["ProgramName"].ToString();
                                    
                                    lstProductionControl.Add(productionControl);
                                }
                                returnProductionControl.Code = "00";
                                returnProductionControl.Message = "Lấy dữ liệu thành công.";
                                returnProductionControl.lstProductionControl = lstProductionControl;
                                returnProductionControl.userID = MyShareInfo.ID;
                                returnProductionControl.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProductionControl.Code = "01";
                                returnProductionControl.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControl.Total = 0;
                                returnProductionControl.lstProductionControl = null;
                                returnProductionControl.userID = MyShareInfo.ID;
                                returnProductionControl.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControl;
        }
        public ReturnProductionControl SearchProductionControl(string where)
        {
            List<ProductionControl> lstProductionControl = null;
            ProductionControl productionControl = null;
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_SearchProductionControl";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionControl = new List<ProductionControl>();
                                while (sqlDr.Read())
                                {
                                    productionControl = new ProductionControl();
                                    productionControl.ID = int.Parse(sqlDr["ID"].ToString());
                                    productionControl.IndicationID = sqlDr["IndicationID"].ToString();
                                    productionControl.MachineID = sqlDr["MachineID"].ToString();
                                    productionControl.MachineName = sqlDr["MachineName"].ToString();
                                    productionControl.PdtCtrlDateTime = Convert.ToDateTime(sqlDr["PdtCtrlDateTime"].ToString());
                                    productionControl.UserID = sqlDr["UserID"].ToString();
                                    productionControl.UserName = sqlDr["UserName"].ToString();
                                    productionControl.ItemName = sqlDr["ItemName"].ToString();
                                    productionControl.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionControl.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionControl.SeqNo = sqlDr["SeqNo"].ToString();
                                    productionControl.Result = sqlDr["Result"].ToString();
                                    productionControl.ProgramName = sqlDr["ProgramName"].ToString();
                                    lstProductionControl.Add(productionControl);
                                }
                                returnProductionControl.Code = "00";
                                returnProductionControl.Message = "Lấy dữ liệu thành công.";
                                returnProductionControl.lstProductionControl = lstProductionControl;
                                returnProductionControl.userID = MyShareInfo.ID;
                                returnProductionControl.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProductionControl.Code = "01";
                                returnProductionControl.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControl.Total = 0;
                                returnProductionControl.lstProductionControl = null;
                                returnProductionControl.userID = MyShareInfo.ID;
                                returnProductionControl.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControl;
        }
        public ReturnProductionControl SearchProductionControl_UsingDataTables(SearchProductionControl searchProductionControl, int pageNumber,int pageSize)
        {
            List<ProductionControl> lstProductionControl = null;
            ProductionControl productionControl = null;
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_SearchProductionControl";
                        cmd.Parameters.AddWithValue("@MachineID", searchProductionControl.MachineID);
                        cmd.Parameters.AddWithValue("@IndicationID", searchProductionControl.IndicationID);
                        cmd.Parameters.AddWithValue("@ProgramName", searchProductionControl.ProgramName);
                        cmd.Parameters.AddWithValue("@Result", searchProductionControl.Result);
                        if(searchProductionControl.FromDate != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", searchProductionControl.FromDate);
                        if (searchProductionControl.ToDate != DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", searchProductionControl.ToDate);

                        //paging
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionControl = new List<ProductionControl>();
                                while (sqlDr.Read())
                                {
                                    productionControl = new ProductionControl();
                                    productionControl.ID = int.Parse(sqlDr["ID"].ToString());
                                    productionControl.IndicationID = sqlDr["IndicationID"].ToString();
                                    productionControl.MachineID = sqlDr["MachineID"].ToString();
                                    productionControl.MachineName = sqlDr["MachineName"].ToString();
                                    productionControl.PdtCtrlDateTime = Convert.ToDateTime(sqlDr["PdtCtrlDateTime"].ToString());
                                    productionControl.UserID = sqlDr["UserID"].ToString();
                                    productionControl.UserName = sqlDr["UserName"].ToString();
                                    productionControl.ItemName = sqlDr["ItemName"].ToString();
                                    productionControl.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionControl.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionControl.SeqNo = sqlDr["SeqNo"].ToString();
                                    productionControl.Result = sqlDr["Result"].ToString();
                                    productionControl.ProgramName = sqlDr["ProgramName"].ToString();
                                    lstProductionControl.Add(productionControl);
                                }
                                returnProductionControl.Code = "00";
                                returnProductionControl.Message = "Lấy dữ liệu thành công.";
                                returnProductionControl.lstProductionControl = lstProductionControl;
                                
                            }
                            else
                            {
                                returnProductionControl.Code = "01";
                                returnProductionControl.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControl.Total = 0;
                                returnProductionControl.lstProductionControl = null;
                            
                            }
                            
                        }
                        //paging.
                        if (returnProductionControl.Code == "00")
                            returnProductionControl.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControl;
        }
        public ReturnProductionControl GetbyID(int ID)
        {
            List<ProductionControl> lstProductionControl = null;
            ProductionControl productionControl = null;
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionControl_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionControl = new List<ProductionControl>();
                                while (sqlDr.Read())
                                {
                                    productionControl = new ProductionControl();
                                    productionControl.ID = int.Parse(sqlDr["ID"].ToString());
                                    productionControl.IndicationID = sqlDr["IndicationID"].ToString();
                                    productionControl.MachineID = sqlDr["MachineID"].ToString();
                                    productionControl.MachineName = sqlDr["MachineName"].ToString();
                                    productionControl.PdtCtrlDateTime = Convert.ToDateTime(sqlDr["PdtCtrlDateTime"].ToString());
                                    productionControl.UserID = sqlDr["UserID"].ToString();
                                    productionControl.UserName = sqlDr["UserName"].ToString();
                                    productionControl.ItemName = sqlDr["ItemName"].ToString();
                                    productionControl.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionControl.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionControl.SeqNo = sqlDr["SeqNo"].ToString();
                                    productionControl.Result = sqlDr["Result"].ToString();
                                    productionControl.ProgramName = sqlDr["ProgramName"].ToString();
                                    
                                    lstProductionControl.Add(productionControl);
                                }
                                returnProductionControl.Code = "00";
                                returnProductionControl.Message = "Lấy dữ liệu thành công.";
                                returnProductionControl.lstProductionControl = lstProductionControl;
                                //}
                            }
                            else
                            {
                                returnProductionControl.Code = "01";
                                returnProductionControl.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControl.Total = 0;
                                returnProductionControl.lstProductionControl = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControl;
        }
        public ReturnProductionControl Insert(ProductionControl productionControl)
        {
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = productionControl.ID;
                        cmd.Parameters.Add(new SqlParameter("@IndicationID", SqlDbType.VarChar)).Value = productionControl.IndicationID;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = productionControl.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@PdtCtrlDateTime", SqlDbType.DateTime)).Value = productionControl.PdtCtrlDateTime;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.VarChar)).Value = productionControl.UserID;
                        cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar)).Value = productionControl.ItemName;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value = productionControl.ItemCode;
                        cmd.Parameters.Add(new SqlParameter("@BatchNo", SqlDbType.VarChar)).Value = productionControl.BatchNo;
                        cmd.Parameters.Add(new SqlParameter("@SeqNo", SqlDbType.NVarChar)).Value = productionControl.SeqNo;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = productionControl.Result;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = productionControl.ProgramName;
                        cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        returnProductionControl.Code = "00";
                        returnProductionControl.Message = "Cập nhật dữ liệu thành công.";
                        if (returnProductionControl.Code == "00")
                            returnProductionControl.LastID = Convert.ToInt32(cmd.Parameters["@LastID"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.LastID = -1;
                returnProductionControl.lstProductionControl = null;
            }
            return returnProductionControl;
        }
        public ReturnProductionControl DeleteByID(int _ID)
        {
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProductionControl.Code = "00";
                        returnProductionControl.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControl;
        }

        public ReturnProductionControl CountBySelection(string where)
        {
            List<ProductionControl> lstProductionControl = null;
            ReturnProductionControl returnProductionControl = new ReturnProductionControl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControl_CountBySelection";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.NVarChar)).Value = productionControl.MachineID;
                        //cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar)).Value = productionControl.ProgramName;
                        //cmd.Parameters.Add(new SqlParameter("@PdtCtrlDateTime", SqlDbType.VarChar)).Value = productionControl.PdtCtrlDateTime;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionControl = new List<ProductionControl>();
                                while (sqlDr.Read())
                                {
                                    returnProductionControl.Total = Convert.ToInt16(sqlDr[0].ToString());
                                }
                                returnProductionControl.Code = "00";
                                returnProductionControl.Message = "Lấy dữ liệu thành công.";
                                returnProductionControl.lstProductionControl = lstProductionControl;
                            }
                            else
                            {
                                returnProductionControl.Code = "01";
                                returnProductionControl.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControl.Total = 0;
                                returnProductionControl.lstProductionControl = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControl.Code = "99";
                returnProductionControl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControl.Total = 0;
                returnProductionControl.lstProductionControl = null;
                mylog4net.Error("public ReturnProductionControl CountProductionControlBySelection(ProductionControl productionControl) ", ex);
            }
            return returnProductionControl;
        }
    }
}