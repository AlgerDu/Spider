using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models.Domain.Novel;
using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Models.Domain.CrawlUrl
{
    /// <summary>
    /// 爬取小说使用的 url
    /// </summary>
    internal class NovelCrawlUrl : Spider.Core.Url, IUrl
    {
        IWebsiteProxy _website;
        PageType _type;

        #region 继承需要构造函数

        public NovelCrawlUrl(
            string url
            , IWebsiteProxy website
            , PageType type)
            : base(url)
        {
            _website = website;
            _type = type;
        }

        //public NovelCrawlUrl(string host, string relativePath, IWebsiteProxy website)
        //    : this(host + relativePath, website)
        //{ }
        #endregion

        /// <summary>
        /// url 对应页面的处理代码
        /// </summary>
        public PageParse PaseCode
        {
            get
            {
                return _website.UrlPageParseCode(String, PageType);
            }
        }

        /// <summary>
        /// url 对应的页面类型
        /// </summary>
        public PageType PageType { get { return _type; } }

        /// <summary>
        /// url 关联的小说领域模型
        /// </summary>
        public Novel.Novel NovelInfo { get; set; }
    }

    /// <summary>
    /// 目录页面
    /// </summary>
    internal class CatalogUrl : NovelCrawlUrl
    {
        public CatalogUrl(
            string url
            , IWebsiteProxy website
            , PageType type)
            : base(url, website, type)
        { }

        /// <summary>
        /// 是否官网
        /// </summary>
        public bool Official { get; set; }
    }

    /// <summary>
    /// 目录页面
    /// </summary>
    internal class ChapterTxtUrl : NovelCrawlUrl
    {
        public ChapterTxtUrl(
            string url
            , IWebsiteProxy website
            , PageType type)
            : base(url, website, type)
        { }

        /// <summary>
        /// 关联的小说章节
        /// </summary>
        public Chapter ChapterInfo { get; set; }
    }
}
