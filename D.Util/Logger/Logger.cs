using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// ILogger 实现类，可以作为基类使用
    /// </summary>
    public class Logger : ILogger
    {
        IList<ILogWriter> _writers;
        string _fullName;

        /// <summary>
        /// IWriters 用于给子类使用
        /// </summary>
        protected IEnumerable<ILogWriter> Writers
        {
            get
            {
                return _writers;
            }
        }

        public Logger(
            ILogWriter[] writers,
            string fullName)
        {
            _writers = writers.ToList();
            _fullName = fullName;
        }

        protected virtual void WriterLog(
            int eventID,
            LogContextType type,
            string text)
        {
            var contect = new LogContext
            {
                ClassFullName = _fullName,
                CreateTime = DateTime.Now,
                EventID = eventID,
                Text = text,
                ThreadID = Thread.CurrentThread.ManagedThreadId,
                Type = type
            };

            foreach (var writer in _writers)
            {
                writer.Write(contect);
            }
        }

        #region ILogger 实现
        public void LogCritical(string format, params object[] args)
        {
            WriterLog(0, LogContextType.crit, string.Format(format, args));
        }

        public void LogCritical(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.crit, string.Format(format, args));
        }

        public void LogDebug(string format, params object[] args)
        {
            WriterLog(0, LogContextType.dbug, string.Format(format, args));
        }

        public void LogDebug(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.dbug, string.Format(format, args));
        }

        public void LogError(string format, params object[] args)
        {
            WriterLog(0, LogContextType.fail, string.Format(format, args));
        }

        public void LogError(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.fail, string.Format(format, args));
        }

        public void LogInformation(string format, params object[] args)
        {
            WriterLog(0, LogContextType.info, string.Format(format, args));
        }

        public void LogInformation(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.info, string.Format(format, args));
        }

        public void LogTrace(string format, params object[] args)
        {
            WriterLog(0, LogContextType.trce, string.Format(format, args));
        }

        public void LogTrace(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.trce, string.Format(format, args));
        }

        public void LogWarning(string format, params object[] args)
        {
            WriterLog(0, LogContextType.warn, string.Format(format, args));
        }

        public void LogWarning(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogContextType.warn, string.Format(format, args));
        }
        #endregion
    }
}
