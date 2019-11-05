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
    public class ProductionListDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProductionListDB));
        public ReturnProductionList ProductionListAll()
        {
            List<ProductionList> lstProductionList = null;
            ProductionList productionList = null;
            ReturnProductionList returnProductionList = new ReturnProductionList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstProductionList = new List<ProductionList>();
                                while (sqlDr.Read())
                                {
                                    productionList = new ProductionList();
                                    productionList.IndicatorID = sqlDr["IndicatorID"].ToString();
                                    productionList.ItemName = sqlDr["ItemName"].ToString();
                                    productionList.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionList.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionList.ProgramName = sqlDr["ProgramName"].ToString();
                                    productionList.LineID = sqlDr["LineID"].ToString();
                                    productionList.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    productionList.PatternCode = sqlDr["PatternCode"].ToString();
                                    productionList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    lstProductionList.Add(productionList);
                                }
                                returnProductionList.Code = "00";
                                returnProductionList.Message = "Lấy dữ liệu thành công.";
                                returnProductionList.lstProductionList = lstProductionList;
                            }
                            else
                            {
                                returnProductionList.Code = "01";
                                returnProductionList.Message = "Không tồn tại bản ghi nào.";
                                returnProductionList.Total = 0;
                                returnProductionList.lstProductionList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionList.Code = "99";
                returnProductionList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionList.Total = 0;
                returnProductionList.lstProductionList = null;
                mylog4net.Error("", ex);
            }
            return returnProductionList;
        }

        public ReturnProductionList GetbyID(string ID)
        {
            List<ProductionList> lstProductionList = null;
            ProductionList productionList = null;
            ReturnProductionList returnProductionList = new ReturnProductionList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IndicatorID", SqlDbType.VarChar)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionList = new List<ProductionList>();
                                while (sqlDr.Read())
                                {
                                    productionList = new ProductionList();
                                    productionList.IndicatorID = sqlDr["IndicatorID"].ToString();
                                    productionList.ItemName = sqlDr["ItemName"].ToString();
                                    productionList.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionList.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionList.ProgramName = sqlDr["ProgramName"].ToString();
                                    productionList.LineID = sqlDr["LineID"].ToString();
                                    productionList.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    productionList.PatternCode = sqlDr["PatternCode"].ToString();
                                    productionList.MachineTypeName = sqlDr["TypeName"].ToString();
                                    lstProductionList.Add(productionList);
                                }
                                returnProductionList.Code = "00";
                                returnProductionList.Message = "Lấy dữ liệu thành công.";
                                returnProductionList.lstProductionList = lstProductionList;
                                returnProductionList.Total = lstProductionList.Count;
                                //}
                            }
                            else
                            {
                                returnProductionList.Code = "01";
                                returnProductionList.Message = "Không tồn tại bản ghi nào.";
                                returnProductionList.Total = 0;
                                returnProductionList.lstProductionList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionList.Code = "99";
                returnProductionList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionList.Total = 0;
                returnProductionList.lstProductionList = null;
                mylog4net.Error("", ex);
            }
            return returnProductionList;
        }
        public ReturnProductionList GetbyItemCode(string ItemCode)
        {
            List<ProductionList> lstProductionList = null;
            ProductionList productionList = null;
            ReturnProductionList returnProductionList = new ReturnProductionList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionList_SelectByItemCode";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value = ItemCode;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionList = new List<ProductionList>();
                                while (sqlDr.Read())
                                {
                                    productionList = new ProductionList();
                                    productionList.IndicatorID = sqlDr["IndicatorID"].ToString();
                                    productionList.ItemName = sqlDr["ItemName"].ToString();
                                    productionList.ItemCode = sqlDr["ItemCode"].ToString();
                                    productionList.BatchNo = sqlDr["BatchNo"].ToString();
                                    productionList.ProgramName = sqlDr["ProgramName"].ToString();
                                    productionList.LineID = sqlDr["LineID"].ToString();
                                    productionList.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    productionList.PatternCode = sqlDr["PatternCode"].ToString();
                                    lstProductionList.Add(productionList);
                                }
                                returnProductionList.Code = "00";
                                returnProductionList.Message = "Lấy dữ liệu thành công.";
                                returnProductionList.lstProductionList = lstProductionList;
                                returnProductionList.Total = lstProductionList.Count;
                                //}
                            }
                            else
                            {
                                returnProductionList.Code = "01";
                                returnProductionList.Message = "Không tồn tại bản ghi nào.";
                                returnProductionList.Total = 0;
                                returnProductionList.lstProductionList = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionList.Code = "99";
                returnProductionList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionList.Total = 0;
                returnProductionList.lstProductionList = null;
                mylog4net.Error("", ex);
            }
            return returnProductionList;
        }
        public ReturnProductionList Insert(ProductionList productionList)
        {
            ReturnProductionList returnProductionList = new ReturnProductionList();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IndicatorID", SqlDbType.VarChar)).Value = productionList.IndicatorID;
                        cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar)).Value = productionList.ItemName;
                        cmd.Parameters.Add(new SqlParameter("@ItemCode", SqlDbType.VarChar)).Value = productionList.ItemCode;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = productionList.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@BatchNo", SqlDbType.VarChar)).Value = productionList.BatchNo;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = productionList.LineID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = productionList.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@PatternCode", SqlDbType.VarChar)).Value = productionList.PatternCode;
                        cmd.ExecuteNonQuery();

                        returnProductionList.Code = "00";
                        returnProductionList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionList.Code = "99";
                returnProductionList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionList.Total = 0;
                returnProductionList.lstProductionList = null;
                mylog4net.Error("", ex);
            }
            return returnProductionList;
        }


        public ReturnProductionList DeleteByID(string  _ID)
        {
            ReturnProductionList returnProductionList = new ReturnProductionList();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IndicatorID", SqlDbType.VarChar)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProductionList.Code = "00";
                        returnProductionList.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionList.Code = "99";
                returnProductionList.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionList.Total = 0;
                returnProductionList.lstProductionList = null;
                mylog4net.Error("", ex);
            }
            return returnProductionList;
        }
        
    }
}