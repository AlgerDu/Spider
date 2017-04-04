using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models
{
    /// <summary>
    /// 爬取小说的 URL 类型
    /// </summary>
    public enum NovelCrawlUrlType
    {
        /// <summary>
        /// 官网目录
        /// </summary>
        Official = 51,

        /// <summary>
        /// 第三方目录
        /// </summary>
        Third
    }

    /// <summary>
    /// 页面类型
    /// </summary>
    public enum PageType
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknown = 41,

        /// <summary>
        /// 小说目录
        /// </summary>
        NovelCatalog,

        /// <summary>
        /// 小说章节正文
        /// </summary>
        NovelChatperContext
    }
}
