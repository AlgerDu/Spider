using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件事件处理器
    /// </summary>
    public interface IPluginEventHandler<Event>
        where Event : class, IPluginEvent
    {
        /// <summary>
        /// 处理事件，返回值暂定为 bool 标志成功与失败
        /// TODO：返回值可能需要扩充
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        void Handle(Event e);
    }
}
