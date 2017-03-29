using D.NovelCrawl.Core.Interface;
using D.NovelCrawl.Core.Models;
using D.NovelCrawl.Core.Models.DTO;
using D.Util.Interface;
using D.Util.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 与个人网站交互，获取小说爬取的信息，以及上传整理好的小说章节
    /// </summary>
    public class WebsiteProxy : IWebsitProxy
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

            var task = _jQuery.Post(
                _host + "/NovelCrawl/NovelCatalog",
                "uid=" + uid.ToString(),
                (object sender, jQuerySuccessEventArgs<Result<NovelCatalogModel>> sea) =>
                {
                    result = sea.Data.Data;
                    _logger.LogInformation("获取到需要爬取的小说 " + result.Vs.Length);
                });

            Task.WaitAll(task);

            return result;
        }

        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrls(Guid uid)
        {
            throw new NotImplementedException();
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
                        Uuid = Guid.Parse("6a4cd19a-77f5-4601-ad87-7e23653f00dc"),
                        Name = "修真聊天群"
                    }
                }
            };
        }

        public bool UploadNovelChapter(Guid uid, ChapterModel chapter)
        {
            throw new NotImplementedException();
        }

        public bool UploadNovelVolume(Guid uid, VolumeModel chapter)
        {
            throw new NotImplementedException();
        }

        public string UrlPageProcessSpiderscriptCode(string host, UrlTypes type)
        {
            throw new NotImplementedException();
        }

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
