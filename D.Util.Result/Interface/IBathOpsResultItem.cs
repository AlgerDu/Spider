using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 通用数据返回接口
    /// </summary>
    public interface IBathOpsResultItem
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        int Code { get; }

        /// <summary>
        /// item 在批量操作中顺序
        /// </summary>
        long Index { get; }

        /// <summary>
        /// 返回消息
        /// </summary>
        string Message { get; }
    }

    /// <summary>
    /// 批量操作结果
    /// </summary>
    public interface IBathOpsResult : IResult
    {
        /// <summary>
        /// 操作的总数
        /// </summary>
        long OpsCount { get; }

        /// <summary>
        /// 操作结果
        /// </summary>
        IEnumerable<IBathOpsResultItem> Items { get; }
    }

    /// <summary>
    /// 批量操作结果项（泛型）
    /// </summary>
    public interface IBathOpsResultItem<T>
    {
        /// <summary>
        /// 返回码 0 代表成功
        /// </summary>
        int Code { get; }

        /// <summary>
        /// item 在批量操作中顺序
        /// </summary>
        long Index { get; }

        /// <summary>
        /// 返回消息
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 需要返回的数据
        /// </summary>
        T Data { get; }
    }

    /// <summary>
    /// 批量操作结果（泛型）
    /// </summary>
    public interface IBathOpsResult<T> : IResult
    {
        /// <summary>
        /// 操作的总数
        /// </summary>
        long OpsCount { get; }

        /// <summary>
        /// 操作结果
        /// </summary>
        IEnumerable<IBathOpsResultItem<T>> Items { get; }
    }
}
