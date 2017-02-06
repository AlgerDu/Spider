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
        ILogWriter[] _writers;
        Dictionary<Type, ILogger> _records = new Dictionary<Type, ILogger>();

        public LoggerFactory(
            ILogWriter[] writers
            )
        {
            _writers = writers;
        }

        public ILogger CreateLogger<T>() where T : class
        {
            var t = typeof(T);

            if (!_records.ContainsKey(t))
            {
                var logger = new Logger(_writers, t.FullName);
                _records.Add(t, logger);
            }

            return _records[t];
        }
    }
}
