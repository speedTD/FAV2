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
    public class ProgramPdtCtrlHistoryDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ProgramPdtCtrlHistoryDB));
        public ReturnProgramPdtCtrlHistory ListbyPage(int pageNumber, int pageSize)
        {
            List<ProgramPdtCtrlHistory> lstProgramPdtCtrlHistory = null;
            ProgramPdtCtrlHistory ProgramPdtCtrlHistory = null;
            ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrlHistory_SelectByPage";
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
                                lstProgramPdtCtrlHistory = new List<ProgramPdtCtrlHistory>();
                                while (sqlDr.Read())
                                {
                                    ProgramPdtCtrlHistory = new ProgramPdtCtrlHistory();
                                    ProgramPdtCtrlHistory.ProgramName = sqlDr["ProgramName"].ToString();
                                    ProgramPdtCtrlHistory.Part = sqlDr["Part"].ToString();
                                    ProgramPdtCtrlHistory.Parameter = sqlDr["Parameter"].ToString();
                                    ProgramPdtCtrlHistory.ControlItem = sqlDr["ControlItem"].ToString();
                                    ProgramPdtCtrlHistory.ColumnName = sqlDr["ColumnName"].ToString();
                                    ProgramPdtCtrlHistory.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    ProgramPdtCtrlHistory.Unit = sqlDr["Unit"].ToString();
                                    ProgramPdtCtrlHistory.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    ProgramPdtCtrlHistory.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    ProgramPdtCtrlHistory.ItemName = sqlDr["ItemName"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["HistoryOperatorID"].ToString()))
                                    {
                                        ProgramPdtCtrlHistory.HistoryOperatorID = Int32.Parse(sqlDr["HistoryOperatorID"].ToString());//Convert.ToInt32(sqlDr["HistoryOperatorID"].ToString());
                                        ProgramPdtCtrlHistory.HistoryOperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(ProgramPdtCtrlHistory.HistoryOperatorID));
                                    }
                                    if (!String.IsNullOrEmpty(sqlDr["OperatorID"].ToString()))
                                    {
                                        ProgramPdtCtrlHistory.OperatorID = Int32.Parse(sqlDr["OperatorID"].ToString());//Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                        ProgramPdtCtrlHistory.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(ProgramPdtCtrlHistory.OperatorID));
                                    }                                    
                                    ProgramPdtCtrlHistory.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    ProgramPdtCtrlHistory.HistoryDate = DateTime.Parse(sqlDr["HistoryDate"].ToString());
                                    ProgramPdtCtrlHistory.StatusCRUD = sqlDr["StatusCRUD"].ToString();
                                    
                                    lstProgramPdtCtrlHistory.Add(ProgramPdtCtrlHistory);
                                }
                                returnProgramPdtCtrlHistory.Code = "00";
                                returnProgramPdtCtrlHistory.Message = "Lấy dữ liệu thành công./Successful!";
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = lstProgramPdtCtrlHistory;
                                returnProgramPdtCtrlHistory.userID = MyShareInfo.ID;
                                returnProgramPdtCtrlHistory.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnProgramPdtCtrlHistory.Code = "01";
                                returnProgramPdtCtrlHistory.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrlHistory.Total = 0;
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                                returnProgramPdtCtrlHistory.userID = MyShareInfo.ID;
                                returnProgramPdtCtrlHistory.UserName = MyShareInfo.UserName;
                            }
                        }
                        //get return Totalpage value.
                        if (returnProgramPdtCtrlHistory.Code == "00")
                            returnProgramPdtCtrlHistory.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrlHistory.Code = "99";
                returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrlHistory.Total = 0;
                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrlHistory;
        }

        public ReturnProgramPdtCtrlHistory DeleteByKey(ProgramPdtCtrlHistory p)
        {
            ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrlHistory_DeleteByKey";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Parameter", SqlDbType.VarChar)).Value = p.Parameter;

                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.VarChar)).Value = p.ControlItem;

                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProgramPdtCtrlHistory.Code = "00";
                        returnProgramPdtCtrlHistory.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrlHistory.Code = "99";
                returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrlHistory.Total = 0;
                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrlHistory;
        }

        public ReturnProgramPdtCtrlHistory GetbyKey(ProgramPdtCtrlHistory p)
        {
            List<ProgramPdtCtrlHistory> lstProgramPdtCtrlHistory = null;
            ProgramPdtCtrlHistory programPdtCtrlHistory = null;
            ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProgramPdtCtrlHistory_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Parameter", SqlDbType.NVarChar)).Value = p.Parameter;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = p.ControlItem;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrlHistory = new List<ProgramPdtCtrlHistory>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrlHistory = new ProgramPdtCtrlHistory();
                                    programPdtCtrlHistory.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrlHistory.Parameter = sqlDr["Parameter"].ToString();
                                    programPdtCtrlHistory.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrlHistory.ColumnName = sqlDr["ColumnName"].ToString();
                                    programPdtCtrlHistory.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrlHistory.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrlHistory.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrlHistory.UpperLimit = sqlDr["UpperLimit"].ToString();

                                    if (!String.IsNullOrEmpty(sqlDr["HistoryOperatorID"].ToString()))
                                    {
                                        programPdtCtrlHistory.HistoryOperatorID = Int32.Parse(sqlDr["HistoryOperatorID"].ToString());//Convert.ToInt32(sqlDr["HistoryOperatorID"].ToString());
                                        programPdtCtrlHistory.HistoryOperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrlHistory.HistoryOperatorID));
                                    }
                                    if (!String.IsNullOrEmpty(sqlDr["OperatorID"].ToString()))
                                    {
                                        programPdtCtrlHistory.OperatorID = Int32.Parse(sqlDr["OperatorID"].ToString());//Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                        programPdtCtrlHistory.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrlHistory.OperatorID));
                                    }
                                    programPdtCtrlHistory.ItemName = sqlDr["ItemName"].ToString();
                                    lstProgramPdtCtrlHistory.Add(programPdtCtrlHistory);
                                }
                                returnProgramPdtCtrlHistory.Code = "00";
                                returnProgramPdtCtrlHistory.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = lstProgramPdtCtrlHistory;
                                returnProgramPdtCtrlHistory.Total = lstProgramPdtCtrlHistory.Count;
                                //}
                            }
                            else
                            {
                                returnProgramPdtCtrlHistory.Code = "01";
                                returnProgramPdtCtrlHistory.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrlHistory.Total = 0;
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrlHistory.Code = "99";
                returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrlHistory.Total = 0;
                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrlHistory;
        }

        public ReturnProgramPdtCtrlHistory Insert(ProgramPdtCtrlHistory p)
        {
            ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrlHistory_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = p.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Parameter", SqlDbType.VarChar)).Value = p.Parameter;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.VarChar)).Value = p.ControlItem;
                        cmd.Parameters.Add(new SqlParameter("@ColumnName", SqlDbType.VarChar)).Value = p.ColumnName;
                        cmd.Parameters.Add(new SqlParameter("@LowerLimit", SqlDbType.VarChar)).Value = p.LowerLimit;
                        cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.VarChar)).Value = p.Unit;
                        cmd.Parameters.Add(new SqlParameter("@UpperLimit", SqlDbType.VarChar)).Value = p.UpperLimit;
                        cmd.Parameters.Add(new SqlParameter("@HistoryOperatorID", SqlDbType.VarChar)).Value = p.HistoryOperatorID;
                        cmd.Parameters.Add(new SqlParameter("@SpecDisplay", SqlDbType.VarChar)).Value = p.SpecDisplay;
                        cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.VarChar)).Value = p.ItemName;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnProgramPdtCtrlHistory.Code = "00";
                        returnProgramPdtCtrlHistory.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrlHistory.Code = "99";
                returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrlHistory.Total = 0;
                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrlHistory;
        }

        //public ReturnProgramPdtCtrlHistory SearchProgramPdtCtrlHistory(ReturnProgramPdtCtrlHistory searchProgramPdtCtrlHistory, int pageSize)
        //{
        //    List<ProgramPdtCtrlHistory> lstProgramPdtCtrlHistory = null;
        //    ProgramPdtCtrlHistory programPdtCtrlHistory = null;
        //    ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
        //    try
        //    {
        //        using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("", sqlConnection))
        //            {
        //                cmd.CommandText = "sp_tProgramPdtCtrlHistory_Search";
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = searchProgramPdtCtrlHistory.aProgramPdtCtrlHistory.ProgramName;
        //                cmd.Parameters.Add(new SqlParameter("@Parameter", SqlDbType.NVarChar)).Value = searchProgramPdtCtrlHistory.aProgramPdtCtrlHistory.Parameter;
        //                cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = searchProgramPdtCtrlHistory.aProgramPdtCtrlHistory.ControlItem;

        //                cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = searchProgramPdtCtrlHistory.PageNumber;
        //                cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
        //                cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

        //                using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
        //                {
        //                    if (sqlDr.HasRows)
        //                    {
        //                        lstProgramPdtCtrlHistory = new List<ProgramPdtCtrlHistory>();
        //                        while (sqlDr.Read())
        //                        {
        //                            programPdtCtrlHistory = new ProgramPdtCtrlHistory();
        //                            programPdtCtrlHistory.ProgramName = sqlDr["ProgramName"].ToString();
        //                            programPdtCtrlHistory.Parameter = sqlDr["Parameter"].ToString();
        //                            programPdtCtrlHistory.ControlItem = sqlDr["ControlItem"].ToString();
        //                            programPdtCtrlHistory.SpecDisplay = sqlDr["SpecDisplay"].ToString();
        //                            programPdtCtrlHistory.Unit = sqlDr["Unit"].ToString();
        //                            programPdtCtrlHistory.UpperLimit = sqlDr["UpperLimit"].ToString();
        //                            programPdtCtrlHistory.LowerLimit = sqlDr["LowerLimit"].ToString();
        //                            programPdtCtrlHistory.ColumnName = sqlDr["ColumnName"].ToString();
        //                            programPdtCtrlHistory.OperatorID = Convert.ToInt32(sqlDr["OperatorID"].ToString());
        //                            programPdtCtrlHistory.HistoryOperatorID = Convert.ToInt32(sqlDr["HistoryOperatorID"].ToString());
        //                            programPdtCtrlHistory.ItemName = sqlDr["ItemName"].ToString();

        //                            lstProgramPdtCtrlHistory.Add(programPdtCtrlHistory);
        //                        }
        //                        returnProgramPdtCtrlHistory.Code = "00";
        //                        returnProgramPdtCtrlHistory.Message = "Lấy dữ liệu thành công.";
        //                        returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = lstProgramPdtCtrlHistory;
        //                    }
        //                    else
        //                    {
        //                        returnProgramPdtCtrlHistory.Code = "01";
        //                        returnProgramPdtCtrlHistory.Message = "Không tồn tại bản ghi nào.";
        //                        returnProgramPdtCtrlHistory.Total = 0;
        //                        returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
        //                    }
        //                }
        //                //get return Totalpage value.
        //                if (returnProgramPdtCtrlHistory.Code == "00")
        //                    returnProgramPdtCtrlHistory.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        returnProgramPdtCtrlHistory.Code = "99";
        //        returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
        //        returnProgramPdtCtrlHistory.Total = 0;
        //        returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
        //        mylog4net.Error("", ex);
        //    }
        //    return returnProgramPdtCtrlHistory;
        //}

        public ReturnProgramPdtCtrlHistory SearchProgramPdtCtrlHistory(ProgramPdtCtrlHistory searchProgramPdtCtrlHistory, int pageNumber, int pageSize)
        {
            List<ProgramPdtCtrlHistory> lstProgramPdtCtrlHistory = null;
            ProgramPdtCtrlHistory programPdtCtrlHistory = null;
            ReturnProgramPdtCtrlHistory returnProgramPdtCtrlHistory = new ReturnProgramPdtCtrlHistory();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProgramPdtCtrlHistory_Search";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.VarChar)).Value = searchProgramPdtCtrlHistory.ProgramName;
                        cmd.Parameters.Add(new SqlParameter("@Part", SqlDbType.NVarChar)).Value = searchProgramPdtCtrlHistory.Part;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = searchProgramPdtCtrlHistory.ControlItem;

                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstProgramPdtCtrlHistory = new List<ProgramPdtCtrlHistory>();
                                while (sqlDr.Read())
                                {
                                    programPdtCtrlHistory = new ProgramPdtCtrlHistory();
                                    programPdtCtrlHistory.ProgramName = sqlDr["ProgramName"].ToString();
                                    programPdtCtrlHistory.Parameter = sqlDr["Parameter"].ToString();
                                    programPdtCtrlHistory.ControlItem = sqlDr["ControlItem"].ToString();
                                    programPdtCtrlHistory.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    programPdtCtrlHistory.Unit = sqlDr["Unit"].ToString();
                                    programPdtCtrlHistory.UpperLimit = sqlDr["UpperLimit"].ToString();
                                    programPdtCtrlHistory.LowerLimit = sqlDr["LowerLimit"].ToString();
                                    programPdtCtrlHistory.ColumnName = sqlDr["ColumnName"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["HistoryOperatorID"].ToString()))
                                    {
                                        programPdtCtrlHistory.HistoryOperatorID = Int32.Parse(sqlDr["HistoryOperatorID"].ToString());//Convert.ToInt32(sqlDr["HistoryOperatorID"].ToString());
                                        programPdtCtrlHistory.HistoryOperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrlHistory.HistoryOperatorID));
                                    }
                                    if (!String.IsNullOrEmpty(sqlDr["OperatorID"].ToString()))
                                    {
                                        programPdtCtrlHistory.OperatorID = Int32.Parse(sqlDr["OperatorID"].ToString());//Convert.ToInt32(sqlDr["OperatorID"].ToString());
                                        programPdtCtrlHistory.OperatorName = (new UserDB()).getUserNameByID(Convert.ToInt32(programPdtCtrlHistory.OperatorID));
                                    }
                                    programPdtCtrlHistory.CreatedDate = DateTime.Parse(sqlDr["CreatedDate"].ToString());
                                    programPdtCtrlHistory.HistoryDate = DateTime.Parse(sqlDr["HistoryDate"].ToString());
                                    programPdtCtrlHistory.StatusCRUD = sqlDr["StatusCRUD"].ToString();

                                    lstProgramPdtCtrlHistory.Add(programPdtCtrlHistory);
                                }
                                returnProgramPdtCtrlHistory.Code = "00";
                                returnProgramPdtCtrlHistory.Message = "Lấy dữ liệu thành công.";
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = lstProgramPdtCtrlHistory;
                            }
                            else
                            {
                                returnProgramPdtCtrlHistory.Code = "01";
                                returnProgramPdtCtrlHistory.Message = "Không tồn tại bản ghi nào.";
                                returnProgramPdtCtrlHistory.Total = 0;
                                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                            }
                        }
                        //get return Totalpage value.
                        if (returnProgramPdtCtrlHistory.Code == "00")
                            returnProgramPdtCtrlHistory.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnProgramPdtCtrlHistory.Code = "99";
                returnProgramPdtCtrlHistory.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnProgramPdtCtrlHistory.Total = 0;
                returnProgramPdtCtrlHistory.lstProgramPdtCtrlHistory = null;
                mylog4net.Error("", ex);
            }
            return returnProgramPdtCtrlHistory;
        }
    }
}