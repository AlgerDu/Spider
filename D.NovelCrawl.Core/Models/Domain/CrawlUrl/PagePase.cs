using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.Domain.CrawlUrl
{
    /// <summary>
    /// 值对象 属于领域对象 Url
    /// </summary>
    public class PageParse
    {
        /// <summary>
        /// 页面数据提取的 SSCript 代码
        /// </summary>
        public string SSCriptCode { get; set; }

        /// <summary>
        /// 页面 HTML 文本的最小长度，用于最基本的爬取成功标志
        /// </summary>
        public long MinLength { get; set; }
    }
}
