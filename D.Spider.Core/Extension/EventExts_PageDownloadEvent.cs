using D.Spider.Core.Event;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Extension
{
    public static class EventExts_PageDownloadEvent
    {
        public static IPageDownloadEvent CreatePageDownloadEvent(this IEventFactory eventFactory, IPlugin fromPlugin, IUrl url)
        {
            return new PageDownloadEvent
            {
                FromPlugin = fromPlugin.Symbol,
                Url = url,
            };
        }

        public static IPageDownloadCompleteEvent CreatePageDownloadCompleteEvent(this IEventFactory eventFactory, IPlugin fromPlugin, IPluginSymbol toPlugin, IPage page)
        {
            var e = new PageDownloadCompleteEvent
            {
                FromPlugin = fromPlugin.Symbol,
                Page = page
            };

            e.AddToPluginSymbol(toPlugin);

            return e;
        }

        public static IUrlCrawledEvent CreateUrlCrawledEvent(
            this IEventFactory eventFactory
            , IPlugin fromPlugin
            , IPluginSymbol toPlugin
            , IPage page)
        {
            var e = new UrlCrawledEvent
            {
                FromPlugin = fromPlugin.Symbol,
                Page = page
            };

            e.AddToPluginSymbol(toPlugin);

            return e;
        }
    }
}
