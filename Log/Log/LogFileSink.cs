using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;                                                    // for streamWriter

namespace Logging
{
    // sink to allow logging to a local file
    public class LogFileSink : iLogSink
    {
        public LogFileSink(string logFilePath = @"c:\temp")
        {
            DateTime startTime = DateTime.Now;
      
            string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            string pathName = logFilePath + @"\" +
                               String.Format("{0:D2}{1:D2}{2:D2}_{3:D2}{4:D2}{5:D2}", startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second) + @" " +
                               processName + @".log.txt";

            sw = new StreamWriter(pathName);
            sw.AutoFlush = true;
        }


        // iLogSink members
        public void LogString(string type, string processID, string s)
        {
            DateTime time = DateTime.Now;
            if (sw != null)
            {
                sw.WriteLine("{0} [{1}] : {2} : {3}", time.ToString(), processID, type, s);
            }
        }

        public void CloseDown()
        {
            sw.Close();
            sw = null;
        }

        private StreamWriter sw;                        // the writer used to update the log file
    }
}
