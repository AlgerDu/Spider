using D.Spider.Core.Event;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Extension
{
    public static class PageDownloadEventExtensions
    {
        /// <summary>
        /// 创建页面下载事件
        /// </summary>
        /// <param name="fromPlugin"></param>
        /// <param name="url"></param>
        /// <param name="pageDownloadOptions"></param>
        /// <returns></returns>
        public static IPageDownloadEvent CreatePageDownloadEvent(
            this IPlugin fromPlugin
            , IUrl url
            , IPageDownloadOptions pageDownloadOptions = null)
        {
            return new PageDownloadEvent
            {
                FromPlugin = fromPlugin.Symbol,
                Url = url,
                DownloadOptions = pageDownloadOptions
            };
        }

        public static IPageDownloadCompleteEvent CreatePageDownloadCompleteEvent(
            this IPlugin fromPlugin
            , IPluginSymbol toPlugin
            , Guid pageDownloadEventUid
            , IPage page)
        {
            var e = new PageDownloadCompleteEvent
            {
                FromPlugin = fromPlugin.Symbol,
                Page = page,
                PageDownloadEventUid = pageDownloadEventUid
            };

            e.AddToPluginSymbol(toPlugin);

            return e;
        }
    }
}
