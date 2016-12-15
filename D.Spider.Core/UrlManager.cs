using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;

namespace D.Spider.Core
{
    /// <summary>
    /// IUrlManager 的实现
    /// </summary>
    public class UrlManager : IUrlManager
    {
        IEventBus _eventBus;
        ILogger _logger;

        List<IUrl> _allUrl;
        List<IUrl> _waitingCrawlUrl;
        List<IUrl> _crawlingUrl;

        public UrlManager(IEventBus eventBus, ILoggerFactory loggerFactory)
        {
            _eventBus = eventBus;
            _logger = loggerFactory.CreateLogger<UrlManager>();

            _allUrl = new List<IUrl>();
            _waitingCrawlUrl = new List<IUrl>();
            _crawlingUrl = new List<IUrl>();

            _eventBus.Subscribe(this);
        }

        ~UrlManager()
        {
            _allUrl.Clear();
            _waitingCrawlUrl.Clear();
            _crawlingUrl.Clear();
        }

        #region IUrlManager 接口实现

        public void AddUrl(IUrl url)
        {
            var find = _allUrl.Find(uu => uu.Equal(url));

            _logger.LogInformation("向 UrlManager 中添加 Url " + url.UrlString);

            if (find == null)
            {
                _allUrl.Add(url);

                find = url;
            }

            if (url.NeedCrawl() && _waitingCrawlUrl.Find(uu => uu == find) == null)
            {
                _waitingCrawlUrl.Add(find);

                _eventBus.Publish(new UrlWaitingEvent());
            }
        }

        public bool IsWaitingCrawl(IUrl url)
        {
            foreach (var lUrl in _allUrl)
            {
                if (lUrl.Equal(url))
                {
                    return true;
                }
            }

            return false;
        }

        public IUrl NextCrawl()
        {
            lock (this)
            {
                var nextUrl = _waitingCrawlUrl.FirstOrDefault();

                if (nextUrl != null)
                {
                    _waitingCrawlUrl.RemoveAt(0);
                    _crawlingUrl.Add(nextUrl);
                }

                return nextUrl;
            }
        }

        public void Handle(UrlCrawledEvent e)
        {
            lock (this)
            {

                var findIndex = _crawlingUrl.FindIndex(u => u.Equal(e.Page.Url));
                if (findIndex > -1)
                {
                    _crawlingUrl.RemoveAt(findIndex);
                }
            }
        }

        #endregion
    }
}
