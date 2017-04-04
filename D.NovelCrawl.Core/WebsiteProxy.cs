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
            var data = new ChapterTxtUploadModel
            {
                CUid = chapter.Uid,
                Text = chapter.Text
            };

            return _jQuery.Ajax(
                AjaxRequestTypes.POST,
                _host + "/NovelCrawl/UploadChapterText",
                data,
                AjaxContenTypes.JSON,
                (object sender, jQuerySuccessEventArgs<Result> sea) =>
                {
                    if (sea.Data.Code == 0)
                    {
                        _logger.LogInformation("上传章节内容 {0} \n {1}", chapter.Name, chapter.Text);
                        chapter.NeedRecrawl = false;
                        chapter.Text = string.Empty;
                    }
                    else
                    {
                        _logger.LogWarning("上传章节内容 失败 code = " + sea.Data.Code + " \r\n Message = " + sea.Data.Message);
                    }
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("上传章节内容  请求失败 StatusCode = " + eea.StatusCode);
                });
        }

        public Task UploadChapter(Guid bookUid, Chapter chapter)
        {
            var data = new ChapterUploadModel
            {
                BookUid = bookUid,
                Name = chapter.Name,
                PublishTime = chapter.PublishTime,
                Uid = chapter.Uid,
                Vip = chapter.Vip,
                VolumeIndex = chapter.VolumeIndex,
                VolumeNo = chapter.VolumeNo,
                WordCount = chapter.WordCount
            };

            return _jQuery.Ajax(
                AjaxRequestTypes.POST,
                _host + "/NovelCrawl/UploadChapter",
                data,
                AjaxContenTypes.JSON,
                (object sender, jQuerySuccessEventArgs<Result> sea) =>
                {
                    if (sea.Data.Code == 0)
                    {
                        chapter.Uploaded = true;

                        _logger.LogInformation("上传章节信息：第 {0} 卷 第 {1} 章 {2}", chapter.VolumeNo, chapter.VolumeIndex, chapter.Name);
                    }
                    else
                    {
                        _logger.LogWarning("上传章节信息 失败 code = " + sea.Data.Code);
                    }
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("上传章节信息  请求失败 StatusCode = " + eea.StatusCode);
                });
        }

        public Task UploadVolume(Guid bookUid, Volume volume)
        {
            _logger.LogInformation("上传卷信息：第 " + volume.No + " 卷 " + volume.Name);

            var data = new VolumeModel
            {
                BookUid = bookUid,
                Name = volume.Name,
                No = volume.No
            };

            return _jQuery.Ajax(
                AjaxRequestTypes.POST,
                _host + "/NovelCrawl/UploadVolume",
                data,
                AjaxContenTypes.JSON,
                (object sender, jQuerySuccessEventArgs<Result> sea) =>
                {
                    if (sea.Data.Code == 0)
                    {
                        volume.Uploaded = true;
                        _logger.LogInformation("上传卷信息：第 " + volume.No + " 卷 " + volume.Name);
                    }
                    else
                    {
                        _logger.LogWarning("上传卷信息 失败 code = " + sea.Data.Code);
                    }
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("上传卷信息  请求失败 StatusCode = " + eea.StatusCode);
                });
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
    }
}
