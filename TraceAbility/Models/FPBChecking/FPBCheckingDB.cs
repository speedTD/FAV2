using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestABC.Common;
using TestABC.Models.Data;

using log4net;
namespace TestABC.Models.FPBChecking
{
    public class FPBCheckingDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(FPBCheckingDB));
        public ReturnFPBChecking Insert(FPBChecking FPBChecking)
        {
            ReturnFPBChecking returnFPBChecking = new ReturnFPBChecking();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBChecking_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = FPBChecking.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = FPBChecking.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@FPBCheckingDate", SqlDbType.DateTime)).Value = FPBChecking.FPBCheckingDate;
                        cmd.Parameters.Add(new SqlParameter("@BatchNo", SqlDbType.VarChar)).Value = FPBChecking.BatchNo;
                        cmd.Parameters.Add(new SqlParameter("@IndicationNo", SqlDbType.VarChar)).Value = FPBChecking.IndicationNo;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value = FPBChecking.ItemCode;
                        cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar)).Value = FPBChecking.ItemName;
                        cmd.Parameters.Add(new SqlParameter("@MachineName", SqlDbType.VarChar)).Value = FPBChecking.MachineName;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.VarChar)).Value = FPBChecking.UserID;
                        cmd.Parameters.Add(new SqlParameter("@SeqNo", SqlDbType.VarChar)).Value = FPBChecking.SeqNo == null ? "" : FPBChecking.SeqNo;
                        cmd.Parameters.Add(new SqlParameter("@BlockID", SqlDbType.VarChar)).Value = FPBChecking.BlockID == null ? "" : FPBChecking.BlockID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = FPBChecking.Result == null ? "" : FPBChecking.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContent", SqlDbType.NVarChar)).Value = "OK";
                        cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        returnFPBChecking.Code = "00";
                        returnFPBChecking.Message = "Cập nhật dữ liệu thành công.";
                        if (returnFPBChecking.Code == "00")
                            returnFPBChecking.LastID = Convert.ToInt32(cmd.Parameters["@LastID"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBChecking.Code = "99";
                returnFPBChecking.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBChecking.Total = 0;
                returnFPBChecking.LstFPBChecking = null;
                mylog4net.Error("", ex);
            }
            return returnFPBChecking;
        }
        public ReturnFPBChecking ListAll()
        {
            List<FPBChecking> lstFPBChecking = null;
            FPBChecking FPBChecking = null;
            ReturnFPBChecking returnFPBChecking = new ReturnFPBChecking();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBChecking_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstFPBChecking = new List<FPBChecking>();
                                while (sqlDr.Read())
                                {
                                    FPBChecking = new FPBChecking();
                                    FPBChecking.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPBChecking.IndicationNo = sqlDr["IndicationNo"].ToString();
                                    FPBChecking.FPBCheckingDate = DateTime.Parse(sqlDr["FPBCheckingDate"].ToString());
                                    FPBChecking.ItemName = sqlDr["ItemName"].ToString();
                                    FPBChecking.ItemCode = sqlDr["ItemCode"].ToString();
                                    FPBChecking.BatchNo = sqlDr["BatchNo"].ToString();
                                    FPBChecking.MachineID = sqlDr["MachineID"].ToString();
                                    FPBChecking.MachineName = sqlDr["MachineName"].ToString();
                                    FPBChecking.UserID = sqlDr["OperatorID"].ToString();
                                    FPBChecking.SeqNo = sqlDr["SeqNo"].ToString();
                                    FPBChecking.BlockID = sqlDr["BlockID"].ToString();
                                    FPBChecking.Result = sqlDr["Result"].ToString();
                                    FPBChecking.ResultContent = sqlDr["ResultContent"].ToString();
                                    FPBChecking.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());

                                    lstFPBChecking.Add(FPBChecking);
                                }
                                returnFPBChecking.Code = "00";
                                returnFPBChecking.Message = "Lấy dữ liệu thành công.";
                                returnFPBChecking.LstFPBChecking = lstFPBChecking;
                                returnFPBChecking.UserID = MyShareInfo.ID;
                                returnFPBChecking.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnFPBChecking.Code = "01";
                                returnFPBChecking.Message = "Không tồn tại bản ghi nào.";
                                returnFPBChecking.Total = 0;
                                returnFPBChecking.LstFPBChecking = null;
                                returnFPBChecking.UserID = MyShareInfo.ID;
                                returnFPBChecking.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBChecking.Code = "99";
                returnFPBChecking.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBChecking.Total = 0;
                returnFPBChecking.LstFPBChecking = null;
                mylog4net.Error("", ex);
            }
            return returnFPBChecking;
        }
        public ReturnFPBChecking DeleteByID(int _ID)
        {
            ReturnFPBChecking returnFPBChecking = new ReturnFPBChecking();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPBChecking_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnFPBChecking.Code = "00";
                        returnFPBChecking.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBChecking.Code = "99";
                returnFPBChecking.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBChecking.Total = 0;
                returnFPBChecking.LstFPBChecking = null;
                mylog4net.Error("", ex);
            }
            return returnFPBChecking;
        }
        public ReturnFPBChecking GetbyID(string ID)
        {
            List<FPBChecking> lstFPBChecking = null;
            FPBChecking FPBChecking = null;
            ReturnFPBChecking returnFPBChecking = new ReturnFPBChecking();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPBChecking_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = int.Parse(ID);

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPBChecking = new List<FPBChecking>();
                                while (sqlDr.Read())
                                {
                                    FPBChecking = new FPBChecking();
                                    FPBChecking.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPBChecking.IndicationNo = sqlDr["IndicationNo"].ToString();
                                    FPBChecking.FPBCheckingDate = DateTime.Parse(sqlDr["FPBCheckingDate"].ToString());
                                    FPBChecking.ItemName = sqlDr["ItemName"].ToString();
                                    FPBChecking.ItemCode = sqlDr["ItemCode"].ToString();
                                    FPBChecking.BatchNo = sqlDr["BatchNo"].ToString();
                                    FPBChecking.MachineID = sqlDr["MachineID"].ToString();
                                    FPBChecking.MachineName = sqlDr["MachineName"].ToString();
                                    FPBChecking.UserID = sqlDr["OperatorID"].ToString();
                                    FPBChecking.SeqNo = sqlDr["SeqNo"].ToString();
                                    FPBChecking.BlockID = sqlDr["BlockID"].ToString();
                                    FPBChecking.Result = sqlDr["Result"].ToString();
                                    FPBChecking.ResultContent = sqlDr["ResultContent"].ToString();
                                    FPBChecking.Images = sqlDr["Images"].ToString();
                                    FPBChecking.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstFPBChecking.Add(FPBChecking);
                                }
                                returnFPBChecking.Code = "00";
                                returnFPBChecking.Message = "Lấy dữ liệu thành công.";
                                returnFPBChecking.LstFPBChecking = lstFPBChecking;
                                returnFPBChecking.UserID = MyShareInfo.ID;
                                returnFPBChecking.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnFPBChecking.Code = "01";
                                returnFPBChecking.Message = "Không tồn tại bản ghi nào.";
                                returnFPBChecking.Total = 0;
                                returnFPBChecking.LstFPBChecking = null;
                                returnFPBChecking.UserID = MyShareInfo.ID;
                                returnFPBChecking.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPBChecking.Code = "99";
                returnFPBChecking.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPBChecking.Total = 0;
                returnFPBChecking.LstFPBChecking = null;
                mylog4net.Error("", ex);
            }
            return returnFPBChecking;
        }
        public ReturnFPBChecking SearchFPBChecking(ReturnFPBChecking searchFPBChecking, int pageSize)
        {
            List<FPBChecking> lstFPBChecking = null;
            FPBChecking FPBChecking = null;
            ReturnFPBChecking returnProgramPdtCtrl = new ReturnFPBChecking();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tFPBChecking_Search";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = searchFPBChecking.aFPBChecking.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.NVarChar)).Value = searchFPBChecking.aFPBChecking.Result;
                        if (searchFPBChecking.aFPBChecking.FPBCheckingDate == DateTime.MinValue)
                            cmd.Parameters.Add(new SqlParameter("@FPBCheckingDate", SqlDbType.NVarChar)).Value = DBNull.Value;
                        else cmd.Parameters.Add(new SqlParameter("@FPBCheckingDate", SqlDbType.NVarChar)).Value = searchFPBChecking.aFPBChecking.FPBCheckingDate.ToString("G");
                        //cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = searchFPBChecking.PageNumber;
                        //cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        //cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstFPBChecking = new List<FPBChecking>();
                                while (sqlDr.Read())
                                {
                                    FPBChecking = new FPBChecking();
                                    FPBChecking.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPBChecking.IndicationNo = sqlDr["IndicationNo"].ToString();
                                    FPBChecking.FPBCheckingDate = DateTime.Parse(sqlDr["FPBCheckingDate"].ToString());
                                    FPBChecking.ItemName = sqlDr["ItemName"].ToString();
                                    FPBChecking.ItemCode = sqlDr["ItemCode"].ToString();
                                    FPBChecking.BatchNo = sqlDr["BatchNo"].ToString();
                                    FPBChecking.MachineID = sqlDr["MachineID"].ToString();
                                    FPBChecking.MachineName = sqlDr["MachineName"].ToString();
                                    FPBChecking.UserID = sqlDr["OperatorID"].ToString();
                                    FPBChecking.SeqNo = sqlDr["SeqNo"].ToString();
                                    FPBChecking.BlockID = sqlDr["BlockID"].ToString();
                                    FPBChecking.Result = sqlDr["Result"].ToString();
                                    FPBChecking.ResultContent = sqlDr["ResultContent"].ToString();
                                    FPBChecking.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());


                                    lstFPBChecking.Add(FPBChecking);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.LstFPBChecking = lstFPBChecking;
                                returnProgramPdtCtrl.UserID = MyShareInfo.ID;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.LstFPBChecking = null;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                            }
                        }
                        //get return Totalpage value.
                        //if (returnProgramPdtCtrl.Code == "00")
                        //    returnProgramPdtCtrl.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.LstFPBChecking = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }
    }
}