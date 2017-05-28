using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    /// <summary>
    /// ILoggerFactory 实现
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        const LogLevel _defaultLevel = LogLevel.info;

        ILogWriter[] _writers;
        Dictionary<Type, ILogger> _records = new Dictionary<Type, ILogger>();

        public LoggerFactory(
            ILogWriter[] writers
            )
        {
            _writers = writers;
        }

        public ILogger CreateLogger<T>(LogLevel level = LogLevel.info) where T : class
        {
            var t = typeof(T);

            if (!_records.ContainsKey(t))
            {
                var logger = new Logger(_writers, t.FullName, level);
                _records.Add(t, logger);
            }

            return _records[t];
        }

        public ILogger CreateLogger<T>(string level) where T : class
        {
            var l = _defaultLevel;

            try
            {
                l = (LogLevel)Enum.Parse(typeof(LogLevel), level);
            }
            catch
            {
                l = _defaultLevel;
            }

            return CreateLogger<T>(l);
        }
    }
}
