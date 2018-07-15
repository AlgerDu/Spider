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
        /// 插件分类
        /// </summary>
        PluginType? PType { get; }

        /// <summary>
        /// 实例 ID，在一个 spider 的生命周期内，应该是唯一的 (应该有 plugin manager 给与赋值，不暴露 set 方法，由其它方式赋值)
        /// </summary>
        int? InstanceID { get; }

        /// <summary>
        /// 插件名（理论上应该是唯一的，但是如何让大家的插件名唯一，需要想办法解决）
        /// </summary>
        string Name { get; }
    }
}
