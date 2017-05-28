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
        LogLevel _level;

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

        protected LogLevel Level
        {
            get
            {
                return _level;
            }
        }

        public Logger(
            ILogWriter[] writers,
            string fullName,
            LogLevel level)
        {
            _writers = writers.ToList();
            _fullName = fullName;
            _level = level;
        }

        protected virtual void WriterLog(
            int eventID,
            LogLevel level,
            string text)
        {
            if (level < _level)
            {
                return;
            }

            var contect = new LogContext
            {
                ClassFullName = _fullName,
                CreateTime = DateTime.Now,
                EventID = eventID,
                Text = text,
                ThreadID = Thread.CurrentThread.ManagedThreadId,
                Level = level
            };

            foreach (var writer in _writers)
            {
                writer.Write(contect);
            }
        }

        #region ILogger 实现
        public void LogCritical(string format, params object[] args)
        {
            WriterLog(0, LogLevel.crit
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogCritical(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.crit
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogDebug(string format, params object[] args)
        {
            WriterLog(0, LogLevel.dbug
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogDebug(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.dbug
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogError(string format, params object[] args)
        {
            WriterLog(0, LogLevel.fail
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogError(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.fail
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogInformation(string format, params object[] args)
        {
            WriterLog(0, LogLevel.info
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogInformation(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.info
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogTrace(string format, params object[] args)
        {
            WriterLog(0, LogLevel.trce
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogTrace(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.trce
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogWarning(string format, params object[] args)
        {
            WriterLog(0, LogLevel.warn
                , args.Length == 0 ? format : string.Format(format, args));
        }

        public void LogWarning(int eventID, string format, params object[] args)
        {
            WriterLog(eventID, LogLevel.warn
                , args.Length == 0 ? format : string.Format(format, args));
        }
        #endregion
    }
}
