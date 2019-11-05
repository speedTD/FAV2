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
    public class ConditionSettingDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(ConditionSettingDB));
        public ReturnConditionSetting ConditionSettingAll()
        {
            List<ConditionSetting> lstConditionSetting = null;
            ConditionSetting conditionSetting = null;
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControlList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstConditionSetting = new List<ConditionSetting>();
                                while (sqlDr.Read())
                                {
                                    conditionSetting = new ConditionSetting();
                                    conditionSetting.ID = int.Parse(sqlDr["ID"].ToString());
                                    conditionSetting.LineID = sqlDr["LineID"].ToString();
                                    conditionSetting.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    conditionSetting.MachineTypeName = sqlDr["TypeName"].ToString();
                                    conditionSetting.PatternCode = sqlDr["PatternCode"].ToString();
                                    conditionSetting.ControlItem = sqlDr["ControlItem"].ToString();
                                    conditionSetting.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    conditionSetting.Unit = sqlDr["Unit"].ToString();
                                    conditionSetting.LowerLimit = float.Parse(sqlDr["LowerLimit"].ToString());
                                    conditionSetting.UpperLimit = float.Parse(sqlDr["UpperLimit"].ToString());

                                    lstConditionSetting.Add(conditionSetting);
                                }
                                returnConditionSetting.Code = "00";
                                returnConditionSetting.Message = "Lấy dữ liệu thành công.";
                                returnConditionSetting.lstConditionSetting = lstConditionSetting;
                            }
                            else
                            {
                                returnConditionSetting.Code = "01";
                                returnConditionSetting.Message = "Không tồn tại bản ghi nào.";
                                returnConditionSetting.Total = 0;
                                returnConditionSetting.lstConditionSetting = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
            return returnConditionSetting;
        }

        public ReturnConditionSetting GetbyID(int ID)
        {
            List<ConditionSetting> lstConditionSetting = null;
            ConditionSetting conditionSetting = null;
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionControlList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstConditionSetting = new List<ConditionSetting>();
                                while (sqlDr.Read())
                                {
                                    conditionSetting = new ConditionSetting();
                                    conditionSetting.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    conditionSetting.LineID = sqlDr["LineID"].ToString();
                                    conditionSetting.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    conditionSetting.MachineTypeName = sqlDr["TypeName"].ToString();
                                    conditionSetting.PatternCode = sqlDr["PatternCode"].ToString();
                                    conditionSetting.ControlItem = sqlDr["ControlItem"].ToString();
                                    conditionSetting.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    conditionSetting.Unit = sqlDr["Unit"].ToString();
                                    conditionSetting.LowerLimit = float.Parse(sqlDr["LowerLimit"].ToString());
                                    conditionSetting.UpperLimit = float.Parse(sqlDr["UpperLimit"].ToString());

                                    lstConditionSetting.Add(conditionSetting);
                                }
                                returnConditionSetting.Code = "00";
                                returnConditionSetting.Message = "Lấy dữ liệu thành công.";
                                //_ReturnTool.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnConditionSetting.lstConditionSetting = lstConditionSetting;
                                //}
                            }
                            else
                            {
                                returnConditionSetting.Code = "01";
                                returnConditionSetting.Message = "Không tồn tại bản ghi nào.";
                                returnConditionSetting.Total = 0;
                                returnConditionSetting.lstConditionSetting = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
            return returnConditionSetting;
        }

        public ReturnConditionSetting Insert(ConditionSetting conditionSetting)
        {
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControlList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = conditionSetting.ID;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = conditionSetting.LineID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.VarChar)).Value = conditionSetting.MachineTypeID;
                        cmd.Parameters.Add(new SqlParameter("@PatternCode", SqlDbType.VarChar)).Value = conditionSetting.PatternCode;
                        cmd.Parameters.Add(new SqlParameter("@ControlItem", SqlDbType.NVarChar)).Value = conditionSetting.ControlItem;
                        cmd.Parameters.Add(new SqlParameter("@SpecDisplay", SqlDbType.NVarChar)).Value = conditionSetting.SpecDisplay;
                        cmd.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar)).Value = conditionSetting.Unit;
                        cmd.Parameters.Add(new SqlParameter("@LowerLimit", SqlDbType.Float)).Value = conditionSetting.LowerLimit;
                        cmd.Parameters.Add(new SqlParameter("@UpperLimit", SqlDbType.Float)).Value = conditionSetting.UpperLimit;
                        cmd.ExecuteNonQuery();

                        returnConditionSetting.Code = "00";
                        returnConditionSetting.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
            return returnConditionSetting;
        }

        public ReturnConditionSetting DeleteByID(int _ID)
        {
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tProductionControlList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnConditionSetting.Code = "00";
                        returnConditionSetting.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
            return returnConditionSetting;
        }


        public ReturnConditionSetting GetbyMachinePattern(string lineID, string machineID, string patternCode)
        {
            List<ConditionSetting> lstConditionSetting = null;
            ConditionSetting conditionSetting = null;
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionControlList_SelectByMachinePattern";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = lineID;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machineID;
                        cmd.Parameters.Add(new SqlParameter("@PatternCode", SqlDbType.VarChar)).Value = patternCode;


                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstConditionSetting = new List<ConditionSetting>();
                                while (sqlDr.Read())
                                {
                                    conditionSetting = new ConditionSetting();
                                    conditionSetting.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    conditionSetting.LineID = sqlDr["LineID"].ToString();
                                    conditionSetting.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    conditionSetting.PatternCode = sqlDr["PatternCode"].ToString();
                                    conditionSetting.ControlItem = sqlDr["ControlItem"].ToString();
                                    conditionSetting.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    conditionSetting.Unit = sqlDr["Unit"].ToString();
                                    conditionSetting.LowerLimit = float.Parse(sqlDr["LowerLimit"].ToString());
                                    conditionSetting.UpperLimit = float.Parse(sqlDr["UpperLimit"].ToString());

                                    lstConditionSetting.Add(conditionSetting);
                                }
                                returnConditionSetting.Code = "00";
                                returnConditionSetting.Message = "Lấy dữ liệu thành công.";
                                //_ReturnTool.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnConditionSetting.lstConditionSetting = lstConditionSetting;
                                //}
                            }
                            else
                            {
                                returnConditionSetting.Code = "01";
                                returnConditionSetting.Message = "Không tồn tại bản ghi nào.";
                                returnConditionSetting.Total = 0;
                                returnConditionSetting.lstConditionSetting = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
            return returnConditionSetting;
        }
        public ReturnConditionSetting GetbyMachineTypeID(string MachineID, string BatchNo)
        {
            List<ConditionSetting> lstConditionSetting = null;
            ConditionSetting conditionSetting = null;
            ReturnConditionSetting returnConditionSetting = new ReturnConditionSetting();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tProductionControlList_SelectByMachineTypeID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;
                        cmd.Parameters.Add(new SqlParameter("@BatchNo", SqlDbType.VarChar)).Value = BatchNo;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstConditionSetting = new List<ConditionSetting>();
                                while (sqlDr.Read())
                                {
                                    conditionSetting = new ConditionSetting();
                                    conditionSetting.ID = Convert.ToInt32(sqlDr["ID"].ToString());
                                    conditionSetting.LineID = sqlDr["LineID"].ToString();
                                    conditionSetting.MachineTypeID = Convert.ToInt32(sqlDr["MachineTypeID"].ToString());
                                    conditionSetting.PatternCode = sqlDr["PatternCode"].ToString();
                                    conditionSetting.ControlItem = sqlDr["ControlItem"].ToString();
                                    conditionSetting.SpecDisplay = sqlDr["SpecDisplay"].ToString();
                                    conditionSetting.Unit = sqlDr["Unit"].ToString();
                                    conditionSetting.LowerLimit = float.Parse(sqlDr["LowerLimit"].ToString());
                                    conditionSetting.UpperLimit = float.Parse(sqlDr["UpperLimit"].ToString());

                                    lstConditionSetting.Add(conditionSetting);
                                }
                                returnConditionSetting.Code = "00";
                                returnConditionSetting.Message = "Lấy dữ liệu thành công.";
                                //_ReturnTool.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnConditionSetting.lstConditionSetting = lstConditionSetting;
                                //}
                            }
                            else
                            {
                                returnConditionSetting.Code = "01";
                                returnConditionSetting.Message = "Không tồn tại bản ghi nào.";
                                returnConditionSetting.Total = 0;
                                returnConditionSetting.lstConditionSetting = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnConditionSetting.Code = "99";
                returnConditionSetting.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnConditionSetting.Total = 0;
                returnConditionSetting.lstConditionSetting = null;
                mylog4net.Error("", ex);
            }
           return returnConditionSetting;
        }
    }
}