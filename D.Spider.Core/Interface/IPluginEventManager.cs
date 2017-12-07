using D.Spider.Core.Interface.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件事件管理者
    /// </summary>
    public interface IPluginEventManager
    {
        /// <summary>
        /// 添加事件处理器
        /// </summary>
        /// <param name="plugin"></param>
        void AddEventHandler(IPlugin plugin);

        /// <summary>
        /// 移除事件处理器
        /// </summary>
        /// <param name="plugin"></param>
        void RemoveEventHandler(IPlugin plugin);
    }
}
