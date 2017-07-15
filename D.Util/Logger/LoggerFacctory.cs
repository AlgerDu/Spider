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
        const LogLevel _defaultLevel = LogLevel.trce;

        CommonConfig _config;

        ILogWriter[] _writers;
        Dictionary<Type, ILogger> _records = new Dictionary<Type, ILogger>();

        public LoggerFactory(
            ILogWriter[] writers
            , IConfig config
            )
        {
            _writers = writers;
            _config = config.GetItem<CommonConfig>();
        }

        public ILogger CreateLogger<T>(LogLevel level = LogLevel.trce) where T : class
        {
            var t = typeof(T);

            if (!_records.ContainsKey(t))
            {
                var trueLevel = JudgeClassLogLevel(t.FullName);

                var logger = new Logger(_writers, t.FullName, trueLevel);
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

        /// <summary>
        /// 判断 类名 所在的命名空间的日志等级
        /// </summary>
        /// <param name="classFullName"></param>
        /// <returns></returns>
        private LogLevel JudgeClassLogLevel(string classFullName)
        {
            if (_config == null || _config.Filters.Length <= 0)
            {
                return _defaultLevel;
            }

            var filter = _config.Filters.FirstOrDefault(f => classFullName.Contains(f.Namesapce));

            if (filter != null)
            {
                return filter.LogLevel;
            }
            else
            {
                return _defaultLevel;
            }
        }
    }
}
