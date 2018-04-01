using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Utility
{
    public class TraceLog
    {
        public static void whereAmI(StackFrame stackFrame, string additionalInfo)
        {
            string logInfo = "TRACE: ";
            string filename = stackFrame.GetFileName();
            if (filename != null)
            {
                logInfo += stackFrame.GetFileName();
            }
            int lineNumber = stackFrame.GetFileLineNumber();
            if (lineNumber != 0)
            {
                logInfo += "(line " + stackFrame.GetFileLineNumber() + "): ";
            }
            logInfo += stackFrame.GetMethod();
            if (additionalInfo != "")
            {
                logInfo += ", " + additionalInfo;
            }
            Log.Write(logInfo);
        }
    }
}
