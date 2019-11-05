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
    public class FPBCheckingItemDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(FPBCheckingItemDB));

        public ReturnFPBCheckingItem ListAll()
        {
            List<FPBCheckingItem> lstFPBCheckingItem = null;
            FPBCheckingItem FPBCheckingItem = null;
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingItem_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingItem = new List<FPBCheckingItem>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingItem = new FPBCheckingItem();
                                    //FPBCheckingItem.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPBCheckingItem.MachineID = sqlDr["MachineID"].ToString();
                                    //FPBCheckingItem.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    FPBCheckingItem.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());
                                    //FPBCheckingItem.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstFPBCheckingItem.Add(FPBCheckingItem);
                                }
                                returnFPBCheckingItem.Code = "00";
                                returnFPBCheckingItem.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingItem.LstFPBCheckingItem = lstFPBCheckingItem;
                            }
                            else
                            {
                                returnFPBCheckingItem.Code = "01";
                                returnFPBCheckingItem.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingItem.Total = 0;
                                returnFPBCheckingItem.LstFPBCheckingItem = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }

        public ReturnFPBCheckingItem GetbyID(string IDFPBCheckingItem)
        {
            List<FPBCheckingItem> lstFPBCheckingItem = null;
            FPBCheckingItem FPBCheckingItem = null;
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBCheckingItem_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IDFPBCheckingItem", SqlDbType.Int)).Value = int.Parse(IDFPBCheckingItem);

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingItem = new List<FPBCheckingItem>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingItem = new FPBCheckingItem();
                                    FPBCheckingItem.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPBCheckingItem.MachineID = sqlDr["MachineID"].ToString();
                                    FPBCheckingItem.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    FPBCheckingItem.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());
                                    FPBCheckingItem.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstFPBCheckingItem.Add(FPBCheckingItem);
                                }
                                returnFPBCheckingItem.Code = "00";
                                returnFPBCheckingItem.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingItem.LstFPBCheckingItem = lstFPBCheckingItem;
                            }
                            else
                            {
                                returnFPBCheckingItem.Code = "01";
                                returnFPBCheckingItem.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingItem.Total = 0;
                                returnFPBCheckingItem.LstFPBCheckingItem = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }
        public ReturnFPBCheckingItem GetbyMachineID(string MachineID, int FrequencyID)
        {
            List<FPBCheckingItem> lstFPBCheckingItem = null;
            FPBCheckingItem FPBCheckingItem = null;
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBCheckingItem_SelectByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.Int)).Value = FrequencyID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingItem = new List<FPBCheckingItem>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingItem = new FPBCheckingItem();
                                    FPBCheckingItem.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPBCheckingItem.MachineID = sqlDr["MachineID"].ToString();
                                    FPBCheckingItem.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    FPBCheckingItem.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());
                                    FPBCheckingItem.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstFPBCheckingItem.Add(FPBCheckingItem);
                                }
                                returnFPBCheckingItem.Code = "00";
                                returnFPBCheckingItem.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingItem.LstFPBCheckingItem = lstFPBCheckingItem;
                            }
                            else
                            {
                                returnFPBCheckingItem.Code = "01";
                                returnFPBCheckingItem.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingItem.Total = 0;
                                returnFPBCheckingItem.LstFPBCheckingItem = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }
        public ReturnFPBCheckingItem Insert(FPBCheckingItem FPBCheckingItem)
        {
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingItem_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IDFPBCheckingItem", SqlDbType.Int)).Value = FPBCheckingItem.IDFPBCheckingItem;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = FPBCheckingItem.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@CheckingItemName", SqlDbType.NVarChar)).Value = FPBCheckingItem.CheckingItemName;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.Int)).Value = FPBCheckingItem.FrequencyID;
                        cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit)).Value = 1;

                        cmd.ExecuteNonQuery();

                        returnFPBCheckingItem.Code = "00";
                        returnFPBCheckingItem.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }


        public ReturnFPBCheckingItem DeleteByID(int _ID)
        {
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingItem_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IDFPBCheckingItem", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnFPBCheckingItem.Code = "00";
                        returnFPBCheckingItem.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }
        public ReturnFPBCheckingItem DeleteByMachineID(string MachineID, string FrequencyID)
        {
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingItem_DeleteByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.Int)).Value = int.Parse(FrequencyID);
                        cmd.ExecuteNonQuery();
                        returnFPBCheckingItem.Code = "00";
                        returnFPBCheckingItem.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }
        public ReturnFPBCheckingItem GetFPBCheckingItemName(string MachineID)
        {
            List<FPBCheckingItem> lstFPBCheckingItem = null;
            FPBCheckingItem FPBCheckingItem = null;
            ReturnFPBCheckingItem returnFPBCheckingItem = new ReturnFPBCheckingItem();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBCheckingItem_GetFPBCheckingItemName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingItem = new List<FPBCheckingItem>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingItem = new FPBCheckingItem();
                                    FPBCheckingItem.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPBCheckingItem.MachineID = sqlDr["MachineID"].ToString();
                                    FPBCheckingItem.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    FPBCheckingItem.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());

                                    lstFPBCheckingItem.Add(FPBCheckingItem);
                                }
                                returnFPBCheckingItem.Code = "00";
                                returnFPBCheckingItem.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingItem.LstFPBCheckingItem = lstFPBCheckingItem;
                                returnFPBCheckingItem.UserID = MyShareInfo.ID;
                                returnFPBCheckingItem.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnFPBCheckingItem.Code = "01";
                                returnFPBCheckingItem.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingItem.Total = 0;
                                returnFPBCheckingItem.LstFPBCheckingItem = null;
                                returnFPBCheckingItem.UserID = MyShareInfo.ID;
                                returnFPBCheckingItem.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingItem.Code = "99";
                returnFPBCheckingItem.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingItem.Total = 0;
                returnFPBCheckingItem.LstFPBCheckingItem = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingItem;
        }
    }
}