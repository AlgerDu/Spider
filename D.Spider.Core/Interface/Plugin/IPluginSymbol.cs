using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 插件的标志
    /// 通过 symbol 可以确定符合条件的一个或者多个插件
    /// 相当与通过三个条件定位插件
    /// 当不限制某个条件时请返回 null
    /// </summary>
    public interface IPluginSymbol : IEqualityComparer<IPluginSymbol>
    {
        /// <summary>
        /// 实例 Uid
        /// </summary>
        Guid? InstanceUid { get; }

        /// <summary>
        /// 插件类型
        /// </summary>
        PluginType? PType { get; }

        /// <summary>
        /// 插件名
        /// </summary>
        string Name { get; }
    }
}
