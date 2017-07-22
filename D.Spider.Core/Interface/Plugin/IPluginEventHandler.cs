using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface.Plugin
{
    /// <summary>
    /// 插件事件处理器
    /// </summary>
    public interface IPluginEventHandler<TE>
        where TE : IPluginEvent
    {
        /// <summary>
        /// 处理事件，返回值暂定为 bool 标志成功与失败
        /// TODO：可能需要扩充
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool Handle(TE e);
    }
}
