using D.Spider.Core.Events;
using D.Util.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// 下载器
    /// 发布事件：UrlCrawedEvent
    /// 订阅事件：UrlWaitingEvent
    /// </summary>
    public interface IDownloader
        : IEventHandler<UrlWaitingEvent>
    {
        /// <summary>
        /// 由 ISpider 调用，开始爬取 UrlManager 中待爬取的 Url
        /// </summary>
        void Run();
    }
}
