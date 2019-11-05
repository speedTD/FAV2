using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace TestABC.Common
{
    public static class ConnectSQLCommon
    {
        private static string _SqlConnectionString = string.Empty;  
        public static string SM_SqlConnectionString
        { 
            get
            {
                if (String.IsNullOrEmpty(_SqlConnectionString))
                    _SqlConnectionString = ConfigurationManager.ConnectionStrings["TraceSqlConnectionString"].ConnectionString; 
                return _SqlConnectionString;
            }
            set
            { }
        }
           
        public static SqlConnection CreateAndOpenSqlConnection()
        {
            SqlConnection sqlConn = new SqlConnection(SM_SqlConnectionString);
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();
                return sqlConn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
                  
        public static SqlDataReader ExecuteDataReader(SqlCommand mSqlCommand)
        {
            try
            {
                return mSqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw;
                //LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteDataReader: " + exception.Message);
            }
        }
            
        public static SqlDataReader ExecuteDataReader(SqlCommand mSqlCommand, SqlConnection mConnection)
        {   
            try
            {
                mSqlCommand.Connection = mConnection;
                return mSqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw;
                //LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteDataReader: " + exception.Message);
            }
        }
    }
}