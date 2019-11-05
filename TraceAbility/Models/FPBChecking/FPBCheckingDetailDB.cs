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
    public class FPBCheckingDetailDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(FPBCheckingDetailDB));
        public ReturnFPBCheckingDetail Insert(FPBCheckingDetail FPBCheckingDetail)
        {
            ReturnFPBCheckingDetail returnFPBCheckingDetail = new ReturnFPBCheckingDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingDetail_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = FPBCheckingDetail.ID;
                        cmd.Parameters.Add(new SqlParameter("@FPBCheckingID", SqlDbType.Int)).Value = FPBCheckingDetail.FPBCheckingID;
                        cmd.Parameters.Add(new SqlParameter("@Images", SqlDbType.Text)).Value = FPBCheckingDetail.Images;
                        //cmd.Parameters.Add(new SqlParameter("@FPBCheckingItemID", SqlDbType.Int)).Value = FPBCheckingDetail.FPBCheckingItemID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = FPBCheckingDetail.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContent", SqlDbType.NVarChar)).Value = FPBCheckingDetail.ResultContent;
                        cmd.ExecuteNonQuery();

                        returnFPBCheckingDetail.Code = "00";
                        returnFPBCheckingDetail.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingDetail.Code = "99";
                returnFPBCheckingDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingDetail.Total = 0;
                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                mylog4net.Error("", ex);
            }
            return returnFPBCheckingDetail;
        }
        public ReturnFPBCheckingDetail SelectByFPBCheckingID(int FPBCheckingID)
        {
            List<FPBCheckingDetail> lstFPBCheckingDetail = null;
            FPBCheckingDetail FPBCheckingDetail = null;
            ReturnFPBCheckingDetail returnFPBCheckingDetail = new ReturnFPBCheckingDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBCheckingDetail_SelectByFPBCheckingID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FPBCheckingID", SqlDbType.Int)).Value = FPBCheckingID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingDetail = new List<FPBCheckingDetail>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingDetail = new FPBCheckingDetail();
                                    FPBCheckingDetail.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPBCheckingDetail.FPBCheckingID = int.Parse(sqlDr["FPBCheckingID"].ToString());
                                    FPBCheckingDetail.Images = sqlDr["Images"].ToString();
                                    //FPBCheckingDetail.FPBCheckingItemID = int.Parse(sqlDr["FPBCheckingItemID"].ToString());
                                    FPBCheckingDetail.Result = sqlDr["Result"].ToString();
                                    FPBCheckingDetail.ResultContent = sqlDr["ResultContent"].ToString();
                                    //FPBCheckingDetail.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    //FPBCheckingDetail.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());
                                    lstFPBCheckingDetail.Add(FPBCheckingDetail);
                                }
                                returnFPBCheckingDetail.Code = "00";
                                returnFPBCheckingDetail.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingDetail.LstFPBCheckingDetail = lstFPBCheckingDetail;
                            }
                            else
                            {
                                returnFPBCheckingDetail.Code = "01";
                                returnFPBCheckingDetail.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingDetail.Total = 0;
                                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingDetail.Code = "99";
                returnFPBCheckingDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingDetail.Total = 0;
                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                mylog4net.Error("SelectByFPBCheckingID", ex);

            }
            return returnFPBCheckingDetail;
        }

        public ReturnFPBCheckingDetail LoadFPBCheckingDetail_ByFPBCheckingID(int FPBCheckingID)
        {
            List<FPBCheckingDetail> lstFPBCheckingDetail = null;
            FPBCheckingDetail FPBCheckingDetail = null;
            ReturnFPBCheckingDetail returnFPBCheckingDetail = new ReturnFPBCheckingDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBCheckingDetail_LoadByFPBCheckingID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@FPBCheckingID", SqlDbType.Int)).Value = FPBCheckingID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBCheckingDetail = new List<FPBCheckingDetail>();
                                while (sqlDr.Read())
                                {
                                    FPBCheckingDetail = new FPBCheckingDetail();
                                    FPBCheckingDetail.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPBCheckingDetail.FPBCheckingID = int.Parse(sqlDr["FPBCheckingID"].ToString());
                                    FPBCheckingDetail.Images = sqlDr["Images"].ToString();
                                    //FPBCheckingDetail.FPBCheckingItemID = int.Parse(sqlDr["FPBCheckingItemID"].ToString());
                                    FPBCheckingDetail.Result = sqlDr["Result"].ToString();
                                    FPBCheckingDetail.ResultContent = sqlDr["ResultContent"].ToString();
                                    //FPBCheckingDetail.CheckingItemName = sqlDr["CheckingItemName"].ToString();
                                    //FPBCheckingDetail.FrequencyID = int.Parse(sqlDr["FrequencyID"].ToString());
                                    lstFPBCheckingDetail.Add(FPBCheckingDetail);
                                }
                                returnFPBCheckingDetail.Code = "00";
                                returnFPBCheckingDetail.Message = "Lấy dữ liệu thành công.";
                                returnFPBCheckingDetail.LstFPBCheckingDetail = lstFPBCheckingDetail;
                            }
                            else
                            {
                                returnFPBCheckingDetail.Code = "01";
                                returnFPBCheckingDetail.Message = "Không tồn tại bản ghi nào.";
                                returnFPBCheckingDetail.Total = 0;
                                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingDetail.Code = "99";
                returnFPBCheckingDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingDetail.Total = 0;
                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                mylog4net.Error("LoadFPBCheckingDetail_ByFPBCheckingID", ex);

            }
            return returnFPBCheckingDetail;
        }

        public ReturnFPBCheckingDetail DeleteByID(int ID)
        {
            ReturnFPBCheckingDetail returnFPBCheckingDetail = new ReturnFPBCheckingDetail();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBCheckingDetail_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
                        cmd.ExecuteNonQuery();
                        returnFPBCheckingDetail.Code = "00";
                        returnFPBCheckingDetail.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBCheckingDetail.Code = "99";
                returnFPBCheckingDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBCheckingDetail.Total = 0;
                returnFPBCheckingDetail.LstFPBCheckingDetail = null;
                mylog4net.Error("DeleteByID", ex);
            }
            return returnFPBCheckingDetail;
        }
    }
}