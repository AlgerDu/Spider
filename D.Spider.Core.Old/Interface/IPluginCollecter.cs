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
        /// <returns>此类型是否已经添加</returns>
        bool Collect<T>() where T : IPlugin;

        /// <summary>
        /// 收集插件类型
        /// </summary>
        /// <param name="plugingType">此类型是否已经添加</param>
        /// <returns></returns>
        bool Collect(Type plugingType);

        /// <summary>
        /// 获取所有已经收集到的插件类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetCollectedPluginType();
    }
}
