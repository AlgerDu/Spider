using D.NovelCrawl.Core.Models;
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
    public interface IWebsitProxy
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
        /// <param name="uuid"></param>
        /// <returns></returns>
        NovelCatalogModel NovelCatalog(Guid uuid);

        /// <summary>
        /// 获取小说对应的 url 信息
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid uuid);

        /// <summary>
        /// 上传爬取到的卷详细信息
        /// </summary>
        /// <param name="uuid">小说 uuid</param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        bool UploadNovelVolume(Guid uuid, VolumeModel chapter);

        /// <summary>
        /// 上传爬取到的章节详细信息
        /// </summary>
        /// <param name="uuid">小说 uuid</param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        bool UploadNovelChapter(Guid uuid, ChapterModel chapter);

        /// <summary>
        /// 获取某个域名下某个类型的页面的 Spiderscript 处理代码
        /// </summary>
        /// <param name="host"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string UrlPageProcessSpiderscriptCode(string host, UrlTypes type);
    }
}
