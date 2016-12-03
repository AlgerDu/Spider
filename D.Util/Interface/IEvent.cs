using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 事件
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 唯一标识符
        /// </summary>
        Guid GUID { get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        DateTime TimeStamp { get; }
    }
}
