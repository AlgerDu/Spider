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
    //public class VirtualWebsetProxy : IWebsitProxy
    //{
    //    readonly NovelModel[] _novels = new NovelModel[]
    //            {
    //                new NovelModel {
    //                    Uuid = Guid.NewGuid(),
    //                    Name = "修真聊天群"}
    //            };


    //    ILogger _logger;

    //    public VirtualWebsetProxy(
    //        ILoggerFactory loggerFactory
    //        )
    //    {
    //        _logger = loggerFactory.CreateLogger<VirtualWebsetProxy>();
    //    }

    //    public NovelCatalogModel NovelCatalog(Guid uuid)
    //    {
    //        return new NovelCatalogModel();
    //    }

    //    public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid uuid)
    //    {
    //        return new NovelCrawlUrlModel[]
    //        {
    //            new NovelCrawlUrlModel
    //            {
    //                Url = "http://book.qidian.com/info/3602691#Catalog",
    //                Official = true
    //            },
    //            new NovelCrawlUrlModel
    //            {
    //                Url = "http://www.biquge.tw/36_36007/",
    //                Official = false
    //            }
    //        };
    //    }

    //    public ListResult<NovelModel> NovelList(PageModel page = null)
    //    {
    //        return new ListResult<NovelModel>
    //        {
    //            RecordCount = 1,
    //            PageNumber = 1,
    //            PageSize = -1,
    //            CurrPageData = _novels
    //        };
    //    }

    //    public string UrlPageProcessSpiderscriptCode(string host, UrlTypes type)
    //    {
    //        if (host == "http://book.qidian.com" && type == UrlTypes.NovleCatalog)
    //        {
    //            return @"
    //                var vs:array
    //                foreach $('div.catalog-content-wrap div.volume-wrap div.volume')
    //                    var v:object
    //                    v.Name = $('h3').text
    //                    var cs:array
    //                    foreach $('li')
    //                        var c:object
    //                        c = $('a').attr('title').regex('时间：(?<PublicTime>[\s\S]*?)章节字数：(?<WordCount>[\d]{0,5})')
    //                        c.Name = $('a').text
    //                        c = $('a').attr('href').regex('(?<Vip>vip)')
    //                        c.Href = $('a').attr('href')
    //                        cs[] = c
    //                    v.Chapters = cs
    //                    vs[] = v
    //                return vs";
    //        }
    //        else if (host == "http://read.qidian.com" && type == UrlTypes.NovleChapterTxt)
    //        {
    //            return @"
    //                var c:object
    //                c.Name = $('div.main-text-wrap h3.j_chapterName').text
    //                c.Text = $('div.main-text-wrap div.read-content').html
    //                return c";
    //        }
    //        else if (host == "http://www.biquge.tw" && type == UrlTypes.NovleCatalog)
    //        {
    //            return @"
    //                var vs:array
    //                foreach $('div.list')
    //                    var v:object
    //                    v.Name = $('dt').text
    //                    var cs:array
    //                    foreach $('dd')
    //                        var c:object
    //                        c.Name = $('a').text
    //                        c.Href = $('a').attr('href')
    //                        cs[] = c
    //                    v.Chapters = cs
    //                    vs[] = v
    //                return vs";
    //        }
    //        else if (host == "http://www.biquge.tw" && type == UrlTypes.NovleChapterTxt)
    //        {
    //            return @"
    //                var c:object
    //                c.Name = $('div.bookname h1').text
    //                c.Text = $('#content').html.remove('<script[^>]*?>.*?</script>|<div align=[^>]*?>.*</div>')
    //                return c";
    //        }
    //        else
    //        {
    //            return string.Empty;
    //        }
    //    }

    //    public bool UploadNovelVolume(Guid uuid, VolumeModel chapter)
    //    {
    //        return false;
    //    }

    //    public bool UploadNovelChapter(Guid uuid, ChapterModel chapter)
    //    {
    //        _logger.LogDebug("{0}:\r\n{1}", chapter.Name, chapter.Text);

    //        return true;
    //    }
    //}
}
