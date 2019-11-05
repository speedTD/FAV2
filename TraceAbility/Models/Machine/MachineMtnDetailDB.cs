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
    public class MachineMtnDetailDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnDetailDB));
        public ReturnMachineMtnDetail Insert(MachineMtnDetail machineMtnDetail)
        {
            ReturnMachineMtnDetail returnMachineMtnDetail = new ReturnMachineMtnDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnDetail_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtnDetail.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineMtnID", SqlDbType.Int)).Value = machineMtnDetail.MachineMtnID;
                        cmd.Parameters.Add(new SqlParameter("@MachineMtnContentID", SqlDbType.Int)).Value = machineMtnDetail.MachineMtnContentID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = machineMtnDetail.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContents", SqlDbType.NVarChar)).Value = machineMtnDetail.ResultContents;
                                                             
                        cmd.Parameters.Add(new SqlParameter("@ContentMtn", SqlDbType.NVarChar)).Value = machineMtnDetail.ContentMtn;
                        cmd.Parameters.Add(new SqlParameter("@ToolMtn", SqlDbType.NVarChar)).Value = machineMtnDetail.ToolMtn;
                        cmd.Parameters.Add(new SqlParameter("@MethodMtn", SqlDbType.NVarChar)).Value = machineMtnDetail.MethodMtn;
                        cmd.Parameters.Add(new SqlParameter("@Standard", SqlDbType.NVarChar)).Value = machineMtnDetail.Standard;
                        cmd.Parameters.Add(new SqlParameter("@Min", SqlDbType.NVarChar)).Value = machineMtnDetail.Min;
                        cmd.Parameters.Add(new SqlParameter("@Max", SqlDbType.NVarChar)).Value = machineMtnDetail.Max;
                        cmd.Parameters.Add(new SqlParameter("@ActualValue", SqlDbType.NVarChar)).Value = machineMtnDetail.ActualValue;
                        
                        cmd.ExecuteNonQuery();
                        returnMachineMtnDetail.Code = "00";
                        returnMachineMtnDetail.Message = "Cập nhật dữ liệu thành công.";
                    }
                }

            }
            catch (Exception ex)
            {
                returnMachineMtnDetail.Code = "99";
                returnMachineMtnDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnDetail.Total = 0;
                returnMachineMtnDetail.lstMachineMtnDetail = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnDetail;
        }
        public ReturnMachineMtnContentDetail SelectByMachineMtnID(int MachineMtnID)
        {
            List<MachineMtnContentDetail> lstMachineMtnContentDetail = null;
            MachineMtnContentDetail machineMtnContentDetail = null;
            ReturnMachineMtnContentDetail returnMachineMtnContentDetail = new ReturnMachineMtnContentDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnDetail_SelectByMachineMtnID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineMtnID", SqlDbType.Int)).Value = MachineMtnID;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtnContentDetail = new List<MachineMtnContentDetail>();
                                while (sqlDr.Read())
                                {
                                    machineMtnContentDetail = new MachineMtnContentDetail();
                                    machineMtnContentDetail.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnContentDetail.MachineMtnID = Convert.ToInt16(sqlDr["MachineMtnID"].ToString());
                                    machineMtnContentDetail.MachineMtnContentID = Convert.ToInt16(sqlDr["MachineMtnContentID"].ToString());
                                    machineMtnContentDetail.Result = sqlDr["Result"].ToString();
                                    machineMtnContentDetail.ResultContents = sqlDr["ResultContents"].ToString();
                                    machineMtnContentDetail.MachinePart = sqlDr["MachinePartID"].ToString();
                                    machineMtnContentDetail.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    machineMtnContentDetail.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    machineMtnContentDetail.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    machineMtnContentDetail.Standard = sqlDr["Standard"].ToString();
                                    machineMtnContentDetail.MachinePartName = sqlDr["Name"].ToString();
                                    machineMtnContentDetail.Min = sqlDr["Min"].ToString();
                                    machineMtnContentDetail.Max = sqlDr["Max"].ToString();
                                    machineMtnContentDetail.ActualValue = sqlDr["ActualValue"].ToString();
                                    lstMachineMtnContentDetail.Add(machineMtnContentDetail);
                                }
                                returnMachineMtnContentDetail.Code = "00";
                                returnMachineMtnContentDetail.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnContentDetail.lstMachineMtnContentDetail = lstMachineMtnContentDetail;
                            }
                            else
                            {
                                returnMachineMtnContentDetail.Code = "01";
                                returnMachineMtnContentDetail.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnContentDetail.Total = 0;
                                returnMachineMtnContentDetail.lstMachineMtnContentDetail = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnContentDetail.Code = "99";
                returnMachineMtnContentDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnContentDetail.Total = 0;
                returnMachineMtnContentDetail.lstMachineMtnContentDetail = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnContentDetail;
        }

        public ReturnMachineMtnDetail CountbyMachineMtnContentID(int machineMtnContentID)
        {
            //List<int> lstint = null;
            ReturnMachineMtnDetail returnMachineMtnDetail = new ReturnMachineMtnDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnDetail_CountbyMachineMtnContentID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineMtnContentID", SqlDbType.Int)).Value = machineMtnContentID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                //lstint = new List<int>();
                                while (sqlDr.Read())
                                {
                                    returnMachineMtnDetail.Total = Convert.ToInt16(sqlDr[0].ToString());
                                }
                                returnMachineMtnDetail.Code = "00";
                                returnMachineMtnDetail.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnDetail.lstMachineMtnDetail = null;
                            }
                            else
                            {
                                returnMachineMtnDetail.Code = "01";
                                returnMachineMtnDetail.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnDetail.Total = 0;
                                returnMachineMtnDetail.lstMachineMtnDetail = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnDetail.Code = "99";
                returnMachineMtnDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnDetail.Total = 0;
                returnMachineMtnDetail.lstMachineMtnDetail = null;
                mylog4net.Error("public ReturnMachineMtnDetail CountbyCondition(int machineMtnContentID) :", ex);
            }
            return returnMachineMtnDetail;
        }
    }
}