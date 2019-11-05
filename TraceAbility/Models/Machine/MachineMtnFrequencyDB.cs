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
    public class MachineMtnFrequencyDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnFrequencyDB));
        public ReturnMachineMtnFrequencyView MachineMtnFrequencyAll()
        {
            List<MachineMtnFrequencyView> lstMachineMtnFrequencyView = null;
            MachineMtnFrequencyView machineMtnFrequencyView = null;
            ReturnMachineMtnFrequencyView returnMachineMtnFrequencyView = new ReturnMachineMtnFrequencyView();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnFrequency_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtnFrequencyView = new List<MachineMtnFrequencyView>();
                                while (sqlDr.Read())
                                {
                                    machineMtnFrequencyView = new MachineMtnFrequencyView();
                                    machineMtnFrequencyView.MachineID = sqlDr["MachineID"].ToString();
                                    machineMtnFrequencyView.Daily = Convert.ToInt16(sqlDr["TanSuat1"].ToString());
                                    machineMtnFrequencyView.Weekly = Convert.ToInt16(sqlDr["TanSuat2"].ToString());
                                    machineMtnFrequencyView.Monthly = Convert.ToInt16(sqlDr["TanSuat3"].ToString());
                                    machineMtnFrequencyView.ThreeMonths = Convert.ToInt16(sqlDr["TanSuat4"].ToString());
                                    machineMtnFrequencyView.SixMonths = Convert.ToInt16(sqlDr["TanSuat5"].ToString());
                                    machineMtnFrequencyView.Yearly = Convert.ToInt16(sqlDr["TanSuat6"].ToString());
                                    lstMachineMtnFrequencyView.Add(machineMtnFrequencyView);
                                }
                                returnMachineMtnFrequencyView.Code = "00";
                                returnMachineMtnFrequencyView.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnFrequencyView.lstMachineMtnFrequencyView = lstMachineMtnFrequencyView;
                            }
                            else
                            {
                                returnMachineMtnFrequencyView.Code = "01";
                                returnMachineMtnFrequencyView.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnFrequencyView.Total = 0;
                                returnMachineMtnFrequencyView.lstMachineMtnFrequencyView = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnFrequencyView.Code = "99";
                returnMachineMtnFrequencyView.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnFrequencyView.Total = 0;
                returnMachineMtnFrequencyView.lstMachineMtnFrequencyView = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnFrequencyView;
        }

        public ReturnMachineMtnFrequency GetbyMachineID(string MachineID)
        {
            List<MachineMtnFrequency> lstMachineMtnFrequency = null;
            MachineMtnFrequency machineMtnFrequency = null;
            ReturnMachineMtnFrequency returnMachineMtnFrequency = new ReturnMachineMtnFrequency();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineMtnFrequency_SelectByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtnFrequency = new List<MachineMtnFrequency>();
                                while (sqlDr.Read())
                                {
                                    machineMtnFrequency = new MachineMtnFrequency();
                                    machineMtnFrequency.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtnFrequency.MachineID = sqlDr["MachineID"].ToString();
                                    machineMtnFrequency.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());

                                    lstMachineMtnFrequency.Add(machineMtnFrequency);
                                }
                                returnMachineMtnFrequency.Code = "00";
                                returnMachineMtnFrequency.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtnFrequency.lstMachineMtnFrequency = lstMachineMtnFrequency;
                            }
                            else
                            {
                                returnMachineMtnFrequency.Code = "01";
                                returnMachineMtnFrequency.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtnFrequency.Total = 0;
                                returnMachineMtnFrequency.lstMachineMtnFrequency = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnFrequency.Code = "99";
                returnMachineMtnFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnFrequency.Total = 0;
                returnMachineMtnFrequency.lstMachineMtnFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnFrequency;
        }

        //public ReturnMachineMtnFrequency Insert(MachineMtnFrequency machineMtnFrequency)
        //{
        //    ReturnMachineMtnFrequency returnMachineMtnFrequency = new ReturnMachineMtnFrequency();
        //    try
        //    {
        //        using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("", sqlConnection))
        //            {
        //                cmd.CommandText = "sp_tMachineMtnFrequency_InsertUpdate";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtnFrequency.ID;
        //                cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineMtnFrequency.MachineID;
        //                cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = machineMtnFrequency.FrequencyID;
        //                cmd.ExecuteNonQuery();

        //                returnMachineMtnFrequency.Code = "00";
        //                returnMachineMtnFrequency.Message = "Cập nhật dữ liệu thành công.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        returnMachineMtnFrequency.Code = "99";
        //        returnMachineMtnFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
        //        returnMachineMtnFrequency.Total = 0;
        //        returnMachineMtnFrequency.lstMachineMtnFrequency = null;
        //    }
        //    return returnMachineMtnFrequency;
        //}

        public ReturnMachineMtnFrequency Insert(MachineMtnFrequency machineMtnFrequency)
        {
            ReturnMachineMtnFrequency returnMachineMtnFrequency = new ReturnMachineMtnFrequency();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnFrequency_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtnFrequency.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineMtnFrequency.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = machineMtnFrequency.FrequencyID;
                        cmd.ExecuteNonQuery();

                        returnMachineMtnFrequency.Code = "00";
                        returnMachineMtnFrequency.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnFrequency.Code = "99";
                returnMachineMtnFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnFrequency.Total = 0;
                returnMachineMtnFrequency.lstMachineMtnFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnFrequency;
        }

        public ReturnMachineMtnFrequency DeleteByMachineID(string MachineID)
        {
            ReturnMachineMtnFrequency returnMachineMtnFrequency = new ReturnMachineMtnFrequency();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtnFrequency_DeleteByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;
                        cmd.ExecuteNonQuery();
                        returnMachineMtnFrequency.Code = "00";
                        returnMachineMtnFrequency.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtnFrequency.Code = "99";
                returnMachineMtnFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtnFrequency.Total = 0;
                returnMachineMtnFrequency.lstMachineMtnFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtnFrequency;
        }
    }
}