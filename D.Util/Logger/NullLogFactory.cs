using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger
{
    public class NullLogFactory : ILoggerFactory
    {
        public ILogger CreateLogger<T>(LogLevel level = LogLevel.info) where T : class
        {
            return new NullLogger();
        }

        public ILogger CreateLogger<T>(string level) where T : class
        {
            return new NullLogger();
        }
    }
}
