using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D.Util.Logger.Console
{
    public class ConsoleLogger : ILogger
    {
        ILogWriter _writer;
        string _fullName;

        internal ConsoleLogger(ConsoleLogWriter writer, string fullName)
        {
            _writer = writer;
            _fullName = fullName;
        }

        public void LogCritical(string text)
        {
            WriteLog(LogContextType.crit, text);
        }

        public void LogDebug(string text)
        {
            WriteLog(LogContextType.dbug, text);
        }

        public void LogError(string text)
        {
            WriteLog(LogContextType.fail, text);
        }

        public void LogInformation(string text)
        {
            WriteLog(LogContextType.info, text);
        }

        public void LogTrace(string text)
        {
            WriteLog(LogContextType.trce, text);
        }

        public void LogWarning(string text)
        {
            WriteLog(LogContextType.warn, text);
        }

        private void WriteLog(LogContextType type, string txt)
        {
            var context = new LogContext()
            {
                Type = type,
                ClassFullName = _fullName,
                CreateTime = DateTime.Now,
                Text = txt,
                ThreadID = Thread.CurrentThread.ManagedThreadId
            };

            _writer.Write(context);
        }
    }
}
