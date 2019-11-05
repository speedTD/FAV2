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
    public class MachineTypeDB
    {
        private static readonly ILog mylog4net = LogManager.GetLogger(typeof(MachineTypeDB));

        public ReturnMachineType MachineTypeAll()
        {
            List<MachineType> lstMachineType = null;
            MachineType machineType = null;
            ReturnMachineType returnMachineType = new ReturnMachineType();
            try
            {
                
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineType_SelectAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineType = new List<MachineType>();
                                while (sqlDr.Read())
                                {
                                    machineType = new MachineType();
                                    machineType.ID = int.Parse(sqlDr["ID"].ToString());
                                    machineType.TypeName = sqlDr["TypeName"].ToString();
                                    machineType.Description = sqlDr["Description"].ToString();
                                    machineType.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    lstMachineType.Add(machineType);
                                    
                                }

                                returnMachineType.Total = lstMachineType.Count;
                                returnMachineType.Code = "00";
                                returnMachineType.Message = "Lấy dữ liệu thành công.";
                                returnMachineType.lstMachineType = lstMachineType;
                            }
                            else
                            {
                                returnMachineType.Code = "01";
                                returnMachineType.Message = "Không tồn tại bản ghi nào.";
                                returnMachineType.Total = 0;
                                returnMachineType.lstMachineType = null;
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineType.Code = "99";
                returnMachineType.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineType.Total = 0;
                returnMachineType.lstMachineType = null;
                mylog4net.Error("", ex);
            }
            return returnMachineType;
        }

        public ReturnMachineType GetbyID(int ID)
        {
            List<MachineType> lstMachineType = null;
            MachineType machineType = null;
            ReturnMachineType returnMachineType = new ReturnMachineType();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineType_SelectByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineType = new List<MachineType>();
                                while (sqlDr.Read())
                                {
                                    machineType = new MachineType();
                                    machineType.ID = int.Parse(sqlDr["ID"].ToString());
                                    machineType.TypeName = sqlDr["TypeName"].ToString();
                                    machineType.Description = sqlDr["Description"].ToString();
                                    machineType.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    lstMachineType.Add(machineType);
                                }
                                returnMachineType.Code = "00";
                                returnMachineType.Message = "Lấy dữ liệu thành công.";
                                returnMachineType.lstMachineType = lstMachineType;
                            }
                            else
                            {
                                returnMachineType.Code = "01";
                                returnMachineType.Message = "Không tồn tại bản ghi nào.";
                                returnMachineType.Total = 0;
                                returnMachineType.lstMachineType = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineType.Code = "99";
                returnMachineType.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineType.Total = 0;
                returnMachineType.lstMachineType = null;
                mylog4net.Error("", ex);
            }
            return returnMachineType;
        }
        public ReturnMachineType GetbyTypeName(string typeName)
        {
            List<MachineType> lstMachineType = null;
            MachineType machineType = null;
            ReturnMachineType returnMachineType = new ReturnMachineType();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        //cmd.Connection = _sqlConnection;
                        cmd.CommandText = "sp_tMachineType_SelectByTypeName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TypeName", SqlDbType.NVarChar)).Value = typeName;
                        using (SqlDataReader sqlDr = ConnectSQLCommon.ExecuteDataReader(cmd, sqlConnection))
                        {
                            if (sqlDr.HasRows)
                            {
                                lstMachineType = new List<MachineType>();
                                while (sqlDr.Read())
                                {
                                    machineType = new MachineType();
                                    machineType.ID = int.Parse(sqlDr["ID"].ToString());
                                    machineType.TypeName = sqlDr["TypeName"].ToString();
                                    machineType.Description = sqlDr["Description"].ToString();
                                    machineType.isActive = SMCommon.ConvertToBoolean(sqlDr["isActive"].ToString());
                                    lstMachineType.Add(machineType);
                                }
                                returnMachineType.Code = "00";
                                returnMachineType.Message = "Lấy dữ liệu thành công.";
                                returnMachineType.lstMachineType = lstMachineType;
                                returnMachineType.Total = lstMachineType.Count;
                            }
                            else
                            {
                                returnMachineType.Code = "01";
                                returnMachineType.Message = "Không tồn tại bản ghi nào.";
                                returnMachineType.Total = 0;
                                returnMachineType.lstMachineType = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineType.Code = "99";
                returnMachineType.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineType.Total = 0;
                returnMachineType.lstMachineType = null;
                mylog4net.Error("", ex);
            }
            return returnMachineType;
        }

        public ReturnMachineType Insert(MachineType machineType)
        {
            ReturnMachineType returnMachineType = new ReturnMachineType();
            try
            {
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineType_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = machineType.ID;
                        cmd.Parameters.Add(new SqlParameter("@TypeName", SqlDbType.NVarChar)).Value = machineType.TypeName.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar)).Value = machineType.Description.Trim();
                        cmd.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Int)).Value = machineType.isActive;
                        cmd.ExecuteNonQuery();

                        returnMachineType.Code = "00";
                        if(machineType.ID==0) returnMachineType.Message = "Thêm mới dữ liệu thành công.";
                        else returnMachineType.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineType.Code = "99";
                returnMachineType.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineType.Total = 0;
                returnMachineType.lstMachineType = null;
                mylog4net.Error("", ex);
            }
            return returnMachineType;
        }


        public ReturnMachineType DeleteByID(int _ID)
        {
            ReturnMachineType returnMachineType = new ReturnMachineType();
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlConnection sqlConnection = ConnectSQLCommon.CreateAndOpenSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("", sqlConnection))
                    {
                        cmd.CommandText = "sp_tMachineType_DeleteByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = _ID;
                        cmd.ExecuteNonQuery();
                        returnMachineType.Code = "00";
                        returnMachineType.Message = "Cập nhật dữ liệu thành công.";
                    }
                }
            }
            catch (Exception ex)
            {
                returnMachineType.Code = "99";
                returnMachineType.Message = "Lỗi xử lý dữ liệu: " + ex.ToString();
                returnMachineType.Total = 0;
                returnMachineType.lstMachineType = null;
                mylog4net.Error("", ex);
            }
            return returnMachineType;
        }
    }
}