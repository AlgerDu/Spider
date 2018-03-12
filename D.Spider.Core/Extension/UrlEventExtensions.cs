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
    public static class UrlEventExtensions
    {
        public static IUrlCrawlEvent CreateUrlEvent(this IEventFactory eventFactory, IPlugin fromPlugin, string url)
        {
            return new UrlCrwalEvent
            {
                FromPlugin = fromPlugin.Symbol,
                ToCrawlUrl = new Url(url)
            };
        }
    }
}
