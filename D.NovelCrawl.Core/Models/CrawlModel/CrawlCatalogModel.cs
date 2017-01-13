using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.CrawlModel
{
    /// <summary>
    /// 爬取到的小说章节
    /// </summary>
    internal class CrawlChapterModel
    {
        public string Name { get; set; }
        public string PublicTime { get; set; }
        public string WordCount { get; set; }
        public string Href { get; set; }
        public string Vip { get; set; }
    }

    /// <summary>
    /// 爬取到的小说卷
    /// </summary>
    internal class CrawlVolumeModel
    {
        public string Name { get; set; }

        public CrawlChapterModel[] Chapters { get; set; }
    }
}
