using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using System.Net;
using System.IO;
using D.Util.Web;
using D.Util.Models;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace D.Spider.Core
{
    /// <summary>
    /// IDownloader 的实现
    /// 使用 IjQuery 进行网页下载
    /// </summary>
    public class HttpDownloader : IPageDownloader, IDisposable
    {
        /// <summary>
        /// 同时下载的最大数量
        /// </summary>
        const int _maxDownloadNumber = 5;

        IEventBus _eventBus;
        ILogger _logger;

        IUrlManager _urlManager;
        IjQuery _jQuery;

        Task _dealTask;

        int _downloadingNumber;
        ManualResetEvent _mre;

        /// <summary>
        /// 正在下载的页面数量
        /// </summary>
        int DownloadingNumber
        {
            get
            {
                lock (this)
                {
                    return _downloadingNumber;
                }
            }
            set
            {
                lock (this)
                {
                    _downloadingNumber = value;

                    if (_downloadingNumber < _maxDownloadNumber)
                    {
                        _mre.Set();
                    }
                }
            }
        }

        public HttpDownloader(
            ILogger<HttpDownloader> logger
            , IEventBus eventBus
            , IUrlManager urlManager
            , IjQuery jQuery)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<HttpDownloader>();

            _urlManager = urlManager;
            _jQuery = jQuery;

            _downloadingNumber = 0;
            _mre = new ManualResetEvent(true);

            _eventBus.Subscribe(this);
        }

        ~HttpDownloader()
        {
            Dispose();
        }

        public void Handle(UrlWaitingEvent e)
        {
            if (DownloadingNumber >= _maxDownloadNumber)
            {
                _logger.LogInformation("正在下载的网页数量已经达到最大数 " + _maxDownloadNumber);
            }
            else
            {
                _mre.Set();
            }
        }

        public void Run()
        {
            _dealTask = new Task(() =>
            {
                while (DownloadingNumber >= 0)
                {
                    if (DownloadingNumber >= _maxDownloadNumber)
                    {
                        _logger.LogInformation("正在下载的网页数量已经达到最大数 " + _maxDownloadNumber);

                        _mre.Reset();
                    }
                    else
                    {
                        var nextCrawlUrl = _urlManager.NextCrawl();

                        if (nextCrawlUrl == null)
                        {
                            _logger.LogInformation("暂时已经没有需要爬取的网页");
                            _mre.Reset();
                        }
                        else
                        {
                            try
                            {
                                DownloadingNumber++;

                                _logger.LogInformation("开始爬取 " + nextCrawlUrl.String);

                                _jQuery.Ajax(
                                    AjaxRequestTypes.GET,
                                    nextCrawlUrl.String,
                                    null,
                                    AjaxContenTypes.Text,
                                    (object sender, jQuerySuccessEventArgs<string> sea) =>
                                    {
                                        nextCrawlUrl.Page = new Page(sea.Data);
                                        DownloadingNumber--;

                                        _eventBus.Publish(new UrlCrawledEvent(nextCrawlUrl));
                                    },
                                    (object sender, jQueryErrorEventArgs eea) =>
                                    {
                                        DownloadingNumber--;

                                        _logger.LogWarning("请求 Url：" + nextCrawlUrl.String + " 失败，状态码：" + (int)eea.StatusCode + "(" + eea.StatusCode + ")");
                                    });
                            }
                            catch (Exception ex)
                            {
                                DownloadingNumber--;
                                _logger.LogInformation("爬取 url 发生错误：" + ex.ToString());
                            }
                        }
                    }

                    _mre.WaitOne();
                }

                _logger.LogDebug("DealTask 任务结束");
            });

            _dealTask.Start();
        }

        public void Dispose()
        {
            DownloadingNumber = -100;
        }
    }
}
