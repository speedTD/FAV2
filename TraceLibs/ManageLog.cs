using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace TraceLibs
{
    public class ManageLog
    {
        private const string LOG_FILE = "Trace_Machine_ErrorLog.txt";
        //MB
        private const int LOG_FILE_SIZE = 10;

        public static void WriteOperation(string strOperation)
        {
            var strLogString = string.Empty;

            strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "---------------" + "  " +
                           "\r\n" + strOperation;

            WriteLog(strLogString);
        }

        
        public static void WriteOperation(string strObjectName, string strOperation)
        {
            var strLogString = string.Empty;

            strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "---------------" + "\r\n" +
                           "  " + strObjectName + "\r\n" + strOperation;

            WriteLog(strLogString);
        }

       
        public static void WriteError(Exception ex)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "---------------" +
                               "\r\n" + "Error：" + "\r\n";
            strLogString += ex.Message + "\r\n";
            strLogString += ex.StackTrace;
            WriteLog(strLogString);
        }

        
        public static void WriteError(string strError)
        {
            var strLogString = "---------------" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "---------------" +
                               "\r\n" + "Error:" + "\r\n";
            strLogString += strError;
            WriteLog(strLogString);
        }

        
        public static void WriteLog(string logContent)
        {
            string strLogDir;
            string strFileName;

            //strLogDir = SystemConfig.AppRoot;
            strLogDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            if (strLogDir == null)
            {
                return;
            }
            if (!strLogDir.EndsWith("\\"))
                strLogDir += "\\";

            strFileName = strLogDir + LOG_FILE;

            try
            {
                var charset = Encoding.GetEncoding("UTF-8");
                if (!File.Exists(strFileName))
                {
                    FileStream oFile;
                    oFile = File.Create(strFileName);
                    var oReader = new StreamWriter(oFile, charset);
                    oReader.WriteLine(logContent);
                    oReader.Close();
                    oFile.Close();
                }
                else
                {
                    // Append text in file when file exitsed
                    var oFile1 = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    if (oFile1.Length > LOG_FILE_SIZE * (1024 * 1024))
                    {
                        oFile1.Close();
                        File.Delete(strFileName);
                    }
                    else
                    {
                        oFile1.Close();
                    }
                    var oReader = new StreamWriter(strFileName, true, charset);
                    oReader.WriteLine(logContent);
                    oReader.Close();
                }
            }
            catch (Exception ex)
            {
                // Throw ex
            }
        }
    }
}
