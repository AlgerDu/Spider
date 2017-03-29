using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.DTO
{
    public class PageParse
    {
        public string Url { get; set; }

        /// <summary>
        /// 页面数据提取的 SSCript 代码
        /// </summary>
        public string SSCriptCode { get; set; }

        /// <summary>
        /// 页面 HTML 文本的最小长度，用于最基本的爬取成功标志
        /// </summary>
        public long MinLength { get; set; }

        /// <summary>
        /// SSCriptCode 是否可以通用（同一域名下所有的相同类型的域名都可以使用）
        /// </summary>
        public bool IsCommon { get; set; }

        /// <summary>
        /// 页面类型
        /// </summary>
        public PageType Type { get; set; }
    }
}
