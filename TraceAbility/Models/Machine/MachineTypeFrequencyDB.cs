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
    public class MachineTypeFrequencyDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineTypeFrequencyDB));
        public ReturnMachineTypeFrequencyView MachineTypeFrequencyAll()
        {
            List<MachineTypeFrequencyView> lstMachineTypeFrequencyView = null;
            MachineTypeFrequencyView machineTypeFrequencyView = null;
            ReturnMachineTypeFrequencyView returnMachineTypeFrequencyView = new ReturnMachineTypeFrequencyView();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineTypeFrequency_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeFrequencyView = new List<MachineTypeFrequencyView>();
                                while (sqlDr.Read())
                                {
                                    machineTypeFrequencyView = new MachineTypeFrequencyView();
                                    machineTypeFrequencyView.MachineTypeID = sqlDr["MachineTypeID"].ToString();
                                    machineTypeFrequencyView.Daily = Convert.ToInt16(sqlDr["TanSuat1"].ToString());
                                    machineTypeFrequencyView.Weekly = Convert.ToInt16(sqlDr["TanSuat2"].ToString());
                                    machineTypeFrequencyView.Monthly = Convert.ToInt16(sqlDr["TanSuat3"].ToString());
                                    machineTypeFrequencyView.ThreeMonths = Convert.ToInt16(sqlDr["TanSuat4"].ToString());
                                    machineTypeFrequencyView.SixMonths = Convert.ToInt16(sqlDr["TanSuat5"].ToString());
                                    machineTypeFrequencyView.Yearly = Convert.ToInt16(sqlDr["TanSuat6"].ToString());
                                    lstMachineTypeFrequencyView.Add(machineTypeFrequencyView);
                                }
                                returnMachineTypeFrequencyView.Code = "00";
                                returnMachineTypeFrequencyView.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeFrequencyView.lstMachineTypeFrequencyView = lstMachineTypeFrequencyView;
                            }
                            else
                            {
                                returnMachineTypeFrequencyView.Code = "01";
                                returnMachineTypeFrequencyView.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeFrequencyView.Total = 0;
                                returnMachineTypeFrequencyView.lstMachineTypeFrequencyView = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeFrequencyView.Code = "99";
                returnMachineTypeFrequencyView.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeFrequencyView.Total = 0;
                returnMachineTypeFrequencyView.lstMachineTypeFrequencyView = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeFrequencyView;
        }

        public ReturnMachineTypeFrequency GetbyMachineTypeID(int MachineTypeID)
        {
            List<MachineTypeFrequency> lstMachineTypeFrequency = null;
            MachineTypeFrequency machineTypeFrequency = null;
            ReturnMachineTypeFrequency returnMachineTypeFrequency = new ReturnMachineTypeFrequency();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineTypeFrequency_SelectByMachineTypeID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = MachineTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineTypeFrequency = new List<MachineTypeFrequency>();
                                while (sqlDr.Read())
                                {
                                    machineTypeFrequency = new MachineTypeFrequency();
                                    machineTypeFrequency.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineTypeFrequency.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    machineTypeFrequency.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());

                                    lstMachineTypeFrequency.Add(machineTypeFrequency);
                                }
                                returnMachineTypeFrequency.Code = "00";
                                returnMachineTypeFrequency.Message = "Lấy dữ liệu thành công.";
                                returnMachineTypeFrequency.lstMachineTypeFrequency = lstMachineTypeFrequency;
                                returnMachineTypeFrequency.Total = lstMachineTypeFrequency.Count;
                            }
                            else
                            {
                                returnMachineTypeFrequency.Code = "01";
                                returnMachineTypeFrequency.Message = "Không tồn tại bản ghi nào.";
                                returnMachineTypeFrequency.Total = 0;
                                returnMachineTypeFrequency.lstMachineTypeFrequency = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeFrequency.Code = "99";
                returnMachineTypeFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeFrequency.Total = 0;
                returnMachineTypeFrequency.lstMachineTypeFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeFrequency;
        }

        public ReturnMachineTypeFrequency Insert(MachineTypeFrequency machineTypeFrequency)
        {
            ReturnMachineTypeFrequency returnMachineTypeFrequency = new ReturnMachineTypeFrequency();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineTypeFrequency_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineTypeFrequency.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = machineTypeFrequency.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.SmallInt)).Value = machineTypeFrequency.FrequencyID;
                        cmd.ExecuteNonQuery();

                        returnMachineTypeFrequency.Code = "00";
                        returnMachineTypeFrequency.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeFrequency.Code = "99";
                returnMachineTypeFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeFrequency.Total = 0;
                returnMachineTypeFrequency.lstMachineTypeFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeFrequency;
        }

        public ReturnMachineTypeFrequency DeleteByMachineTypeID(int MachineTypeID)
        {
            ReturnMachineTypeFrequency returnMachineTypeFrequency = new ReturnMachineTypeFrequency();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineTypeFrequency_DeleteByMachineTypeID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = MachineTypeID;
                        cmd.ExecuteNonQuery();
                        returnMachineTypeFrequency.Code = "00";
                        returnMachineTypeFrequency.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineTypeFrequency.Code = "99";
                returnMachineTypeFrequency.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineTypeFrequency.Total = 0;
                returnMachineTypeFrequency.lstMachineTypeFrequency = null;
                mylog4net.Error("", ex);
            }
            return returnMachineTypeFrequency;
        }
    }
}