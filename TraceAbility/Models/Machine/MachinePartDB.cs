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
    public class MachinePartDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachinePartDB));
        public ReturnMachinePart MachinePartAll()
        {
            List<MachinePart> lstMachinePart = null;
            MachinePart machinePart = null;
            ReturnMachinePart returnMachinePart = new ReturnMachinePart();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachinePart_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachinePart = new List<MachinePart>();
                                while (sqlDr.Read())
                                {
                                    machinePart = new MachinePart();
                                    machinePart.ID = int.Parse(sqlDr["ID"].ToString());
                                    machinePart.Name = sqlDr["Name"].ToString();
                                    machinePart.Description = sqlDr["Description"].ToString();
                                    machinePart.MachineType = int.Parse(sqlDr["MachineType"].ToString());
                                    machinePart.DateCreated = DateTime.Parse(sqlDr["DateCreated"].ToString());
                                    machinePart.TypeName = sqlDr["TypeName"].ToString();
                                    lstMachinePart.Add(machinePart);
                                }
                                returnMachinePart.Code = "00";
                                returnMachinePart.Message = "Lấy dữ liệu thành công.";
                                returnMachinePart.lstMachinePart = lstMachinePart;
                            }
                            else
                            {
                                returnMachinePart.Code = "01";
                                returnMachinePart.Message = "Không tồn tại bản ghi nào.";
                                returnMachinePart.Total = 0;
                                returnMachinePart.lstMachinePart = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachinePart.Code = "99";
                returnMachinePart.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachinePart.Total = 0;
                returnMachinePart.lstMachinePart = null;
                mylog4net.Error("", ex);
            } 
            return returnMachinePart;
        }

        public ReturnMachinePart GetbyID(int ID)
        {
            List<MachinePart> lstMachinePart = null;
            MachinePart machinePart = null;
            ReturnMachinePart returnMachinePart = new ReturnMachinePart();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachinePart_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachinePart = new List<MachinePart>();
                                while (sqlDr.Read())
                                {
                                    machinePart = new MachinePart();
                                    machinePart.ID = int.Parse(sqlDr["ID"].ToString());
                                    machinePart.Name = sqlDr["Name"].ToString();
                                    machinePart.Description = sqlDr["Description"].ToString();
                                    machinePart.MachineType = int.Parse(sqlDr["MachineType"].ToString());
                                    machinePart.DateCreated = DateTime.Parse(sqlDr["DateCreated"].ToString());

                                    lstMachinePart.Add(machinePart);
                                }
                                returnMachinePart.Code = "00";
                                returnMachinePart.Message = "Lấy dữ liệu thành công.";
                                returnMachinePart.lstMachinePart = lstMachinePart;
                            }
                            else
                            {
                                returnMachinePart.Code = "01";
                                returnMachinePart.Message = "Không tồn tại bản ghi nào.";
                                returnMachinePart.Total = 0;
                                returnMachinePart.lstMachinePart = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachinePart.Code = "99";
                returnMachinePart.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachinePart.Total = 0;
                returnMachinePart.lstMachinePart = null;
                mylog4net.Error("", ex);
            }
            return returnMachinePart;
        }
        public ReturnMachinePart GetbyMachineType(int MachineTypeID)
        {
            List<MachinePart> lstMachinePart = null;
            MachinePart machinePart = null;
            ReturnMachinePart returnMachinePart = new ReturnMachinePart();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachinePart_SelectByMachineType";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@MachineTypeID", SqlDbType.Int)).Value = MachineTypeID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachinePart = new List<MachinePart>();
                                while (sqlDr.Read())
                                {
                                    machinePart = new MachinePart();
                                    machinePart.ID = int.Parse(sqlDr["ID"].ToString());
                                    machinePart.Name = sqlDr["Name"].ToString();
                                    machinePart.Description = sqlDr["Description"].ToString();
                                    machinePart.MachineType = int.Parse(sqlDr["MachineType"].ToString());
                                    machinePart.DateCreated = DateTime.Parse(sqlDr["DateCreated"].ToString());

                                    lstMachinePart.Add(machinePart);
                                }
                                returnMachinePart.Code = "00";
                                returnMachinePart.Message = "Lấy dữ liệu thành công.";
                                returnMachinePart.lstMachinePart = lstMachinePart;
                            }
                            else
                            {
                                returnMachinePart.Code = "01";
                                returnMachinePart.Message = "Không tồn tại bản ghi nào.";
                                returnMachinePart.Total = 0;
                                returnMachinePart.lstMachinePart = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachinePart.Code = "99";
                returnMachinePart.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachinePart.Total = 0;
                returnMachinePart.lstMachinePart = null;
                mylog4net.Error("", ex);
            }
            return returnMachinePart;
        }
        public ReturnMachinePart Insert(MachinePart machinePart)
        {
            ReturnMachinePart returnMachinePart = new ReturnMachinePart();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachinePart_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machinePart.ID;
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)).Value = machinePart.Name;
                        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = machinePart.Description;
                        cmd.Parameters.Add(new SqlParameter("@MachineType", SqlDbType.Int)).Value = machinePart.MachineType;
                        cmd.ExecuteNonQuery();

                        returnMachinePart.Code = "00";
                        returnMachinePart.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachinePart.Code = "99";
                returnMachinePart.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachinePart.Total = 0;
                returnMachinePart.lstMachinePart = null;
                mylog4net.Error("", ex);
            }
            return returnMachinePart;
        }


        public ReturnMachinePart DeleteByID(int _ID)
        {
            ReturnMachinePart returnMachinePart = new ReturnMachinePart();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachinePart_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        //cmd.Parameters.Add("@RETURN_CODE", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //int RETURN_CODE = Convert.ToInt32(cmd.Parameters["@RETURN_CODE"].Value);
                        cmd.ExecuteNonQuery();
                        returnMachinePart.Code = "00";
                        returnMachinePart.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachinePart.Code = "99";
                returnMachinePart.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachinePart.Total = 0;
                returnMachinePart.lstMachinePart = null;
                mylog4net.Error("", ex);
            }
            return returnMachinePart;
        }
    }
}