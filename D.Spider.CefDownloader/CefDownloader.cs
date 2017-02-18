using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;

namespace D.Spider
{
    /// <summary>
    /// 对 cefsharp 的封装用来爬取动态网页
    /// </summary>
    public class CefDownloader : IDownloader
    {
        IEventBus _eventBus;
        ILogger _logger;

        IUrlManager _urlManager;

        int _downloadingNumber;
        CefBrowserMainForm _hideForm;

        public CefDownloader(
            IEventBus eventBus
            , ILoggerFactory loggerFactory
            , IUrlManager urlManager)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<CefDownloader>();

            _urlManager = urlManager;

            _downloadingNumber = 0;

            _eventBus.Subscribe(this);
        }

        /// <summary>
        /// 处理 urlManager 发布的 UrlWaitingEvent
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void Handle(UrlWaitingEvent e)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
