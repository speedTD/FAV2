using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using TestABC.Common;
using TestABC.Models.Data;

using log4net;
namespace TestABC.Models.Data
{
    public class LineDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(LineDB));
        public ReturnLine ListAll()
        {
            List<Line> lstLine = null;
            Line line = null;
            ReturnLine returnLine = new ReturnLine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tLine_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                       
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstLine = new List<Line>();
                                while (sqlDr.Read())
                                {
                                    line = new Line();
                                    line.LineID = sqlDr["LineID"].ToString();
                                    line.LineName = sqlDr["LineName"].ToString();
                                    line.FactoryID = sqlDr["FactoryID"].ToString();
                                    line.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstLine.Add(line);
                                }
                                returnLine.Code = "00";
                                returnLine.Message = "Lấy dữ liệu thành công.";
                                returnLine.LstLine = lstLine;
                                returnLine.UserID = MyShareInfo.ID;
                                returnLine.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnLine.Code = "01";
                                returnLine.Message = "Không tồn tại bản ghi nào.";
                                returnLine.Total = 0;
                                returnLine.LstLine = null; 
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                returnLine.Code = "99";
                returnLine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnLine.Total = 0;
                returnLine.LstLine = null;
                mylog4net.Error("", ex);
            }
            return returnLine;
        }

        public ReturnLine GetbyID(string LineID)
        {
            List<Line> lstLine = null;
            Line line = null;
            ReturnLine ReturnLine = new ReturnLine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tLine_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = LineID.Trim();

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstLine = new List<Line>();
                                while (sqlDr.Read())
                                {
                                    line = new Line();
                                    line.LineID = sqlDr["LineID"].ToString();
                                    line.LineName = sqlDr["LineName"].ToString();
                                    line.FactoryID = sqlDr["FactoryID"].ToString();
                                    line.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());

                                    lstLine.Add(line);
                                }
                                ReturnLine.Code = "00";
                                ReturnLine.Message = "Lấy dữ liệu thành công.";
                                ReturnLine.LstLine = lstLine;
                                ReturnLine.Total = lstLine.Count;
                                //}
                            }
                            else
                            {
                                ReturnLine.Code = "01";
                                ReturnLine.Message = "Không tồn tại bản ghi nào.";
                                ReturnLine.Total = 0;
                                ReturnLine.LstLine = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnLine.Code = "99";
                ReturnLine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                ReturnLine.Total = 0;
                ReturnLine.LstLine = null;
                mylog4net.Error("", ex);
            }
            return ReturnLine;
        }

        public ReturnLine Insert(Line line)
        {
            ReturnLine ReturnLine = new ReturnLine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tLine_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = line.LineID;
                        cmd.Parameters.Add(new SqlParameter("@LineName", SqlDbType.VarChar)).Value = line.LineName;
                        cmd.Parameters.Add(new SqlParameter("@FactoryID", SqlDbType.VarChar)).Value = line.FactoryID;                       
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        ReturnLine.Code = "00";
                        ReturnLine.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnLine.Code = "99";
                ReturnLine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                ReturnLine.Total = 0;
                ReturnLine.LstLine = null;
                mylog4net.Error("", ex);
            }
            return ReturnLine;
        }

        public ReturnLine DeleteByID(string _ID)
        {
            ReturnLine ReturnLine = new ReturnLine();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tLine_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = _ID.Trim();
                        cmd.ExecuteNonQuery();
                        ReturnLine.Code = "00";
                        ReturnLine.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnLine.Code = "99";
                ReturnLine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                ReturnLine.Total = 0;
                ReturnLine.LstLine = null;
                mylog4net.Error("", ex);
            }
            return ReturnLine;
        }
    }
}