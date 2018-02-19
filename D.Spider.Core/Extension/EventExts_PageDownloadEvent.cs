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
                Url = url
            };
        }
    }
}
