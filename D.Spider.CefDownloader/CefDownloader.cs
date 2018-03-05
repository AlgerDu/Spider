using CefSharp;
using CefSharp.Internals;
using CefSharp.OffScreen;
using D.Spider.Core;
using D.Spider.Core.Hnadler;
using D.Spider.Core.Interface;
using D.Util.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace D.Spider.Extension.Plugin
{
    /// <summary>
    /// 封装 CefSharp.OffScreen 下载网页；
    /// 需要修改的地方还比较多，暂时还是已可运行为目标
    /// </summary>
    public class CefDownloader : BasePlugin, IPlugin
        , IPluginEventHandler<IPageDownloadEvent>
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
        IEventFactory _eventFactory;

        bool _running;

        ChromiumWebBrowser _browser;

        IPageDownloadEvent _pageDownloadEvent;

        public CefDownloader(
            IEventBus eventBus
            , IEventFactory eventFactory
            , ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CefDownloader>();

            _eventBus = eventBus;
            _eventFactory = eventFactory;

            CreateSymbol("cef_downloader", PluginType.Downloader);
        }

        #region IPlugin 相关
        public override IPlugin Run()
        {
            lock (this)
            {
                _running = true;

                InitCef();

                var setting = new BrowserSettings();
                setting.ImageLoading = CefState.Disabled;

                _browser = new ChromiumWebBrowser("", setting);

                _browser.LifeSpanHandler = new LifeSpanHandler();

                _browser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(CefBrowserLoadEnd);
                _browser.FrameLoadStart += new EventHandler<FrameLoadStartEventArgs>(FrameLoadStart);
            }

            return this;
        }

        public override IPlugin Stop()
        {
            lock (this)
            {
                ShutdownCef();

                _running = false;
            }

            return this;
        }

        #endregion

        #region Handlers
        public void Handle(IPageDownloadEvent e)
        {
            lock (this)
            {
                if (_pageDownloadEvent != null)
                {
                    _pageDownloadEvent = e;

                    DownloadPage();
                }
                else
                {
                    _logger.LogWarning($"{_symbol} 正在执行下载任务，不能处理事件 {e}");
                }
            }
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
        }

        /// <summary>
        /// 关闭 Cef
        /// </summary>
        private void ShutdownCef()
        {
            Cef.Shutdown();
        }

        /// <summary>
        /// 获取需要下载的 url，并且调用 cef 下载
        /// </summary>
        private void DownloadPage()
        {
            LoadPageAsync(_browser, _pageDownloadEvent.Url.ToString());
        }

        private async void CefBrowserLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                Thread.Sleep(_cefBrowserLoadEndSleepTime);

                var html = await _browser.GetSourceAsync();

                _logger.LogDebug($"{_symbol} {_pageDownloadEvent.Url} 页面下载完成，共 {html.Length} 个字符");

                _downloaderUrl.Page = new Page(html);
                _eventBus.Publish(new UrlCrawledEvent(_downloaderUrl));

                _downloaderUrl = null;

                DownloadPage();
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

        private void FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            var url = _pageDownloadEvent.Url.ToString();

            if (e.Frame.IsMain && e.Url != url)
            {
                _logger.LogWarning($"{_symbol} 下载时页面发生自动跳转 {url} => {e.Url}");

                e.Browser.StopLoad();
            }
        }
        #endregion
    }
}
