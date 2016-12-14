using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Logger.NullLogger
{
    /// <summary>
    /// 空日志对象工厂
    /// </summary>
    public class NullLoggerFactory : ILoggerFactory
    {
        readonly static ILogger _nullObject = new NullLogger();

        public ILogger CreateLogger<T>() where T : class
        {
            return _nullObject;
        }
    }
}
