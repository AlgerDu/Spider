using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface.Plugin
{
    /// <summary>
    /// 插件的标志
    /// 通过 symbol 可以确定符合条件的一个或者多个插件
    /// </summary>
    public interface IPluginSymbol
    {
        /// <summary>
        /// 实例 Uid
        /// </summary>
        Guid InstanceUid { get; }

        /// <summary>
        /// 插件类型
        /// </summary>
        PluginType PType { get; }

        /// <summary>
        /// 插件名
        /// </summary>
        string Name { get; }
    }
}
