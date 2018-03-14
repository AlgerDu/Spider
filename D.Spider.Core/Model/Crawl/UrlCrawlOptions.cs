using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Spider.Core.Model.Crawl
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlCrawlOptions : IUrlCrawlOptions, ICycleCrawlOptions
    {
        public TimeSpan Interval { get; set; }

        public int CycleCount { get; set; }

        public UrlCrwalEventType CrawlType { get; set; }
    }
}
