using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TestABC.Common;
using log4net;
namespace TestABC.Models.Data
{
    public class MachineMtnDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineMtnDB));

        public ReturnMachineMtn Insert(MachineMtn machineMtn)
        {
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtn.ID;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineMtn.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@OperatorID", SqlDbType.VarChar)).Value = machineMtn.OperatorID;
                        if (machineMtn.Shift != 0)
                            cmd.Parameters.Add(new SqlParameter("@Shift", SqlDbType.SmallInt)).Value = machineMtn.Shift;
                        else
                            cmd.Parameters.Add(new SqlParameter("@Shift", SqlDbType.SmallInt)).Value = DBNull.Value;
                        cmd.Parameters.Add(new SqlParameter("@MaintenanceDate", SqlDbType.DateTime)).Value = machineMtn.MaintenanceDate;
                        cmd.Parameters.Add(new SqlParameter("@FrequencyID", SqlDbType.Int)).Value = machineMtn.FrequencyID;
                        cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = machineMtn.Result;
                        cmd.Parameters.Add(new SqlParameter("@ResultContents", SqlDbType.NVarChar)).Value = "";
                        if (machineMtn.Week != 0)
                            cmd.Parameters.Add(new SqlParameter("@Week", SqlDbType.Int)).Value = machineMtn.Week;
                        else
                            cmd.Parameters.Add(new SqlParameter("@Week", SqlDbType.Int)).Value = DBNull.Value;

                        if (machineMtn.Month != 0)
                            cmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.SmallInt)).Value = machineMtn.Month;
                        else
                            cmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.SmallInt)).Value = DBNull.Value;
                        if (machineMtn.Year != 0)
                            cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int)).Value = machineMtn.Year;
                        else
                            cmd.Parameters.Add(new SqlParameter("@Year", SqlDbType.Int)).Value = DBNull.Value;

                        cmd.Parameters.Add(new SqlParameter("@CheckerID", SqlDbType.Int)).Value = machineMtn.CheckerID;
                        cmd.Parameters.Add(new SqlParameter("@CheckerResult", SqlDbType.VarChar)).Value = machineMtn.CheckerResult;
                        cmd.Parameters.Add("@LastID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        returnMachineMtn.Code = "00";
                        returnMachineMtn.Message = "Cập nhật dữ liệu thành công.";
                        //if (returnMachineMtn.Code == "00")
                        //    returnMachineMtn.LastID = Convert.ToInt32(cmd.Parameters["@LastID"].Value);
                        if (returnMachineMtn.Code == "00")
                        {
                            returnMachineMtn.LastID = Convert.ToInt32(cmd.Parameters["@LastID"].Value);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("", ex);

            }
            return returnMachineMtn;
        }
        public ReturnMachineMtn SelectByPage(string where, int pageNumber, int pageSize)
        {
            List<MachineMtn> lstMachineMtn = null;
            MachineMtn machineMtn = null;
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_SearchBypage";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;
                        //paging
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", SqlDbType.Int)).Value = pageNumber;
                        cmd.Parameters.Add(new SqlParameter("@pageSize", SqlDbType.Int)).Value = pageSize;
                        cmd.Parameters.Add("@totalRow", SqlDbType.Int).Direction = ParameterDirection.Output;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtn = new List<MachineMtn>();
                                while (sqlDr.Read())
                                {
                                    machineMtn = new MachineMtn();
                                    machineMtn.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtn.MachineID = sqlDr["MachineID"].ToString();
                                    machineMtn.MachineName = sqlDr["MachineName"].ToString();
                                    machineMtn.OperatorID = sqlDr["OperatorID"].ToString();
                                    machineMtn.OperatorName = sqlDr["UserName"].ToString();   // UserName in Sql = operatorName in C#
                                    if (!String.IsNullOrEmpty(sqlDr["Shift"].ToString()))
                                        machineMtn.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    machineMtn.MaintenanceDate = Convert.ToDateTime(sqlDr["MaintenanceDate"].ToString());
                                    machineMtn.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtn.Result = sqlDr["Result"].ToString();
                                    machineMtn.ResultContents = sqlDr["ResultContents"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["Month"].ToString()))
                                        machineMtn.Month = Convert.ToInt16(sqlDr["Month"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Year"].ToString()))
                                        machineMtn.Year = Convert.ToInt16(sqlDr["Year"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Week"].ToString()))
                                        machineMtn.Week = Convert.ToInt16(sqlDr["Week"].ToString());
                                    machineMtn.CheckerID = Convert.ToInt16(sqlDr["CheckerID"].ToString());
                                    //get Checker name.
                                    int _checkerID = 0;
                                    machineMtn.CheckerName = "";
                                    if (Int32.TryParse(sqlDr["CheckerID"].ToString(), out _checkerID))
                                    {
                                        ReturnUser returnUser = (new UserDB()).GetbyID(_checkerID);
                                        if (returnUser.Code == "00")
                                            machineMtn.CheckerName = returnUser.lstUser[0].UserName;
                                    }
                                    machineMtn.CheckerResult = sqlDr["CheckerResult"].ToString();
                                    lstMachineMtn.Add(machineMtn);
                                }
                                returnMachineMtn.Code = "00";
                                returnMachineMtn.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtn.lstMachineMtn = lstMachineMtn;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnMachineMtn.Code = "01";
                                returnMachineMtn.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtn.Total = 0;
                                returnMachineMtn.lstMachineMtn = null;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                        }
                        //paging.
                        if (returnMachineMtn.Code == "00")
                            returnMachineMtn.Total = Convert.ToInt32(cmd.Parameters["@totalRow"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu/Error: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("public ReturnMachineMtn SelectByCondition(string where, int pageSize) ", ex);
            }
            return returnMachineMtn;
        }
        public ReturnMachineMtn SelectByCondition(string where)
        {
            List<MachineMtn> lstMachineMtn = null;
            MachineMtn machineMtn = null;
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_SelectByCondition";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@where", SqlDbType.NVarChar)).Value = where;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtn = new List<MachineMtn>();
                                while (sqlDr.Read())
                                {
                                    machineMtn = new MachineMtn();
                                    machineMtn.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtn.MachineID = sqlDr["MachineID"].ToString();
                                    machineMtn.MachineName = sqlDr["MachineName"].ToString();
                                    machineMtn.OperatorID = sqlDr["OperatorID"].ToString();
                                    machineMtn.OperatorName = sqlDr["UserName"].ToString();   // UserName in Sql = operatorName in C#
                                    if (!String.IsNullOrEmpty(sqlDr["Shift"].ToString()))
                                        machineMtn.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    machineMtn.MaintenanceDate = Convert.ToDateTime(sqlDr["MaintenanceDate"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["FrequencyID"].ToString()))
                                        machineMtn.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtn.Result = sqlDr["Result"].ToString();
                                    machineMtn.ResultContents = sqlDr["ResultContents"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["Month"].ToString()))
                                        machineMtn.Month = Convert.ToInt16(sqlDr["Month"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Year"].ToString()))
                                        machineMtn.Year = Convert.ToInt16(sqlDr["Year"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Week"].ToString()))
                                        machineMtn.Week = Convert.ToInt16(sqlDr["Week"].ToString());
                                    machineMtn.CheckerID = Convert.ToInt16(sqlDr["CheckerID"].ToString());
                                    //get Checker name.
                                    int _checkerID = 0;
                                    machineMtn.CheckerName = "";
                                    if (Int32.TryParse(sqlDr["CheckerID"].ToString(), out _checkerID))
                                    {
                                        ReturnUser returnUser = (new UserDB()).GetbyID(_checkerID);
                                        if (returnUser.Code == "00")
                                            machineMtn.CheckerName = returnUser.lstUser[0].UserName;
                                    }
                                    machineMtn.CheckerResult = sqlDr["CheckerResult"].ToString();
                                    lstMachineMtn.Add(machineMtn);
                                }
                                returnMachineMtn.Code = "00";
                                returnMachineMtn.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtn.lstMachineMtn = lstMachineMtn;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnMachineMtn.Code = "01";
                                returnMachineMtn.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtn.Total = 0;
                                returnMachineMtn.lstMachineMtn = null;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("public ReturnMachineMtn SelectByCondition(string where, int pageSize) ", ex);
            }
            return returnMachineMtn;
        }

        public ReturnMachineMtn DeleteByID(int _ID)
        {
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnMachineMtn.Code = "00";
                        returnMachineMtn.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtn;
        }

        //public ReturnMachineMtn DeleteMachineMtnDetailByID(int _ID)
        //{
        //    //ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
        //    //try
        //    //{
        //    //    // Gọi vào DB để lấy dữ liệu.
        //    //    using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
        //    //    {
        //    //        using (SqlCommand cmd = new SqlCommand("", sqlConnection))
        //    //        {
        //    //            cmd.CommandText = "sp_tMachineMtn_DeleteByID";
        //    //            cmd.CommandType = CommandType.StoredProcedure;
        //    //            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
        //    //            //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
        //    //            //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
        //    //            cmd.ExecuteNonQuery();
        //    //            returnMachineMtn.Code = "00";
        //    //            returnMachineMtn.Message = "Cập nhật dữ liệu thành công.";
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    returnMachineMtn.Code = "99";
        //    //    returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
        //    //    returnMachineMtn.Total = 0;
        //    //    returnMachineMtn.lstMachineMtn = null;
        //    //    mylog4net.Error("", ex);
        //    //}
        //    //return returnMachineMtn;

        //}

        public ReturnMachineMtn GetMachineMtnbyID(int ID)
        {
            List<MachineMtn> lstMachineMtn = null;
            MachineMtn machineMtn = null;
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtn = new List<MachineMtn>();
                                while (sqlDr.Read())
                                {
                                    machineMtn = new MachineMtn();
                                    machineMtn.ID = Convert.ToInt16(sqlDr["ID"].ToString());
                                    machineMtn.MachineID = sqlDr["MachineID"].ToString();
                                    //get MachineName.
                                    ReturnMachine returnMachine = (new MachineDB()).GetbyID(machineMtn.MachineID);
                                    if (returnMachine.Code == "00")
                                        machineMtn.MachineName = returnMachine.lstMachine[0].MachineName;
                                    //get OperatorName.
                                    int _operatorID = 0;
                                    ReturnUser returnUser;
                                    if (Int32.TryParse(sqlDr["OperatorID"].ToString(), out _operatorID))
                                    {
                                        returnUser = (new UserDB()).GetbyID(_operatorID);
                                        if (returnUser.Code == "00")
                                            machineMtn.OperatorName = returnUser.lstUser[0].UserName;
                                    }
                                    machineMtn.Shift = Convert.ToInt16(sqlDr["Shift"].ToString());
                                    machineMtn.MaintenanceDate = Convert.ToDateTime(sqlDr["MaintenanceDate"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["FrequencyID"].ToString()))
                                        machineMtn.FrequencyID = Convert.ToInt16(sqlDr["FrequencyID"].ToString());
                                    machineMtn.Result = sqlDr["Result"].ToString();
                                    machineMtn.ResultContents = sqlDr["ResultContents"].ToString();
                                    if (!String.IsNullOrEmpty(sqlDr["Month"].ToString()))
                                        machineMtn.Month = Convert.ToInt16(sqlDr["Month"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Year"].ToString()))
                                        machineMtn.Year = Convert.ToInt16(sqlDr["Year"].ToString());
                                    if (!String.IsNullOrEmpty(sqlDr["Week"].ToString()))
                                        machineMtn.Week = Convert.ToInt16(sqlDr["Week"].ToString());
                                    if (sqlDr["CheckerID"].ToString() != "")
                                    {
                                        machineMtn.CheckerID = Convert.ToInt16(sqlDr["CheckerID"].ToString());
                                        //get name.
                                        int _CheckerID = 0;
                                        ReturnUser returnChecker;
                                        if (Int32.TryParse(sqlDr["CheckerID"].ToString(), out _operatorID))
                                        {
                                            returnChecker = (new UserDB()).GetbyID(_CheckerID);
                                            if (returnChecker.Code == "00")
                                                machineMtn.CheckerName = returnChecker.lstUser[0].UserName;
                                        }
                                    }
                                    if (sqlDr["CheckerResult"].ToString() != "")
                                    {
                                        machineMtn.CheckerResult = sqlDr["CheckerResult"].ToString();
                                    }
                                    lstMachineMtn.Add(machineMtn);
                                }
                                returnMachineMtn.Code = "00";
                                returnMachineMtn.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtn.lstMachineMtn = lstMachineMtn;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnMachineMtn.Code = "01";
                                returnMachineMtn.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtn.Total = 0;
                                returnMachineMtn.lstMachineMtn = null;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtn;
        }
        public ReturnMachineMtn CountMachineMtByMachineID(string machineID)
        {
            List<MachineMtn> lstMachineMtn = null;
            MachineMtn machineMtn = null;
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_CountByMachineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineID;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineMtn = new List<MachineMtn>();
                                while (sqlDr.Read())
                                {
                                    returnMachineMtn.Total = Convert.ToInt16(sqlDr[0].ToString());
                                    returnMachineMtn.UserID = MyShareInfo.ID;
                                    returnMachineMtn.UserName = MyShareInfo.UserName;
                                }
                                returnMachineMtn.Code = "00";
                                returnMachineMtn.Message = "Lấy dữ liệu thành công.";
                                returnMachineMtn.lstMachineMtn = lstMachineMtn;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                            else
                            {
                                returnMachineMtn.Code = "01";
                                returnMachineMtn.Message = "Không tồn tại bản ghi nào.";
                                returnMachineMtn.Total = 0;
                                returnMachineMtn.lstMachineMtn = null;
                                returnMachineMtn.UserID = MyShareInfo.ID;
                                returnMachineMtn.UserName = MyShareInfo.UserName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtn;
        }
        public ReturnMachineMtn UpdateCheckerResult_ByMachineMtnID(MachineMtn machineMtn)
        {
            ReturnMachineMtn returnMachineMtn = new ReturnMachineMtn();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineMtn_Update_CheckerResult";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineMtn.ID;
                        cmd.Parameters.Add(new SqlParameter("@CheckerID", SqlDbType.Int)).Value = machineMtn.CheckerID;
                        cmd.Parameters.Add(new SqlParameter("@CheckerResult", SqlDbType.VarChar)).Value = machineMtn.CheckerResult;
                        cmd.ExecuteNonQuery();
                        returnMachineMtn.Code = "00";
                        returnMachineMtn.Message = "Cập nhật dữ liệu thành công.";
                    }
                }

            }
            catch (Exception ex)
            {
                returnMachineMtn.Code = "99";
                returnMachineMtn.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineMtn.Total = 0;
                returnMachineMtn.lstMachineMtn = null;
                mylog4net.Error("", ex);
            }
            return returnMachineMtn;

        }
    }
}