using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestABC.Common;
using log4net;
namespace TestABC.Models.Data
{
    public class MachineMtnReportDatasDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnDB));
        //MachinetMtnReport step
        public ReturnMachineMtnReportData getData(MachineMtnReportDataSearch machineMtnReportDataSearch)
        {
            ReturnMachineMtnReportData returnMachineMtnReportData = new ReturnMachineMtnReportData();
            if (String.IsNullOrEmpty(machineMtnReportDataSearch.MachineID))
            {
                returnMachineMtnReportData.Code = "99";
                returnMachineMtnReportData.Message = "Không có MachineID/Input MachineID!  ";
                returnMachineMtnReportData.Total = 0;
                returnMachineMtnReportData.lstMachineMtnReportData = new List<MachineMtnReportData>();
                return returnMachineMtnReportData;
            }

            List<MachineMtnReportData> lstMachineMtnReportData = null;
            MachineMtnReportData MachineMtnReportData = null;

            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnReportDatas";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MachineID", machineMtnReportDataSearch.MachineID);
                        cmd.Parameters.AddWithValue("@OperatorID", machineMtnReportDataSearch.OperatorID);
                        cmd.Parameters.AddWithValue("@Shift", machineMtnReportDataSearch.Shift);
                        cmd.Parameters.AddWithValue("@FrequencyID ", machineMtnReportDataSearch.FrequencyID);
                        if (machineMtnReportDataSearch.MaintenanceDate != DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@MaintenanceDate", SqlDbType.DateTime)).Value = machineMtnReportDataSearch.MaintenanceDate;
                        cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime)).Value = machineMtnReportDataSearch.FromDate;
                        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime)).Value = machineMtnReportDataSearch.ToDate;
                        cmd.Parameters.AddWithValue("@FromInt", machineMtnReportDataSearch.FromInt);
                        cmd.Parameters.AddWithValue("@ToInt", machineMtnReportDataSearch.ToInt);
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtnReportData = new List<MachineMtnReportData>();
                                while (sqlDr.Read())
                                {
                                    MachineMtnReportData = new MachineMtnReportData();
                                    //MachineMtnReportData.MachineID = sqlDr["MachineID"].ToString();
                                    //MachineMtnReportData.MachineName = sqlDr["MachineName"].ToString();
                                    MachineMtnReportData.OperatorID = sqlDr["OperatorID"].ToString();
                                    //MachineMtnReportData.OperatorName = sqlDr["UserName"].ToString();   // UserName in Sql = operatorName in C#
                                    if (!String.IsNullOrEmpty(sqlDr["Shift"].ToString()))
                                        MachineMtnReportData.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    MachineMtnReportData.MaintenanceDate = Convert.ToDateTime(sqlDr["MaintenanceDate"].ToString());
                                    //MachineMtnReportData.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    MachineMtnReportData.TotalResult = sqlDr["TotalResult"] != null ? sqlDr["TotalResult"].ToString() : "";
                                    //if (!String.IsNullOrEmpty(sqlDr["TotalResultContents"].ToString()))
                                    //    MachineMtnReportData.TotalResultContents = sqlDr["TotalResultContents"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["Month"].ToString()))
                                        MachineMtnReportData.Month = Convert.ToInt16(sqlDr["Month"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Year"].ToString()))
                                        MachineMtnReportData.Year = Convert.ToInt16(sqlDr["Year"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Week"].ToString()))
                                        MachineMtnReportData.Week = Convert.ToInt16(sqlDr["Week"].ToString());
                                    MachineMtnReportData.CheckerID = Convert.ToInt16(sqlDr["CheckerID"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["MachinePartID"].ToString()))
                                    {
                                        MachineMtnReportData.MachinePartID = Convert.ToInt16(sqlDr["MachinePartID"].ToString());
                                        MachineMtnReportData.MachinePartName = sqlDr["MachinePartName"].ToString();
                                    }
                                    MachineMtnReportData.ContentMtn = sqlDr["ContentMtn"].ToString();
                                    MachineMtnReportData.ToolMtn = sqlDr["ToolMtn"].ToString();
                                    MachineMtnReportData.MethodMtn = sqlDr["MethodMtn"].ToString();
                                    MachineMtnReportData.Standard = sqlDr["Standard"].ToString();
                                    MachineMtnReportData.MtnDetailResult = sqlDr["MtnDetailResult"].ToString();
                                    MachineMtnReportData.MtnDetailResultContents = sqlDr["MtnDetailResultContents"].ToString();
                                    //get Checker name.
                                    int _checkerID = 0;
                                    MachineMtnReportData.CheckerName = "";
                                    if (Int32.TryParse(sqlDr["CheckerID"].ToString(), out _checkerID))
                                    {
                                        ReturnUser returnUser = (new UserDB()).GetbyID(_checkerID);
                                        if (returnUser.Code == "00")
                                            MachineMtnReportData.CheckerName = returnUser.lstUser[0].UserName;
                                    }
                                    MachineMtnReportData.CheckerResult = sqlDr["CheckerResult"].ToString();
                                    lstMachineMtnReportData.Add(MachineMtnReportData);
                                }
                                returnMachineMtnReportData.Code = "00";
                                returnMachineMtnReportData.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnReportData.lstMachineMtnReportData = lstMachineMtnReportData;
                            }
                            else
                            {
                                returnMachineMtnReportData.Code = "01";
                                returnMachineMtnReportData.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnReportData.Total = 0;
                                returnMachineMtnReportData.lstMachineMtnReportData = null;
                            }
                        }
                        //paging.
                        //if (returnMachineMtnReportData.Code == "00")
                        //    returnMachineMtnReportData.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnReportData.Code = "99";
                returnMachineMtnReportData.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnReportData.Total = 0;
                returnMachineMtnReportData.lstMachineMtnReportData = null;
                mylog4net.Error("public ReturnMachineMtnReportData SelectByCondition(string where, int pageSize) ", ex);
            }
            return returnMachineMtnReportData;
        }

        public void CreateReport(MachineMtnReportDataSearch machineMtnReportDataSearch)
        {
            
            #region get data.
            ReturnMachineMtnReportData reportData = new ReturnMachineMtnReportData();
            reportData = getData(machineMtnReportDataSearch);
            #endregion
            #region Create simple group report excel.
            //create excel file.

            //group report.
            //-- Group MachinePartName.

            //-- Group Machine.


            //insert to excel.

            #endregion

            #region merge excel.

            #endregion
        }
    }


}