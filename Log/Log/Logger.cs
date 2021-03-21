using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Logging
{
    public interface iLogSink
    {
        void LogString(string type, string processID, string s);
        void CloseDown();
    }

    // class to allow log messages to be written to a file
    public class Log
    {
        // the one and only instance of the logger and an accessor to get it
        private static Log myInstance = new Log();
        public static Log Instance { get { return myInstance; } }

        // initialise the Log
        public Log()
        {
            ProcessID = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
        }


        // add a sink for logging
        public void AddSink(iLogSink sink)
        {
            Sinks.AddFirst (sink);
        }


        // private helper to actually do the initialise
        public void StartLogging ()
        {
            DateTime startTime = DateTime.Now;
            LogMsg("Logging started at " + startTime.ToShortTimeString() + ".");
        }

        // close the logging and render the logger unusable
        public void StopLogging()
        {
            LogMsg("Logging stopped.");
            foreach (iLogSink sink in Sinks)
            {
                sink.CloseDown();
            }
        }

        // add a fine debug level string to the log
        public void LogFineDebug(string msg)
        {
            //LogString(@"FINE", msg);
        }


        // add a debug level string to the log
        public void LogDebug(string msg)
        {
            LogString(@"DEBUG", msg);
        }


        // add a info level string to the log
        public void LogInfo(string msg)
        {
            LogString(@"INFO", msg);
        }
        
        
        // add a message level string to the log
        public void LogMsg(string msg)
        {
            LogString(@"MSG", msg);
        }

        // add a error level string to the log with a stack trace
        public void LogError(string error)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(error + Environment.NewLine);

            StackTrace st = new StackTrace();
            sb.Append (st.ToString());

            LogString(@"ERROR", sb.ToString());

        }

        // add a error level exception string to the log with a stack trace
        public void LogException(Exception e)
        {
            LogError(e.ToString() + Environment.NewLine);
        }


        // private helper to add a string to the log
        private void LogString(string type, string s)
        {
            foreach (iLogSink sink in Sinks)
            {
                sink.LogString(ProcessID, type, s);
            }
        }

        private LinkedList<iLogSink> Sinks = new LinkedList<iLogSink>();                    // the sinks to send log messages to
        private string ProcessID;                                               // the id of this process, written to the log file with each log line
    }
}

// to do
// log levels
// handle files which we can't open ansd throw an exception
// trim down stack trace?
// rolling log files for the file logger?
// remove sink?