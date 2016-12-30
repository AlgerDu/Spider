using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;
using System.Net;
using System.IO;
using D.Util.Web;
using D.Util.Models;

namespace D.Spider.Core
{
    /// <summary>
    /// IDownloader 的实现
    /// 只是提交的 http 请求，不解析 js
    /// </summary>
    public class HttpDownloader : IDownloader
    {
        IEventBus _eventBus;
        ILogger _logger;

        IUrlManager _urlManager;
        IjQuery _jQuery;

        public HttpDownloader(
            IEventBus eventBus
            , ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , IjQuery jQuery)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<HttpDownloader>();

            _urlManager = urlManager;
            _jQuery = jQuery;

            _eventBus.Subscribe(this);
        }

        public void Handle(UrlWaitingEvent e)
        {
            var url = _urlManager.NextCrawl();

            if (url != null)
            {
                try
                {
                    _jQuery.Get(
                        url.UrlString,
                        (object sender, jQuerySuccessEventArgs<string> se) =>
                    {
                        _eventBus.Publish(new UrlCrawledEvent(new Page(url, se.Data)));
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("爬取 url 发生错误：" + ex.ToString());
                }
            }
        }

        public void Run()
        {

        }
    }
}
