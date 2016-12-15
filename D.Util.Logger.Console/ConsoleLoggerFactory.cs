using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger.Console
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        static readonly ConsoleLogWriter _writer = new ConsoleLogWriter();
        static readonly Dictionary<Type, ILogger> _records = new Dictionary<Type, ILogger>();

        public ILogger CreateLogger<T>() where T : class
        {
            var t = typeof(T);

            if (!_records.ContainsKey(t))
            {
                var logger = new ConsoleLogger(_writer, t.FullName);
                _records.Add(t, logger);
            }

            return _records[t];
        }
    }
}
