using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Interface
{
    /// <summary>
    /// Url 需要爬取的事件
    /// </summary>
    public interface IUrlEvent : IPluginEvent
    {
        /// <summary>
        /// 需要抓取的 url
        /// </summary>
        IUrl ToCrawlUrl { get; }

        /// <summary>
        /// 爬去参数
        /// </summary>
        IUrlCrawlOptions CrawlOptions { get; }
    }
}
