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
    public class UrlCrawlOptions : IUrlCrawlOptions
    {
        public string Token { get; set; }

        public DateTime? StartTime { get; set; }

        public TimeSpan? Interval { get; set; }

        public int? CycleCount { get; set; }
    }
}
