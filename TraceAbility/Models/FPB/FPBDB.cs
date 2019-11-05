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
    public class FPBDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(FPBDB));
        public ReturnFPB ListAll()
        {
            List<FPB> lstFPB = null;
            FPB FPB = null;
            ReturnFPB returnFPB = new ReturnFPB();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPB_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstFPB = new List<FPB>();
                                while (sqlDr.Read())
                                {
                                    FPB = new FPB();
                                    FPB.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPB.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPB.FPBDate = DateTime.Parse(sqlDr["FPBDate"].ToString());
                                    FPB.IndicationNo = sqlDr["IndicationNo"].ToString();
                                    FPB.UserID = sqlDr["UserID"].ToString();
                                    FPB.SeqNo = sqlDr["SeqNo"].ToString();
                                    FPB.BlockID = sqlDr["BlockID"].ToString();
                                    FPB.Result = sqlDr["Result"].ToString();
                                    FPB.ResultContent = sqlDr["ResultContent"].ToString();

                                    lstFPB.Add(FPB);
                                }
                                returnFPB.Code = "00";
                                returnFPB.Message = "Lấy dữ liệu thành công.";
                                returnFPB.LstFPB = lstFPB;
                                returnFPB.UserID = MyShareInfo.ID;
                                returnFPB.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnFPB.Code = "01";
                                returnFPB.Message = "Không tồn tại bản ghi nào.";
                                returnFPB.Total = 0;
                                returnFPB.LstFPB = null;
                                returnFPB.UserID = MyShareInfo.ID;
                                returnFPB.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPB.Code = "99";
                returnFPB.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPB.Total = 0;
                returnFPB.LstFPB = null;
                mylog4net.Error("", ex);
            }
            return returnFPB;
        }

        public ReturnFPB GetbyID(string ID)
        {
            List<FPB> lstFPB= null;
            FPB FPB= null;
            ReturnFPB returnFPB = new ReturnFPB();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_FPB_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = int.Parse(ID);

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstFPB = new List<FPB>();
                                while (sqlDr.Read())
                                {
                                    FPB = new FPB();
                                    FPB.ID = int.Parse(sqlDr["ID"].ToString());
                                    FPB.IDFPBCheckingItem = int.Parse(sqlDr["IDFPBCheckingItem"].ToString());
                                    FPB.FPBDate = DateTime.Parse(sqlDr["FPBDate"].ToString());
                                    FPB.IndicationNo = sqlDr["IndicationNo"].ToString();
                                    FPB.UserID = sqlDr["UserID"].ToString();
                                    FPB.SeqNo = sqlDr["SeqNo"].ToString();
                                    FPB.BlockID = sqlDr["BlockID"].ToString();
                                    FPB.Result = sqlDr["Result"].ToString();
                                    FPB.ResultContent = sqlDr["ResultContent"].ToString();
                                    lstFPB.Add(FPB);
                                }
                                returnFPB.Code = "00";
                                returnFPB.Message = "Lấy dữ liệu thành công.";
                                returnFPB.LstFPB = lstFPB;
                            }
                            else
                            {
                                returnFPB.Code = "01";
                                returnFPB.Message = "Không tồn tại bản ghi nào.";
                                returnFPB.Total = 0;
                                returnFPB.LstFPB = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPB.Code = "99";
                returnFPB.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPB.Total = 0;
                returnFPB.LstFPB = null;
                mylog4net.Error("", ex);
            }
            return returnFPB;
        }

        public ReturnFPB Insert(FPB FPB)
        {
            ReturnFPB returnFPB = new ReturnFPB();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPB_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = FPB.ID;
                        cmd.Parameters.Add(new SqlParameter("@IDFPBCheckingItem", SqlDbType.Int)).Value = FPB.IDFPBCheckingItem;
                        cmd.Parameters.Add(new SqlParameter("@FPBDate", SqlDbType.DateTime)).Value = FPB.FPBDate;
                        cmd.Parameters.Add(new SqlParameter("@IndicationNo", SqlDbType.VarChar)).Value = FPB.IndicationNo;
                        cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.VarChar)).Value = FPB.UserID;
                        cmd.Parameters.Add(new SqlParameter("@SeqNo", SqlDbType.VarChar)).Value = FPB.SeqNo;
                        cmd.Parameters.Add(new SqlParameter("@BlockID", SqlDbType.VarChar)).Value = FPB.BlockID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = FPB.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContent", SqlDbType.NVarChar)).Value = FPB.ResultContent;

                        cmd.ExecuteNonQuery();

                        returnFPB.Code = "00";
                        returnFPB.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPB.Code = "99";
                returnFPB.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPB.Total = 0;
                returnFPB.LstFPB = null;
                mylog4net.Error("", ex);
            }
            return returnFPB;
        }


        public ReturnFPB DeleteByID(int _ID)
        {
            ReturnFPB returnFPB = new ReturnFPB();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_FPB_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnFPB.Code = "00";
                        returnFPB.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnFPB.Code = "99";
                returnFPB.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnFPB.Total = 0;
                returnFPB.LstFPB = null;
                mylog4net.Error("", ex);
            }
            return returnFPB;
        }
    }
}