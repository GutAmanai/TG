using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace System
{
    public class ExceptionCustom
    {
        private static StreamWriter sw;
          
        public static bool Log(Exception  objException)
        {
            bool bReturn            = false;
            string strException     = string.Empty;
            try
            {
                sw = new StreamWriter(GetLogFilePath(), true);
                sw.WriteLine("Source        : " +  objException.Source.ToString().Trim());
                sw.WriteLine("Method        : " +  objException.TargetSite.Name.ToString());
                sw.WriteLine("Date          : " +  DateTime.Now.ToLongTimeString());
                sw.WriteLine("Time          : " +  DateTime.Now.ToShortDateString());
                sw.WriteLine("Error         : " +  objException.Message.ToString().Trim());
                sw.WriteLine("Stack Trace   : " +  objException.StackTrace.ToString().Trim());
                sw.WriteLine("^^-------------------------------------------------------------------^^");
                sw.Flush();
                sw.Close();
                bReturn    = true;
            }
            catch(Exception)
            {
                bReturn    = false;
            }
            return bReturn;
        }


        /// <summary>
        /// Check the log file in applcation directory.
        /// If it is not available, creae it
        /// 
        /// <RETURNS>Log file path</RETURNS>
        ///</summary>
        private static string GetLogFilePath()
        {
            try
            {               
                string baseDir = AppDomain.CurrentDomain.RelativeSearchPath;
                string retFilePath = baseDir + "\\log.txt";

                if (!File.Exists(retFilePath))
                    File.Create(retFilePath);
                               
                return retFilePath;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }

        private static bool CheckDirectory(string strLogPath)
        {
            try
            {
                int nFindSlashPos  = strLogPath.Trim().LastIndexOf("\\");
                string strDirectoryname = 
                           strLogPath.Trim().Substring(0,nFindSlashPos);

                if (false == Directory.Exists(strDirectoryname))
                    Directory.CreateDirectory(strDirectoryname);
                return true;
            }
            catch(Exception)
            {
                return false;

            }
        }
    }
}
