using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Models.Domain.CrawlUrl;
using D.NovelCrawl.Core.Models.Domain.Novel;
using D.NovelCrawl.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core.Interface
{
    /// <summary>
    /// 与远程个人网站的代理
    /// </summary>
    public interface IWebsiteProxy
    {
        /// <summary>
        /// 获取远程个人网站上面需要爬取的小说列表
        /// 更新小说爬虫持有的小说信息
        /// </summary>
        /// <param name="page">页码信息</param>
        /// <returns></returns>
        ListResult<NovelModel> NovelList(PageModel page = null);

        /// <summary>
        /// 获取个人网站上面保存的完整的小说目录信息
        /// 更新小说爬虫持有的小说目录信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        NovelCatalogModel NovelCatalog(Guid uid);

        /// <summary>
        /// 获取小说对应的 url 信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid uid);

        /// <summary>
        /// 上传爬取到的卷详细信息
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        Task UploadVolume(Guid bookUid, Volume volume);

        /// <summary>
        /// 上传爬取到的章节详细信息
        /// </summary>
        /// <param name="uuid">小说 uuid</param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        Task UploadNovelChapter(Guid bookUid, Chapter chapter);

        /// <summary>
        /// 上传小说正文信息
        /// </summary>
        /// <param name="chapter"></param>
        /// <returns></returns>
        Task UploadChapterText(Chapter chapter);

        /// <summary>
        /// 获取某个域名下某个类型的页面的 Spiderscript 处理代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        PageParse UrlPageParseCode(string url, PageType type);
    }
}
