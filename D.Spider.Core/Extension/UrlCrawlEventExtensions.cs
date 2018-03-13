using D.Spider.Core.Event;
using D.Spider.Core.Interface;
using D.Spider.Core.Model.Crawl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Extension
{
    public static class UrlCrawlEventExtensions
    {
        /// <summary>
        /// 创建一个 normal 类型的 url 爬取事件
        /// </summary>
        /// <param name="fromPlugin"></param>
        /// <param name="url">要爬取的 url</param>
        /// <param name="pageLoadingSeconds">在浏览器中，页面 ajax 加载数据可能需要的时间</param>
        /// <returns></returns>
        public static IUrlCrawlEvent CreateUrlCrawlEvent(
            this IPlugin fromPlugin
            , string url
            , int pageLoadingSeconds = 2)
        {
            return new UrlCrwalEvent
            {
                FromPlugin = fromPlugin.Symbol,
                ToCrawlUrl = new Url(url),
                CrawlOptions = new UrlCrawlOptions
                {
                    CrawlType = UrlCrwalEventType.Normal
                },
                PownloadOptions = new PageDownloadOptions
                {
                    PageLoadingTime = TimeSpan.FromSeconds(pageLoadingSeconds)
                }
            };
        }
    }
}
