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
    public class ProgramPdtCtrlDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProgramPdtCtrlDB));
        public ReturnProgramPdtCtrl ListbyPage(int pageNumber, int pageSize)
        {
            List<ProgramPdtCtrl> lstProgramPdtCtrl = null;
            ProgramPdtCtrl ProgramPdtCtrl = null;
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_SelectByPage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
                                while (sqlDr.Read())
                                {
                                    ProgramPdtCtrl = new ProgramPdtCtrl();
                                    ProgramPdtCtrl.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    ProgramPdtCtrl.ProgramName = sqlDr["ProgramName"].ToString();
                                    ProgramPdtCtrl.Part = sqlDr["Part"].ToString();
                                    ProgramPdtCtrl.ControlItem = sqlDr["ControlItem"].ToString();
                                    ProgramPdtCtrl.ColumnName = sqlDr["ColumnName"].ToString();
                                    ProgramPdtCtrl.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    ProgramPdtCtrl.Unit = sqlDr["Unit"].ToString();
                                    ProgramPdtCtrl.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    ProgramPdtCtrl.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["OperatorID"].ToString()))
                                    {
                                        ProgramPdtCtrl.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                        ProgramPdtCtrl.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(ProgramPdtCtrl.OperatorID));
                                    }
                                    ProgramPdtCtrl.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());

                                    lstProgramPdtCtrl.Add(ProgramPdtCtrl);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.lstProgramPdtCtrl = lstProgramPdtCtrl;
                                returnProgramPdtCtrl.userID = MyShareInfo.ID;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                                returnProgramPdtCtrl.userID = MyShareInfo.ID;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                            }
                        }
                        //get return Totalpage value.
                        if (returnProgramPdtCtrl.Code == "00")
                            returnProgramPdtCtrl.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl DeleteByKey(ProgramPdtCtrl p)
        {
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_DeleteByKey";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.VarChar)).Value = p.Part;

                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.VarChar)).Value = p.ControlItem;

                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProgramPdtCtrl.Code = "00";
                        returnProgramPdtCtrl.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl DeleteByID(int ID, int OperatorID)
        {
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.Int)).Value = OperatorID;
                        cmd.ExecuteNonQuery();

                        returnProgramPdtCtrl.Code = "00";
                        returnProgramPdtCtrl.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl GetbyKey(ProgramPdtCtrl p)
        {
            List<ProgramPdtCtrl> lstProgramPdtCtrl = null;
            ProgramPdtCtrl programPdtCtrl = null;
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProgramPdtCtrl_SelectByKey";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = p.Part;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = p.ControlItem;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrl = new ProgramPdtCtrl();
                                    programPdtCtrl.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    programPdtCtrl.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrl.Part = sqlDr["Part"].ToString();
                                    programPdtCtrl.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrl.ColumnName = sqlDr["ColumnName"].ToString();
                                    programPdtCtrl.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrl.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrl.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrl.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    programPdtCtrl.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                    programPdtCtrl.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrl.OperatorID));

                                    programPdtCtrl.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstProgramPdtCtrl.Add(programPdtCtrl);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.lstProgramPdtCtrl = lstProgramPdtCtrl;
                                returnProgramPdtCtrl.Total = lstProgramPdtCtrl.Count;
                                //}
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl GetbyID(int ID)
        {
            List<ProgramPdtCtrl> lstProgramPdtCtrl = null;
            ProgramPdtCtrl programPdtCtrl = null;
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProgramPdtCtrl_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrl = new ProgramPdtCtrl();
                                    programPdtCtrl.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    programPdtCtrl.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrl.Part = sqlDr["Part"].ToString();
                                    programPdtCtrl.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrl.ColumnName = sqlDr["ColumnName"].ToString();
                                    programPdtCtrl.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrl.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrl.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrl.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    programPdtCtrl.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());

                                    programPdtCtrl.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrl.OperatorID));

                                    programPdtCtrl.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstProgramPdtCtrl.Add(programPdtCtrl);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.lstProgramPdtCtrl = lstProgramPdtCtrl;
                                returnProgramPdtCtrl.Total = lstProgramPdtCtrl.Count;
                                //}
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error(" public ReturnProgramPdtCtrl GetbyID(int ID) ", ex);
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl Insert(ProgramPdtCtrl p)
        {
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(p.Part) ? "" : p.Part ;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = p.ControlItem;
                        cmd.Parameters.Add(new SqlParameter("@ColumnName", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(p.ColumnName) ? "" : p.ColumnName; 
                        cmd.Parameters.Add(new SqlParameter("@LowerLimit", SqlDbType.NVarChar)).Value = p.LowerLimit;
                        cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(p.Unit) ? "" : p.Unit;
                        cmd.Parameters.Add(new SqlParameter("@UpperLimit", SqlDbType.NVarChar)).Value = p.UpperLimit;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.Int)).Value = p.OperatorID;
                        cmd.Parameters.Add(new SqlParameter("@SpecDisplay", SqlDbType.NVarChar)).Value = String.IsNullOrEmpty(p.SpecDisplay) ? "" : p.SpecDisplay;

                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProgramPdtCtrl.Code = "00";
                        returnProgramPdtCtrl.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl SearchProgramPdtCtrl(ProgramPdtCtrl programPdtCtrl, int pageNumber, int pageSize)
        {
            List<ProgramPdtCtrl> lstProgramPdtCtrl = null;
            //ProgramPdtCtrl programPdtCtrl = null;
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_Search";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = programPdtCtrl.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = programPdtCtrl.Part;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = programPdtCtrl.ControlItem;

                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrl = new ProgramPdtCtrl();
                                    programPdtCtrl.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    programPdtCtrl.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrl.Part = sqlDr["Part"].ToString();
                                    programPdtCtrl.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrl.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrl.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrl.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    programPdtCtrl.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrl.ColumnName = sqlDr["ColumnName"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["OperatorID"].ToString()))
                                    {
                                        programPdtCtrl.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                        programPdtCtrl.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrl.OperatorID));
                                    }
                                    programPdtCtrl.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString()); 

                                    lstProgramPdtCtrl.Add(programPdtCtrl);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.lstProgramPdtCtrl = lstProgramPdtCtrl;
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                            }
                        }
                        //get return Totalpage value.
                        if (returnProgramPdtCtrl.Code == "00")
                            returnProgramPdtCtrl.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }
        public ReturnProgramPdtCtrl GetProgramPdtCtrl_ByProgramName(ReturnProgramPdtCtrl searchProgramPdtCtrl)
        {
            List<ProgramPdtCtrl> lstProgramPdtCtrl = null;
            ProgramPdtCtrl programPdtCtrl = null;
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_GetProgramPdtCtrl_ByProgramName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = searchProgramPdtCtrl.aProgramPdtCtrl.ProgramName;
                        //cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = searchProgramPdtCtrl.aProgramPdtCtrl.Part;
                        //cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = searchProgramPdtCtrl.aProgramPdtCtrl.ControlItem;

                        //cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = searchProgramPdtCtrl.PageNumber;
                        //cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        //cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrl = new List<ProgramPdtCtrl>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrl = new ProgramPdtCtrl();
                                    programPdtCtrl.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    programPdtCtrl.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrl.Part = sqlDr["Part"].ToString();
                                    programPdtCtrl.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrl.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrl.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrl.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    programPdtCtrl.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrl.ColumnName = sqlDr["ColumnName"].ToString();
                                    programPdtCtrl.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                    programPdtCtrl.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrl.OperatorID));
                                    programPdtCtrl.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    lstProgramPdtCtrl.Add(programPdtCtrl);
                                }
                                returnProgramPdtCtrl.Code = "00";
                                returnProgramPdtCtrl.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrl.lstProgramPdtCtrl = lstProgramPdtCtrl;
                                returnProgramPdtCtrl.userID = MyShareInfo.ID;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                                returnProgramPdtCtrl.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProgramPdtCtrl.Code = "01";
                                returnProgramPdtCtrl.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrl.Total = 0;
                                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
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
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }

        public ReturnProgramPdtCtrl ImportExcel(Import_ProgramPdtCtrl importProgramPdtCtrl)
        {
            ReturnProgramPdtCtrl returnProgramPdtCtrl = new ReturnProgramPdtCtrl();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrl_ImportExcel";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", importProgramPdtCtrl.ID);
                        cmd.Parameters.AddWithValue("@FactoryID", importProgramPdtCtrl.FactoryID);
                        cmd.Parameters.AddWithValue("@ProgramType", importProgramPdtCtrl.ProgramType);
                        cmd.Parameters.AddWithValue("@ProgramName", importProgramPdtCtrl.ProgramName);
                        cmd.Parameters.AddWithValue("@Part", importProgramPdtCtrl.Part);
                        cmd.Parameters.AddWithValue("@ControlItem", importProgramPdtCtrl.ControlItem);
                        cmd.Parameters.AddWithValue("@ColumnName", importProgramPdtCtrl.ColumnName);
                        cmd.Parameters.AddWithValue("@SpecDisplay", importProgramPdtCtrl.SpecDisplay);
                        cmd.Parameters.AddWithValue("@Unit", importProgramPdtCtrl.Unit);
                        cmd.Parameters.AddWithValue("@LowerLimit", importProgramPdtCtrl.LowerLimit);
                        cmd.Parameters.AddWithValue("@UpperLimit", importProgramPdtCtrl.UpperLimit);
                        cmd.Parameters.AddWithValue("@OperatorID", importProgramPdtCtrl.OperatorID);
                        int x = cmd.ExecuteNonQuery();

                        returnProgramPdtCtrl.Code = "00";
                        returnProgramPdtCtrl.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrl.Code = "99";
                returnProgramPdtCtrl.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrl.Total = 0;
                returnProgramPdtCtrl.lstProgramPdtCtrl = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrl;
        }
    }
}