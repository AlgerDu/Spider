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
        /// <param name="level"></param>
        /// <returns></returns>
        ILogger CreateLogger<T>(LogLevel level = LogLevel.info) where T : class;

        /// <summary>
        /// 生成一个日志对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">当传入的字符串错误时，默认为 info</param>
        /// <returns></returns>
        ILogger CreateLogger<T>(string level) where T : class;
    }
}
