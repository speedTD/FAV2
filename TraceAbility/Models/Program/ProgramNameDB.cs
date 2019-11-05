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
    public class ProgramNameDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProgramNameDB));
        protected string s = "ProgramName";

        public ReturnProgramName ListAll()
        {
            List<ProgramName> lstProgramName = null;
            ProgramName programname = null;
            ReturnProgramName returnProgramName = new ReturnProgramName();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramName_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramName = new List<ProgramName>();
                                while (sqlDr.Read())
                                {
                                    programname = new ProgramName();
                                    programname.programName = sqlDr["ProgramName"].ToString();
                                    programname.ProgramType = sqlDr["ProgramType"].ToString();
                                    programname.FactoryID = sqlDr["FactoryID"].ToString();
                                    programname.OperatorID = Int32.Parse(sqlDr["OperatorID"].ToString());
                                    programname.OperatorName = sqlDr["UserName"].ToString();
                                    programname.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstProgramName.Add(programname);
                                }
                                returnProgramName.Code = "00";
                                returnProgramName.Message = "Lấy dữ liệu thành công.";
                                returnProgramName.lstProgramName = lstProgramName;
                                returnProgramName.userID = MyShareInfo.ID;
                                returnProgramName.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProgramName.Code = "01";
                                returnProgramName.Message = "Không tồn tại bản ghi nào.";
                                returnProgramName.Total = 0;
                                returnProgramName.userID = MyShareInfo.ID;
                                returnProgramName.UserName = MyShareInfo.UserName;
                                returnProgramName.lstProgramName = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramName.Code = "99";
                returnProgramName.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramName.Total = 0;
                returnProgramName.lstProgramName = null;
                mylog4net.Error("", ex);
            }
            return returnProgramName;
        }

        public ReturnProgramName GetbyID(string nameProgram)
        {
            List<ProgramName> lstProgramName = null;
            ProgramName programname = null;
            ReturnProgramName returnProgramName = new ReturnProgramName();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProgramName_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = nameProgram;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramName = new List<ProgramName>();
                                while (sqlDr.Read())
                                {
                                    programname = new ProgramName();
                                    programname.programName = sqlDr["ProgramName"].ToString();
                                    programname.ProgramType = sqlDr["ProgramType"].ToString();
                                    programname.FactoryID = sqlDr["FactoryID"].ToString();
                                    programname.OperatorID = Int32.Parse(sqlDr["OperatorID"].ToString());
                                    programname.OperatorName = sqlDr["UserName"].ToString();
                                    programname.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstProgramName.Add(programname);
                                }
                                returnProgramName.Code = "00";
                                returnProgramName.Message = "Lấy dữ liệu thành công.";
                                returnProgramName.Total = lstProgramName.Count;
                                returnProgramName.lstProgramName = lstProgramName;
                            }
                            else
                            {
                                returnProgramName.Code = "01";
                                returnProgramName.Message = "Không tồn tại bản ghi nào.";
                                returnProgramName.Total = 0;
                                returnProgramName.lstProgramName = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                returnProgramName.Code = "99";
                returnProgramName.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramName.Total = 0;
                returnProgramName.lstProgramName = null;
                mylog4net.Error("", ex);
            }
            return returnProgramName;
        }

        public ReturnProgramName Insert(ProgramName p)
        {
            ReturnProgramName returnProgramName = new ReturnProgramName();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramName_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.programName;
                        cmd.Parameters.Add(new SqlParameter("@ProgramType", SqlDbType.VarChar)).Value = p.ProgramType;
                        cmd.Parameters.Add(new SqlParameter("@FactoryID", SqlDbType.VarChar)).Value = p.FactoryID;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.Int)).Value = p.OperatorID;
                        cmd.ExecuteNonQuery();

                        returnProgramName.Code = "00";
                        returnProgramName.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramName.Code = "99";
                returnProgramName.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramName.Total = 0;
                returnProgramName.lstProgramName = null;
                mylog4net.Error("", ex);
            }
            return returnProgramName;
        }


        public ReturnProgramName DeleteByID(string id)
        {
            ReturnProgramName returnProgramName = new ReturnProgramName();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramName_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = id;
                        cmd.ExecuteNonQuery();
                        returnProgramName.Code = "00";
                        returnProgramName.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramName.Code = "99";
                returnProgramName.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramName.Total = 0;
                returnProgramName.lstProgramName = null;
                mylog4net.Error("public ReturnProgramName DeleteByID(string id)", ex);
            }
            return returnProgramName;
        }
    }
}