using D.NovelCrawl.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.NovelCrawl.Core.Models.DTO;
using D.Util.Interface;
using D.NovelCrawl.Core.Models;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 虚拟的 IWebsitProxy 实现
    /// </summary>
    public class VirtualWebsetProxy : IWebsitProxy
    {
        ILogger _logger;

        public VirtualWebsetProxy(
            ILoggerFactory loggerFactory
            )
        {
            _logger = loggerFactory.CreateLogger<VirtualWebsetProxy>();
        }

        public IEnumerable<NovelVolumeModel> NovelCatalog(Guid guid)
        {
            return new NovelVolumeModel[0];
        }

        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid guid)
        {
            return new NovelCrawlUrlModel[]
            {
                new NovelCrawlUrlModel
                {
                    Url = "http://book.qidian.com/info/3602691#Catalog",
                    Official = true
                }
            };
        }

        public ListResult<NovelListModel> NovelList(PageModel page = null)
        {
            return new ListResult<NovelListModel>
            {
                RecordCount = 1,
                PageNumber = 1,
                PageSize = -1,
                CurrPageData = new NovelListModel[]
                {
                    new NovelListModel {
                        Guid = Guid.NewGuid(),
                        Name = "修真聊天群",
                        ChapterCount = 0,
                        LastChapterName = string.Empty,
                        LastChapterNumber = 0 }
                }
            };
        }

        public bool UploadNovelChapter(NovelChapterDetailModel chapter)
        {
            _logger.LogInformation("上传爬取到的章节信息\r\n" + chapter.ToString());
            return true;
        }

        public string UrlPageProcessSpiderscriptCode(string host, UrlTypes type)
        {
            if (host == "http://book.qidian.com" && type == UrlTypes.NovleCatalog)
            {
                return @"var Volumes:array
                         foreach $('div.volume')
                             var v:object
                             v.Name = $('h3').inner
                             foreach $('li')
                                 var c:object
                                 c.Name = $('a').attr('title')
                         return Volumes";
            }
            //else if ()
            //{

            //}
            else
            {
                return null;
            }
        }
    }
}
