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
    public interface IResult
    {
        /// <summary>
        /// 返回码 0 代表成功
        /// </summary>
        int Code { get; }

        /// <summary>
        /// 返回消息
        /// </summary>
        string Message { get; }
    }

    /// <summary>
    /// 通用数据返回接口
    /// </summary>
    public interface IResult<T> : IResult where T : class
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; }
    }
}
