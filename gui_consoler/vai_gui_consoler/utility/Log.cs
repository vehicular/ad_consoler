using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Utility
{
    public class Log
    {
        public class LogCtrlObj
        {
            private bool fool = true;
            public LogCtrlObj() { fool = false; }
        }

        //private static StreamWriter logFile;
        private static LogCtrlObj logctrlobj = new LogCtrlObj();
        
        private static FileStream fileStream;
        static string logFileName;

        public static bool InitLog(string path)
        {
            logFileName = path + "\\" + DateTime.Now.ToString("yyMMdd_hhmmss") + "_log.txt";
            //logFile = new StreamWriter(logFileName, true);
            fileStream = new FileStream(logFileName, FileMode.Create);
            fileStream.Close();
            return true;
        }

        private static void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fileStream.Write(info, 0, info.Length);  
        }
        
        public static bool Write(string msg)
        {
            System.Threading.Monitor.Enter(logctrlobj);
            
            try
            {
                fileStream = new FileStream(logFileName, FileMode.Append);
                string errorS = DateTime.Now.ToString("yyMMdd_hhmmss") + ": " +
                    msg;
                //logFile.WriteLine(errorS);
                AddText(errorS);
                AddText("\r\n");
            }
            finally
            {
                //logFile.Close();
                fileStream.Close();
            }
            System.Threading.Monitor.Exit(logctrlobj);
            return true;
        }

        public static void Close()
        {
            //logFile.Close();
            fileStream.Close();
        }
    }
}
