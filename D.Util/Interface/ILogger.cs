using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 日志接口，仿 .net core 日志记录
    /// 使用 factory 传入的 writer 进行日志的写入
    /// </summary>
    public interface ILogger
    {
        #region format, args
        /// <summary>
        /// 记录最详细的日志消息，通常仅用于开发阶段调试问题
        /// </summary>
        /// <param name="format"></param>
        void LogTrace(string format, params object[] args);

        /// <summary>
        /// 在开发阶段短期内比较有用。它们包含一些可能会对调试有所助益、但没有长期价值的信息。
        /// </summary>
        /// <param name="format"></param>
        void LogDebug(string format, params object[] args);

        /// <summary>
        /// 这种消息被用于跟踪应用程序的一般流程
        /// </summary>
        /// <param name="format"></param>
        void LogInformation(string format, params object[] args);

        /// <summary>
        /// 当应用程序出现错误或其它不会导致程序停止的流程异常或意外事件时使用警告级别，以供日后调查。
        /// </summary>
        /// <param name="format"></param>
        void LogWarning(string format, params object[] args);

        /// <summary>
        /// 当应用程序由于某些故障停止工作则需要记录错误日志。
        /// </summary>
        /// <param name="format"></param>
        void LogError(string format, params object[] args);

        /// <summary>
        /// 当应用程序或系统崩溃、遇到灾难性故障，需要立即被关注时，应当记录关键级别的日志。
        /// </summary>
        /// <param name="format"></param>
        void LogCritical(string format, params object[] args);
        #endregion

        #region eventID, format, args
        /// <summary>
        /// 记录最详细的日志消息，通常仅用于开发阶段调试问题
        /// </summary>
        /// <param name="format"></param>
        void LogTrace(int eventID, string format, params object[] args);

        /// <summary>
        /// 在开发阶段短期内比较有用。它们包含一些可能会对调试有所助益、但没有长期价值的信息。
        /// </summary>
        /// <param name="format"></param>
        void LogDebug(int eventID, string format, params object[] args);

        /// <summary>
        /// 这种消息被用于跟踪应用程序的一般流程
        /// </summary>
        /// <param name="format"></param>
        void LogInformation(int eventID, string format, params object[] args);

        /// <summary>
        /// 当应用程序出现错误或其它不会导致程序停止的流程异常或意外事件时使用警告级别，以供日后调查。
        /// </summary>
        /// <param name="format"></param>
        void LogWarning(int eventID, string format, params object[] args);

        /// <summary>
        /// 当应用程序由于某些故障停止工作则需要记录错误日志。
        /// </summary>
        /// <param name="format"></param>
        void LogError(int eventID, string format, params object[] args);

        /// <summary>
        /// 当应用程序或系统崩溃、遇到灾难性故障，需要立即被关注时，应当记录关键级别的日志。
        /// </summary>
        /// <param name="format"></param>
        void LogCritical(int eventID, string format, params object[] args);
        #endregion
    }
}
