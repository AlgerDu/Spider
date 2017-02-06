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
        public ILogger CreateLogger<T>() where T : class
        {
            return new Logger(new ILogWriter[] { new NullLogWriter() }, "");
        }
    }
}
