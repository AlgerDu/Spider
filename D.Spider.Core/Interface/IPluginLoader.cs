using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件加载器
    /// </summary>
    public interface IPluginLoader
    {
        /// <summary>
        /// 根据 loader 自己的定义加载所有可以找到的 继承自 IPlugin 的类型
        /// 由 manager 去创建实例
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> Load();
    }
}
