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
    public interface IPluginEventManager : IRunningable<IPluginEventManager>
    {
        /// <summary>
        /// 添加事件处理器
        /// </summary>
        /// <param name="plugin"></param>
        void AddPlugin(IPlugin plugin);

        /// <summary>
        /// 移除事件处理器
        /// </summary>
        /// <param name="plugin"></param>
        void RemovePlugin(IPlugin plugin);
    }
}
