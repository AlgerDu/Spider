using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;
using CefSharp.OffScreen;
using CefSharp;
using System.Threading;
using CefSharp.Internals;

namespace D.Spider.Core
{
    /// <summary>
    /// 封装 CefSharp.OffScreen 下载网页请求
    /// </summary>
    public class CefDownloader : IDownloader, IDisposable
    {
        #region 常量
        /// <summary>
        /// 最大能够使用的 cefbrowser 数量
        /// </summary>
        const int _maxCefBrowserCount = 1;

        /// <summary>
        /// 防止在网页加载完成之后还有部分需要的数据还没有加载完成，需要等待一段时间 （ms）
        /// </summary>
        const int _cefBrowserLoadEndSleepTime = 500;
        #endregion

        ILogger _logger;
        IEventBus _eventBus;

        IUrlManager _urlManager;
        bool _running;

        ChromiumWebBrowser _browser;
        IUrl _downloaderUrl;

        Task _downloadTask;
        ManualResetEvent _mre;

        public CefDownloader(
            IEventBus eventBus
            , ILoggerFactory loggerFactory
            , IUrlManager urlManager)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<CefDownloader>();

            _urlManager = urlManager;

            _mre = new ManualResetEvent(false);

            _eventBus.Subscribe(this);
        }

        ~CefDownloader()
        {
            Dispose();
        }

        #region IDownloader 接口实现
        public void Handle(UrlWaitingEvent e)
        {
            DownloaderPage();
        }

        public void Run()
        {
            _downloadTask = new Task(() =>
            {
                InitCef();

                lock (this)
                {
                    _running = true;
                }

                DownloaderPage();

                _mre.WaitOne();

                ShutdownCef();
            });

            _downloadTask.Start();
        }
        #endregion

        #region IDisposable 实现
        public void Dispose()
        {
            lock (this)
            {
                _running = false;
            }

            _mre.Set();
        }
        #endregion

        #region Cef 相关
        /// <summary>
        /// 初始化 cef
        /// </summary>
        private void InitCef()
        {
            var set = new CefSettings();
            set.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";

            Cef.Initialize(set, performDependencyCheck: true, browserProcessHandler: null);

            _browser = new ChromiumWebBrowser();
            _browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(CefBrowserLoadEnd);

            _logger.LogDebug("cef 初始化完成");
        }

        /// <summary>
        /// 关闭 Cef
        /// </summary>
        private void ShutdownCef()
        {
            _logger.LogDebug("cef 关闭");
            Cef.Shutdown();
        }

        private async void CefBrowserLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                Thread.Sleep(_cefBrowserLoadEndSleepTime);

                var html = await _browser.GetSourceAsync();

                _eventBus.Publish(new UrlCrawledEvent(new Page(_downloaderUrl, html)));

                DownloaderPage();
            }
        }

        private Task LoadPageAsync(IWebBrowser browser, string address)
        {
            //If using .Net 4.6 then use TaskCreationOptions.RunContinuationsAsynchronously
            //and switch to tcs.TrySetResult below - no need for the custom extension method
            var tcs = new TaskCompletionSource<bool>();

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    //This is required when using a standard TaskCompletionSource
                    //Extension method found in the CefSharp.Internals namespace
                    tcs.TrySetResultAsync(true);
                }
            };

            browser.LoadingStateChanged += handler;

            if (!string.IsNullOrEmpty(address))
            {
                browser.Load(address);
            }
            return tcs.Task;
        }

        /// <summary>
        /// 获取需要下载的 url，并且调用 cef 下载
        /// </summary>
        private void DownloaderPage()
        {
            lock (this)
            {
                if (!_running)
                    return;

                if (_downloaderUrl != null)
                    return;
                else
                {
                    _downloaderUrl = _urlManager.NextCrawl();

                    if (_downloaderUrl != null)
                    {
                        LoadPageAsync(_browser, _downloaderUrl.String);
                    }
                }
            }
        }
        #endregion
    }
}
