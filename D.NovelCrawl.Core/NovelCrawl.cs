using D.Spider.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Spider.Core.Events;
using D.Util.Interface;
using D.Spider.Core;

namespace D.NovelCrawl.Core
{
    /// <summary>
    /// 小说爬虫使用的 UrlManager 负责和个人网站交互
    /// 实现两个接口 IUrlManager, IPageProcess
    /// </summary>
    public class NovelCrawl : IPageProcess
    {
        ILogger _logger;

        IUrlManager _urlManager;

        WebsiteProxy _websiteProxy;

        /// <summary>
        /// 所有的小说信息
        /// </summary>
        Dictionary<Guid, Novel> _novels = new Dictionary<Guid, Novel>();

        public NovelCrawl(
            ILoggerFactory loggerFactory
            , IUrlManager urlManager
            , WebsiteProxy proxy)
        {
            _logger = loggerFactory.CreateLogger<NovelCrawl>();

            _urlManager = urlManager;

            _websiteProxy = proxy;
        }

        public void Run()
        {
            //从网站上面获取需要爬取的小说
            var novelList = _websiteProxy.NovelList();

            if (novelList.RecordCount <= 0)
            {
                _logger.LogWarning("没有需要爬取的小说");
            }
            else
            {
                foreach (var n in novelList.CurrPageData)
                {
                    Novel dealNovel = null;

                    if (_novels.ContainsKey(n.Guid))
                    {
                        dealNovel = _novels[n.Guid];
                        dealNovel.Update(n);
                    }
                    else
                    {
                        dealNovel = new Novel();
                        dealNovel.Update(n);

                        _logger.LogInformation("添加需要爬取的小说：" + n.Name);
                        _novels.Add(dealNovel.Guid, dealNovel);
                    }

                    DealNovel(dealNovel);
                }
            }
        }

        #region IPageProcess 实现
        public void Process(IPage page)
        {
            _logger.LogInformation(page.HtmlTxt.Substring(0, 20));
        }
        #endregion

        /// <summary>
        /// 处理小说
        /// 是否需要重新获取信息，判断小说官网目录是否在 UrlManager 列表中
        /// </summary>
        /// <param name="deal"></param>
        private void DealNovel(Novel deal)
        {
            var urls = _websiteProxy.NovelCrawlUrlList(deal.Guid);
            if (urls != null && urls.Count() > 0)
            {
                var official = urls.Where(uu => uu.Official).FirstOrDefault();

                if (official == null)
                {
                    _logger.LogWarning(deal.Name + " 没有记录官网目录 url");
                }
                else
                {
                    deal.OfficialUrl =
                        deal.OfficialUrl == null || deal.OfficialUrl.String != official.Url
                        ? new Url(official.Url) : deal.OfficialUrl;

                    deal.OfficialUrl.Interval = 10;

                    _urlManager.AddUrl(deal.OfficialUrl);
                }
            }
            else
            {
                _logger.LogWarning(deal.Name + " 没有记录需要爬取的 Urls");
            }
        }
    }
}
