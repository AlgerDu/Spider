using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface.Plugin
{
    /// <summary>
    /// 插件事件
    /// </summary>
    public interface IPluginEvent
    {
        /// <summary>
        /// 事件类型唯一标识
        /// </summary>
        Guid TypeUid { get; set; }

        /// <summary>
        /// 事件类型名称
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// 来源（唯一）
        /// </summary>
        IPluginSymbol FromPlugin { get; }

        /// <summary>
        /// 目标（不唯一，可以是某一个类型的插件）
        /// </summary>
        IPluginSymbol ToPluginSymbol { get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 事件状态
        /// </summary>
        PluginEventState State { get; set; }
    }

    /// <summary>
    /// 插件事件 T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPluginEvent<T> : IPluginEvent
    {
        /// <summary>
        /// 事件所携带的数据
        /// </summary>
        T Data { get; }
    }
}
