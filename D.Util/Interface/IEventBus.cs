using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 事件总线，用来发布和订阅事件
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 订阅事件
        /// 同一个事件的所有 handler 在处理事件时没有先后之分
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="handler"></param>
        void Subscribe<Event>(IEventHandler<Event> handler) where Event : class, IEvent;

        /// <summary>
        /// 以异步的方式发布一个事件
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="e"></param>
        /// <returns>有多少个 handler 处理了这个事件</returns>
        Task<int> Publish<Event>(Event e) where Event : class, IEvent;
    }
}
