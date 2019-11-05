using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestABC.Common;

using log4net;
using log4net;

namespace TestABC.Models.ProductionControl
{
    
    public class ProductionControlDetailDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProductionControlDetailDB));
        public ReturnProductionControlDetail InsertProductionControlDetail(ProductionControlDetail productionControlDetail)
        {
            ReturnProductionControlDetail returnProductionControlDetail = new ReturnProductionControlDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControlDetail_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = productionControlDetail.ID;
                        cmd.Parameters.Add(new SqlParameter("@ProductionControlID", SqlDbType.Int)).Value = productionControlDetail.ProductionControlID;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = productionControlDetail.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@ProgramPdtCtrlID", SqlDbType.Int)).Value = productionControlDetail.ProgramPdtCtrlID;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = productionControlDetail.Part;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = productionControlDetail.ControlItem;
                        cmd.Parameters.Add(new SqlParameter("@ColumnName", SqlDbType.NVarChar)).Value = productionControlDetail.ColumnName;
                            cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar)).Value = productionControlDetail.Unit;
                        cmd.Parameters.Add(new SqlParameter("@SpecDisplay", SqlDbType.NVarChar)).Value = productionControlDetail.SpecDisplay;
                        cmd.Parameters.Add(new SqlParameter("@LowerLimit", SqlDbType.NVarChar)).Value = productionControlDetail.LowerLimit;
                        cmd.Parameters.Add(new SqlParameter("@UpperLimit", SqlDbType.NVarChar)).Value = productionControlDetail.UpperLimit;
                        cmd.Parameters.Add(new SqlParameter("@ActualValue", SqlDbType.NVarChar)).Value = productionControlDetail.ActualValue;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = productionControlDetail.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContent", SqlDbType.NVarChar)).Value = productionControlDetail.ResultContent;
                        cmd.ExecuteNonQuery();

                        returnProductionControlDetail.Code = "00";
                        returnProductionControlDetail.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControlDetail.Code = "99";
                returnProductionControlDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControlDetail.Total = 0;
                returnProductionControlDetail.lstProductionControlDetail = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControlDetail;
        }
        public ReturnProductionControlDetail GetbyProductionControlID(int ProductionControlID)
        {
            List<ProductionControlDetail> lstProductionControlDetail = null;
            ProductionControlDetail productionControlDetail = null;
            ReturnProductionControlDetail returnProductionControlDetail = new ReturnProductionControlDetail();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionControlDetail_SelectByProductionControlID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProductionControlID", SqlDbType.Int)).Value = ProductionControlID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProductionControlDetail = new List<ProductionControlDetail>();
                                while (sqlDr.Read())
                                {
                                    productionControlDetail = new ProductionControlDetail();
                                    productionControlDetail.ID = int.Parse(sqlDr["ID"].ToString());
                                    productionControlDetail.ProductionControlID = int.Parse(sqlDr["ProductionControlID"].ToString());
                                    productionControlDetail.ProgramName = sqlDr["ProgramName"].ToString();
                                    productionControlDetail.ProgramPdtCtrlID = Convert.ToInt32(sqlDr["ProgramPdtCtrlID"].ToString());
                                    productionControlDetail.ControlItem = sqlDr["ControlItem"].ToString();
                                    productionControlDetail.ColumnName = sqlDr["ColumnName"].ToString();
                                    productionControlDetail.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    productionControlDetail.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    productionControlDetail.ActualValue = sqlDr["ActualValue"].ToString();
                                    productionControlDetail.Result = sqlDr["Result"].ToString();
                                    productionControlDetail.ResultContent = sqlDr["ResultContent"].ToString();
                                    productionControlDetail.Unit = sqlDr["Unit"].ToString();
                                    productionControlDetail.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    lstProductionControlDetail.Add(productionControlDetail);
                                }
                                returnProductionControlDetail.Code = "00";
                                returnProductionControlDetail.Message = "Lấy dữ liệu thành công.";
                                returnProductionControlDetail.lstProductionControlDetail = lstProductionControlDetail;
                                //}
                            }
                            else
                            {
                                returnProductionControlDetail.Code = "01";
                                returnProductionControlDetail.Message = "Không tồn tại bản ghi nào.";
                                returnProductionControlDetail.Total = 0;
                                returnProductionControlDetail.lstProductionControlDetail = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProductionControlDetail.Code = "99";
                returnProductionControlDetail.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProductionControlDetail.Total = 0;
                returnProductionControlDetail.lstProductionControlDetail = null;
                mylog4net.Error("", ex);
            }
            return returnProductionControlDetail;
        }
    }
}