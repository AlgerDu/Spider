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
    public class WebsiteProxy
    {
        const string _host = "http://localhost:5000";

        ILogger _logger;

        IjQuery _jQuery;

        public WebsiteProxy(ILoggerFactory loggerFactory, IjQuery jQuery)
        {
            _logger = loggerFactory.CreateLogger<WebsiteProxy>();

            _jQuery = jQuery;
        }

        /// <summary>
        /// 获取所有需要爬取的小说
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ListResult<NovelModel> NovelList(PageModel page = null)
        {
            var url = _host + "/Novel/List";

            if (page == null)
            {
                page = new PageModel() { Number = 1, Size = -1 };
            }

            var result = new ListResult<NovelModel>();

            var task = _jQuery.Ajax(
                AjaxRequestTypes.POST,
                url,
                page,
                (object sender, jQuerySuccessEventArgs<ListResult<NovelModel>> sea) =>
                {
                    result = sea.Data;
                    _logger.LogInformation("获取到需要爬取的小说 " + result.RecordCount);
                },
                (object sender, jQueryErrorEventArgs eea) =>
                {
                    _logger.LogWarning("请求 Url：" + url + " 失败，状态码：" + (int)eea.StatusCode + "(" + eea.StatusCode + ")");
                    result = null;
                });

            Task.WaitAll(task);

            return result;
        }

        /// <summary>
        /// 获取爬取某个小说需要的所有的 Url 列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IEnumerable<NovelCrawlUrlModel> NovelCrawlUrlList(Guid guid)
        {
            var url = _host + "/NovelCrawl/UrlList";

            var result = new NovelCrawlUrlModel[0];
            var task = _jQuery.Ajax(
               AjaxRequestTypes.POST,
               url,
               guid,
               (object sender, jQuerySuccessEventArgs<NovelCrawlUrlModel[]> sea) =>
               {
                   result = sea.Data;
                   _logger.LogInformation("爬取小说需要的 Url 数量 " + result.Length);
               },
               (object sender, jQueryErrorEventArgs eea) =>
               {
                   _logger.LogWarning("请求 Url：" + url + " 失败，状态码：" + (int)eea.StatusCode + "(" + eea.StatusCode + ")");
                   result = null;
               });

            Task.WaitAll(task);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IEnumerable<VolumeModel> NovelVCInfo(Guid guid)
        {
            return new VolumeModel[]
            {

            };
        }
    }
}
