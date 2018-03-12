using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Event
{
    public class UrlCrwalEvent : BaseEvent, IUrlCrawlEvent
    {
        public IUrl ToCrawlUrl { get; set; }

        public IUrlCrawlOptions CrawlOptions { get; set; }

        public UrlCrwalEventType CrawlType { get; set; }

        public UrlCrwalEvent() : base()
        {
            CrawlType = UrlCrwalEventType.Normal;

            DealType = DealPluginEventType.First;

            AddToPluginSymbol(PluginType.UrlManager);
        }

        public override string ToString()
        {
            return $"事件 {Uid} ：{CrawlType} 爬取 {ToCrawlUrl}";
        }
    }
}
