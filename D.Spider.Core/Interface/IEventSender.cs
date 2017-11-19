using D.Spider.Core.Interface.Plugin;
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
    public interface IEventSender
    {
        /// <summary>
        /// 发送事件到事件控制中心
        /// </summary>
        /// <param name="e"></param>
        void Send(IPluginEvent e);

        /// <summary>
        /// 取消一个已经发送的事件
        /// 处理中以及处理完成的事件不能取消
        /// </summary>
        /// <param name="e"></param>
        /// <returns>是否成功</returns>
        bool Cancel(IPluginEvent e);
    }
}
