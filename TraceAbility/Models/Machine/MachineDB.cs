using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using TestABC.Common;

using log4net;
namespace TestABC.Models.Data
{
    public class MachineDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineDB));
        public ReturnMachine MachineAll()
        {
            List<Machine> lstMachine = null;
            Machine machine = null;
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineList_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            //if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                            //{
                            if (sqlDr.HasRows)
                            {
                                lstMachine = new List<Machine>();
                                while (sqlDr.Read())
                                {
                                    machine = new Machine();
                                    machine.MachineID = sqlDr["MachineID"].ToString();
                                    machine.MachineName = sqlDr["MachineName"].ToString();
                                    machine.MachineNumber = sqlDr["MachineNumber"].ToString();
                                    machine.Area = sqlDr["Area"].ToString();
                                    machine.Section = sqlDr["Section"].ToString();
                                    machine.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    machine.Maker = sqlDr["Maker"].ToString();                                
                                    machine.SerialNumber = sqlDr["SerialNumber"].ToString();
                                    machine.LineID = sqlDr["LineID"].ToString();
                                    machine.MachineTypeID = Int32.Parse(sqlDr["MachineTypeID"].ToString());
                                    machine.MachineTypeName = sqlDr["MachineTypeName"].ToString();
                                    //machine.isActive = CalculateCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    lstMachine.Add(machine);
                                }
                                returnMachine.Code = "00";
                                returnMachine.Message = "Lấy dữ liệu thành công.";
                                returnMachine.lstMachine = lstMachine;                                
                            }
                            else
                            {
                                returnMachine.Code = "01";
                                returnMachine.Message = "Không tồn tại bản ghi nào.";
                                returnMachine.Total = 0;
                                returnMachine.lstMachine = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }

        public ReturnMachine GetbyID(string MachineID)
       { 
            List<Machine> lstMachine = null;
            Machine machine = null;
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineList_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = MachineID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachine = new List<Machine>();
                                while (sqlDr.Read())
                                {
                                    machine = new Machine();
                                    machine.MachineID = sqlDr["MachineID"].ToString();
                                    machine.MachineName = sqlDr["MachineName"].ToString();
                                    machine.MachineNumber = sqlDr["MachineNumber"].ToString();
                                    machine.Area = sqlDr["Area"].ToString();
                                    machine.Section = sqlDr["Section"].ToString();
                                    machine.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    machine.Maker = sqlDr["Maker"].ToString();
                                    machine.SerialNumber = sqlDr["SerialNumber"].ToString();
                                    machine.LineID = sqlDr["LineID"].ToString();
                                    machine.MachineTypeID = Int32.Parse(sqlDr["MachineTypeID"].ToString());
                                    machine.MachineTypeName = sqlDr["MachineTypeName"].ToString();
                                    lstMachine.Add(machine);
                                }
                                returnMachine.Code = "00";
                                returnMachine.Message = "Lấy dữ liệu thành công.";
                                returnMachine.lstMachine = lstMachine;
                                returnMachine.Total = lstMachine.Count;
                            }
                            else
                            {
                                returnMachine.Code = "01";
                                returnMachine.Message = "Không tồn tại bản ghi nào.";
                                returnMachine.Total = 0;
                                returnMachine.lstMachine = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }

        public ReturnMachine CountbyLineID(string LineID)
        {
            List<Machine> lstMachine = null;
            Machine machine = null;
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineList_CountbyLineID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = LineID.Trim();

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                while (sqlDr.Read())
                                {
                                    returnMachine.lstMachine = null;
                                    returnMachine.Total = Convert.ToInt16(sqlDr[0].ToString());
                                }
                                returnMachine.Code = "00";
                                returnMachine.Message = "Lấy dữ liệu thành công.";

                                
                                //}
                            }
                            else
                            {
                                returnMachine.Code = "01";
                                returnMachine.Message = "Không tồn tại bản ghi nào.";
                                returnMachine.Total = 0;
                                returnMachine.lstMachine = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }


        public ReturnMachine GetbyMachineTypeID(int machineTypeID)
        {
            List<Machine> lstMachine = null;
            Machine machine = null;
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineList_SelectByMachineTypeID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = machineTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachine = new List<Machine>();
                                while (sqlDr.Read())
                                {
                                    machine = new Machine();
                                    machine.MachineID = sqlDr["MachineID"].ToString();
                                    machine.MachineName = sqlDr["MachineName"].ToString();
                                    machine.MachineNumber = sqlDr["MachineNumber"].ToString();
                                    machine.Area = sqlDr["Area"].ToString();
                                    machine.Section = sqlDr["Section"].ToString();
                                    machine.ReceiveDate = DateTime.Parse(sqlDr["ReceiveDate"].ToString());
                                    machine.Maker = sqlDr["Maker"].ToString();
                                    machine.SerialNumber = sqlDr["SerialNumber"].ToString();
                                    machine.LineID = sqlDr["LineID"].ToString();
                                    machine.MachineTypeID = Int32.Parse(sqlDr["MachineTypeID"].ToString());
                                    machine.MachineTypeName = sqlDr["MachineTypeName"].ToString();
                                    //machine.isActive = CalculateCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    lstMachine.Add(machine);
                                }
                                returnMachine.Code = "00";
                                returnMachine.Message = "Lấy dữ liệu thành công.";
                                //_ReturnTool.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                                returnMachine.lstMachine = lstMachine;
                                returnMachine.Total = lstMachine.Count;
                                //}
                            }
                            else
                            {
                                returnMachine.Code = "01";
                                returnMachine.Message = "Không tồn tại bản ghi nào.";
                                returnMachine.Total = 0;
                                returnMachine.lstMachine = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }
        public ReturnMachine Insert(Machine machine)
        {
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineList_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = machine.MachineID;
                        cmd.Parameters.Add(new SqlParameter("@MachineName", SqlDbType.NVarChar)).Value = machine.MachineName;
                        cmd.Parameters.Add(new SqlParameter("@MachineNumber", SqlDbType.NVarChar)).Value = machine.MachineNumber;
                        cmd.Parameters.Add(new SqlParameter("@Area", SqlDbType.VarChar)).Value = machine.Area;                   
                        cmd.Parameters.Add(new SqlParameter("@Section", SqlDbType.VarChar)).Value = machine.Section == null ? "" : machine.Section;
                        cmd.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime)).Value = machine.ReceiveDate;
                        cmd.Parameters.Add(new SqlParameter("@Maker", SqlDbType.VarChar)).Value = machine.Maker == null ? "" : machine.Maker;
                        cmd.Parameters.Add(new SqlParameter("@SerialNumber", SqlDbType.VarChar)).Value = machine.SerialNumber == null ? "" : machine.SerialNumber;
                        cmd.Parameters.Add(new SqlParameter("@LineID", SqlDbType.VarChar)).Value = machine.LineID;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = machine.MachineTypeID;
                        cmd.ExecuteNonQuery();

                        returnMachine.Code = "00";
                        returnMachine.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }
        public ReturnMachine DeleteByID(string _ID)
        {
            ReturnMachine returnMachine = new ReturnMachine();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineList_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineID", SqlDbType.VarChar)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnMachine.Code = "00";
                        returnMachine.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachine.Code = "99";
                returnMachine.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachine.Total = 0;
                returnMachine.lstMachine = null;
                mylog4net.Error("", ex);
            }
            return returnMachine;
        }
    }

}