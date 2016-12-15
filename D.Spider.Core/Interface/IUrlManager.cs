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
    /// Url 管理者（已经爬取和尚未爬取的）
    /// 发布事件：UrlWaitingEvent
    /// 订阅事件：UrlCrawedEvent
    /// </summary>
    public interface IUrlManager
        : IEventHandler<UrlCrawledEvent>
    {
        /// <summary>
        /// 向 IUrlList 中添加一个的 URL
        /// </summary>
        /// <param name="url">待爬取的 URL</param>
        /// <returns></returns>
        void AddUrl(IUrl url);

        /// <summary>
        /// 判断一个 URL 是否在待爬取队列中
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool IsWaitingCrawl(IUrl url);

        /// <summary>
        /// 从待爬取的 URL 队列中获取下一个需要爬取的 RUL
        /// 同时会将 这个 URL 从待爬取的队列中放到正在爬取的 RUl 队列中
        /// 当带爬取的队列为空时返回 null
        /// </summary>
        /// <returns></returns>
        IUrl NextCrawl();
    }
}
