using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    public enum LogContextType
    {
        trce,
        dbug,
        info,
        warn,
        fail,
        crit
    }

    /// <summary>
    /// 日志上下文
    /// </summary>
    public interface ILogContext
    {
        /// <summary>
        /// 日志上下文类型
        /// </summary>
        LogContextType Type { get; }

        /// <summary>
        /// 线程 ID
        /// </summary>
        int ThreadID { get; }

        /// <summary>
        /// 记录日志的 class 的完整名称
        /// </summary>
        string ClassFullName { get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 日志生成的时间
        /// </summary>
        DateTime CreateTime { get; }
    }
}
