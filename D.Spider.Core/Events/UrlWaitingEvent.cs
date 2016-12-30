using D.Util;
using D.Util.Event;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Events
{
    /// <summary>
    /// 当有 Url 需要爬取的时候，UrlManager 发布这个事件
    /// </summary>
    public class UrlWaitingEvent : BaseEvent, IEvent
    {
    }
}
