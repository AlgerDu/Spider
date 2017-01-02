using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.DTO
{
    /// <summary>
    /// 小说爬取 Url 列表
    /// </summary>
    public class CrawlUrlModel
    {
        /// <summary>
        /// Url
        /// </summary>
        /// <returns></returns>
        public string Url { get; set; }

        /// <summary>
        /// 是否是官网
        /// </summary>
        /// <returns></returns>
        public bool Official { get; set; }
    }
}
