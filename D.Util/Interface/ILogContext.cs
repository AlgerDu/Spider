using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 日志类型定义需要按照严重等级，从上到下，依次越来越严重
    /// </summary>
    public enum LogLevel
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
        LogLevel Type { get; }

        /// <summary>
        /// 事件 ID 用来对同一业务的日志进行区分，方便日志查找
        /// </summary>
        int EventID { get; set; }

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
