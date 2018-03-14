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
    public interface IUrlCrawlEvent : IPluginEvent
    {
        /// <summary>
        /// 需要抓取的 url
        /// </summary>
        IUrl ToCrawlUrl { get; }

        /// <summary>
        /// 爬取参数，不同的 CrawlType 对应的爬去参数也不同
        /// </summary>
        IUrlCrawlOptions CrawlOptions { get; }

        /// <summary>
        /// 页面下载时的一些参数
        /// </summary>
        IPageDownloadOptions PownloadOptions { get; }
    }
}
