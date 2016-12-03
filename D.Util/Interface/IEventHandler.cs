using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Util.Interface
{
    /// <summary>
    /// 事件处理程序
    /// </summary>
    public interface IEventHandler<Event>
        where Event : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="e"></param>
        void Handle(Event e);
    }
}
