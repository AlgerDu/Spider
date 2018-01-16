using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    public class UrlEvent : BaseEvent, IUrlEvent
    {
        public IUrl ToCrawlUrl { get; set; }

        public IUrlCrawlOptions CrawlOptions { get; set; }

        public UrlEvent() : base()
        {
            DealType = DealPluginEventType.First;

            AddToPluginSymbol(new PluginSymbol
            {
                PType = PluginType.UrlManager
            });
        }

        public override string ToString()
        {
            return $"{Uid} 爬取 {ToCrawlUrl}";
        }
    }
}
