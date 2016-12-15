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

        public HttpDownloader(
            IEventBus eventBus
            , ILoggerFactory loggerFactory
            , IUrlManager urlManager)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<HttpDownloader>();
            _urlManager = urlManager;

            _eventBus.Subscribe(this);
        }

        public void Handle(UrlWaitingEvent e)
        {
            var url = _urlManager.NextCrawl();

            if (url != null)
            {
                try
                {
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url.RelativePath);

                    using (WebResponse wr = myReq.GetResponse())
                    {
                        Stream respStream = wr.GetResponseStream();
                        using (StreamReader reader = new StreamReader(respStream))
                        {
                            var html = reader.ReadToEnd();
                            _eventBus.Publish(new UrlCrawledEvent(new Page(url, html)));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void Run()
        {

        }
    }
}
