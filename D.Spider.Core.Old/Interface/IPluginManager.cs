using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public interface IPluginManager : IRunningable<IPluginManager>
    {
        /// <summary>
        /// 获取所有的插件实例
        /// </summary>
        IEnumerable<IPlugin> Plugins { get; }

        /// <summary>
        /// 查找所有符合条件 symbol 的插件
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        IEnumerable<IPlugin> Search(IPluginSymbol symbol);

        /// <summary>
        /// 查找符合多个 symbol 条件的插件
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        IEnumerable<IPlugin> Search(IEnumerable<IPluginSymbol> symbols);

        /// <summary>
        /// 加载所有的插件，并且实例化
        /// </summary>
        void LoadAllPlugin();
    }
}
