using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 日志工厂
    /// 持有多个 logWriter 对象
    /// 创造 logger 对象，并且将持有的 writer 对象传递给 logger
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 生成一个日志对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ILogger CreateLogger<T>() where T : class;
    }
}
