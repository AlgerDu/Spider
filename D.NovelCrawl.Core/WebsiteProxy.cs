using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Models.DTO;
using D.Util.Interface;
using D.Util.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using D.NovelCrawl.Core.Models.Domain.CrawlUrl;
using D.NovelCrawl.Core.Models.Domain.Novel;
using D.Spider.Core;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 与个人网站交互，获取小说爬取的信息，以及上传整理好的小说章节
    /// </summary>
    public class WebsiteProxy : IWebsiteProxy
    {
        const string _host = "http://localhost:8091";

        ILogger _logger;

        IjQuery _jQuery;

        public WebsiteProxy(ILoggerFactory loggerFactory, IjQuery jQuery)
        {
            _logger = loggerFactory.CreateLogger<WebsiteProxy>();

            _jQuery = jQuery;
        }

        public NovelCatalogModel NovelCatalog(Guid uid)
        {
            var result = new NovelCatalogModel();

            var task = _jQuery.Ajax(
                AjaxRequestTypes.POST,
                _host + "/NovelCrawl/NovelCatalog",
                "uid=" + uid.ToString(),
                AjaxContenTypes.x_www_form_urlencoded,
                (object sender, jQuerySuccessEventArgs<Result<NovelCatalogModel>> sea) =>
                {
                    if (sea.Data.Code == 0)
                    {
                        result = sea.Data.Data;
                        _logger.LogInformation("获取小说目录信息成功");
                    }
                    else
                    {
                        _logger.LogWarning("获取小说目录信息失败 code = " + sea.Data.Code);
                    }
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("获取小说目录信息 请求失败 StatusCode = " + eea.StatusCode);
                });

            Task.WaitAll(task);

            return result;
        }

        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid uid)
        {
            var result = new NovelCrawlUrlModel[0];

            var task = _jQuery.Ajax(
                AjaxRequestTypes.POST,
                _host + "/NovelCrawl/BookCrawlUrl",
                "uid=" + uid.ToString(),
                AjaxContenTypes.x_www_form_urlencoded,
                (object sender, jQuerySuccessEventArgs<Result<NovelCrawlUrlModel[]>> sea) =>
                {
                    if (sea.Data.Code == 0)
                    {
                        result = sea.Data.Data;
                        _logger.LogInformation("获取小说对应的爬取 Url 成功");
                    }
                    else
                    {
                        _logger.LogWarning("获取小说对应的爬取 Url 失败 code = " + sea.Data.Code);
                    }
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("获取小说对应的爬取 Url  请求失败 StatusCode = " + eea.StatusCode);
                });

            Task.WaitAll(task);

            return result;
        }

        public ListResult<NovelModel> NovelList(PageModel page = null)
        {
            //TODO 这里暂时是模拟数据
            return new ListResult<NovelModel>()
            {
                PageNumber = 1,
                PageSize = 2,
                RecordCount = 1,
                CurrPageData = new NovelModel[]
                {
                        new NovelModel
                        {
                            Uid = Guid.Parse("6a4cd19a-77f5-4601-ad87-7e23653f00dc"),
                            Name = "修真聊天群"
                        }
                }
            };
        }

        public Task UploadChapterText(Chapter chapter)
        {
            _logger.LogInformation("{0} \n {1}", chapter.Name, chapter.Text);

            chapter.Text = string.Empty;

            return null;
        }

        public Task UploadChapter(Guid bookUid, Chapter chapter)
        {
            _logger.LogInformation("上传章节信息：第 {0} 卷 第 {1} 章 {2}", chapter.VolumeNo, chapter.VolumeIndex, chapter.Name);

            chapter.Uploaded = true;

            return null;
        }

        public Task UploadVolume(Guid bookUid, Volume volume)
        {
            _logger.LogInformation("上传卷信息：第 " + volume.No + " 卷 " + volume.Name);

            volume.Uploaded = true;

            return null;
        }

        public PageParse UrlPageParseCode(string url, PageType type)
        {
            var host = new Url(url).Host;

            if (host == "http://book.qidian.com" && type == PageType.NovelCatalog)
            {
                return new PageParse
                {
                    MinLength = 400000,
                    SSCriptCode = @"
                    var vs:array
                    foreach $('div.catalog-content-wrap div.volume-wrap div.volume')
                        var v:object
                        v = $('h3').html.regex('</a>(?<Name>[\s\S]*?)<i>')
                        var cs:array
                        foreach $('li')
                            var c:object
                            c = $('a').attr('title').regex('时间：(?<PublicTime>[\s\S]*?)章节字数：(?<WordCount>[\d]{0,5})')
                            c.Name = $('a').text
                            c = $('a').attr('href').regex('(?<Vip>vip)')
                            c.Href = $('a').attr('href')
                            cs[] = c
                        v.Chapters = cs
                        vs[] = v
                    return vs"
                };
            }
            else if (host == "http://read.qidian.com" && type == PageType.NovelChatperContext)
            {
                return new PageParse
                {
                    MinLength = -1,
                    SSCriptCode = @"
                    var c:object
                    c.Name = $('div.main-text-wrap h3.j_chapterName').text
                    c.Text = $('div.main-text-wrap div.read-content').html
                    return c"
                };
            }
            else if (host == "http://www.biquge.tw" && type == PageType.NovelCatalog)
            {
                return new PageParse
                {
                    MinLength = -1,
                    SSCriptCode = @"
                    var vs:array
                    foreach $('div.list')
                        var v:object
                        v.Name = $('dt').text
                        var cs:array
                        foreach $('dd')
                            var c:object
                            c.Name = $('a').text
                            c.Href = $('a').attr('href')
                            cs[] = c
                        v.Chapters = cs
                        vs[] = v
                    return vs"
                };
            }
            else if (host == "http://www.biquge.tw" && type == PageType.NovelChatperContext)
            {
                return new PageParse
                {
                    MinLength = -1,
                    SSCriptCode = @"
                    var c:object
                    c.Name = $('div.bookname h1').text
                    c.Text = $('#content').html.remove('<script[^>]*?>.*?</script>|<div align=[^>]*?>.*</div>')
                    return c"
                };
            }
            else
            {
                return null;
            }
        }

        //public bool UploadNovelChapter(Guid uid, ChapterModel chapter)
        //{
        //    var result = false;

        //    chapter.BookUid = uid;

        //    var task = _jQuery.Post(
        //        _host + "/NovelCrawl/UploadChapter",
        //        chapter,
        //        (object sender, jQuerySuccessEventArgs<Result<NovelCrawlUrlModel[]>> sea) =>
        //        {
        //            if (sea.Data.Code == 0)
        //            {
        //                result = true;
        //            }
        //        });

        //    Task.WaitAll(task);

        //    return result;
        //}

        //public bool UploadNovelVolume(Guid uid, VolumeModel volume)
        //{
        //    var result = false;

        //    volume.BookUid = uid;

        //    var task = _jQuery.Post(
        //        _host + "/NovelCrawl/UploadVolume",
        //        volume,
        //        (object sender, jQuerySuccessEventArgs<Result<NovelCrawlUrlModel[]>> sea) =>
        //        {
        //            if (sea.Data.Code == 0)
        //            {
        //                result = true;
        //            }
        //        });

        //    Task.WaitAll(task);

        //    return result;
        //}

        //public PageParse UrlPageProcessSpiderscriptCode(string host, PageType type)
        //{
        //    var result = new PageParse();

        //    var pc = new PageParse
        //    {
        //        Url = host,
        //        Type = type
        //    };

        //    var task = _jQuery.Post(
        //        _host + "/Crawl/PageParseCode",
        //        pc,
        //        (object sender, jQuerySuccessEventArgs<Result<PageParse>> sea) =>
        //        {
        //            result = sea.Data.Data;
        //        });

        //    Task.WaitAll(task);

        //    return result;
        //}

        ///// <summary>
        ///// 获取所有需要爬取的小说
        ///// </summary>
        ///// <param name="page"></param>
        ///// <returns></returns>
        //public ListResult<NovelModel> NovelList(PageModel page = null)
        //{
        //    var url = _host + "/Novel/List";

        //    if (page == null)
        //    {
        //        page = new PageModel() { Number = 1, Size = -1 };
        //    }

        //    var result = new ListResult<NovelModel>();

        //    var task = _jQuery.Ajax(
        //        AjaxRequestTypes.POST,
        //        url,
        //        page,
        //        (object sender, jQuerySuccessEventArgs<ListResult<NovelModel>> sea) =>
        //        {
        //            result = sea.Data;
        //            _logger.LogInformation("获取到需要爬取的小说 " + result.RecordCount);
        //        },
        //        (object sender, jQueryErrorEventArgs eea) =>
        //        {
        //            _logger.LogWarning("请求 Url：" + url + " 失败，状态码：" + (int)eea.StatusCode + "(" + eea.StatusCode + ")");
        //            result = null;
        //        });

        //    Task.WaitAll(task);

        //    return result;
        //}

        ///// <summary>
        ///// 获取爬取某个小说需要的所有的 Url 列表
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <returns></returns>
        //public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrlList(Guid guid)
        //{
        //    var url = _host + "/NovelCrawl/UrlList";

        //    var result = new NovelCrawlUrlModel[0];
        //    var task = _jQuery.Ajax(
        //       AjaxRequestTypes.POST,
        //       url,
        //       guid,
        //       (object sender, jQuerySuccessEventArgs<NovelCrawlUrlModel[]> sea) =>
        //       {
        //           result = sea.Data;
        //           _logger.LogInformation("爬取小说需要的 Url 数量 " + result.Length);
        //       },
        //       (object sender, jQueryErrorEventArgs eea) =>
        //       {
        //           _logger.LogWarning("请求 Url：" + url + " 失败，状态码：" + (int)eea.StatusCode + "(" + eea.StatusCode + ")");
        //           result = null;
        //       });

        //    Task.WaitAll(task);

        //    return result;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="guid"></param>
        ///// <returns></returns>
        //public IEnumerable<VolumeModel> NovelVCInfo(Guid guid)
        //{
        //    return new VolumeModel[]
        //    {

        //    };
        //}
    }
}
