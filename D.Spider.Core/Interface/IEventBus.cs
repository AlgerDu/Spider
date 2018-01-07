using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 事件发送器（用来给插件发送事件），将事件发送到事件队列中
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发送事件到事件控制中心
        /// </summary>
        /// <param name="e"></param>
        void Publish<eType>(eType e) where eType : IPluginEvent;

        /// <summary>
        /// 取消一个已经发送的事件
        /// 处理中以及处理完成的事件不能取消
        /// </summary>
        /// <param name="e"></param>
        /// <returns>取消执行的个数</returns>
        int Cancel<eType>(eType e) where eType : IPluginEvent;
    }
}
