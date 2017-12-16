using D.Spider.Core.Interface.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件收集器
    /// </summary>
    public interface IPluginCollecter
    {
        /// <summary>
        /// 收集插件类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Collect<T>() where T : IPlugin;

        /// <summary>
        /// 收集插件类型
        /// </summary>
        /// <param name="plugingType"></param>
        bool Collect(Type plugingType);
    }
}
